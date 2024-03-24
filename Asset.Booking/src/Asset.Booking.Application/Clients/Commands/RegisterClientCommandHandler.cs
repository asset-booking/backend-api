namespace Asset.Booking.Application.Clients.Commands;
using Abstractions.Messaging;
using Asset.Booking.Domain.Client.Validation;
using Domain.Client;
using Domain.Client.Abstractions;
using Shared;
using SharedKernel;
using SharedKernel.Exceptions;

public class RegisterClientCommandHandler : ICommandHandler<RegisterClientCommand>
{
    private readonly IClientRepository _clientRepository;

    public RegisterClientCommandHandler(IClientRepository clientRepository) =>
        _clientRepository = clientRepository;

    public async Task<Result> Handle(RegisterClientCommand request, CancellationToken cancellationToken)
    {
        var dto = request.RegisterClientDto;

        var phoneNumbersResult = PhoneNumbersParser.GetPhoneNumbers(dto.PhoneNumbers);
        if (!phoneNumbersResult.IsSuccess)
        {
            return phoneNumbersResult;
        }

        try
        {
            var contacts = new Contacts(
                dto.Email,
                phoneNumbersResult.Value);

            var address = new Address(
                dto.City,
                dto.ZipCode,
                dto.Street,
                dto.StreetNumber);

            var client = new Client(
                request.ClientId,
                dto.CompanyName,
                contacts,
                address.IsEmpty ? null : address);

            await _clientRepository.AddAsync(client, cancellationToken);
            await _clientRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
        catch (AssetBookingException ex) when (ex.Error is not null)
        {
            return ex.Error;
        }
        catch(Exception ex)
        {
            // log exception details
            return new Error("ReservationClient.Registration", ex.Message);
        }

        return Result.Success();
    }
}