namespace Asset.Booking.Domain.Client.Validation;

using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Ardalis.GuardClauses;
using SharedKernel.Exceptions;

public static class ClientGuards
{
    public static string PhoneNumberContainsNotAllowedCharacters(this IGuardClause guardClause, string input)
    {
        var pattern = new Regex("^[0123456789\\-\\s()+.]+$");
        if (!pattern.IsMatch(input))
            throw new AssetBookingException(ClientErrors.InvalidPhoneNumberCharsFor(input));

        return input;
    }

    public static string MissingCompanyName(this IGuardClause guardClause, string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            throw new AssetBookingException(ClientErrors.MissingCompanyName);
        }

        return input;
    }

    public static string InvalidEmailAddress(this IGuardClause guardClause, string input)
    {
        if (string.IsNullOrWhiteSpace(input) || !new EmailAddressAttribute().IsValid(input))
        {
            throw new AssetBookingException(ClientErrors.InvalidEmailAddress);
        }

        return input;
    }
}
