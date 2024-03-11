namespace Asset.Booking.Application.Clients.Commands.Shared;
using System.Collections.Generic;
using Asset.Booking.Application.Clients.Commands.Dto;
using Asset.Booking.Domain.Client;
using Asset.Booking.Domain.Client.Validation;
using Asset.Booking.SharedKernel;
using SharedKernel.Exceptions;

internal static class PhoneNumbersParser
{
    public static Result<IEnumerable<PhoneNumber>> GetPhoneNumbers(IEnumerable<PhoneNumberDto> dtoPhoneNumbers)
    {
        var results = new List<PhoneNumber>();
        var errors = new List<Error>();

        foreach (PhoneNumberDto dtoPhoneNumber in dtoPhoneNumbers)
        {
            var numberResult = GetPhoneNumber(dtoPhoneNumber);
            if (numberResult.IsSuccess)
            {
                results.Add(numberResult.Value!);
            }
            else
            {
                errors.Add(numberResult.Error);
            }
        }

        if (errors.Count == 1) return errors.First();
        if (errors.Count > 1) return GenericErrors.AggregatedError(errors);

        return results;
    }

    public static Result<PhoneNumber> GetPhoneNumber(PhoneNumberDto dtoPhoneNumber)
    {
        if (string.IsNullOrWhiteSpace(dtoPhoneNumber.Number))
        {
            return Result<PhoneNumber>.Failure(ClientErrors.InvalidPhoneNumberEmpty);
        }

        var numberType = Enumeration.FromValue<PhoneNumberType>(dtoPhoneNumber.Type);
        if (numberType is null)
        {
            return Result<PhoneNumber>.Failure(ClientErrors.InvalidPhoneNumberType);
        }

        try
        {
            var number = new PhoneNumber(dtoPhoneNumber.Number, numberType);
            return number;
        }
        catch (AssetBookingException ex)
        {
            return ex.Error ?? ClientErrors.InvalidPhoneNumberCharsFor(dtoPhoneNumber.Number);
        }
    }
}
