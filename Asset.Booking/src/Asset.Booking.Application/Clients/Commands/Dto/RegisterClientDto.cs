namespace Asset.Booking.Application.Clients.Commands.Dto;

public record RegisterClientDto(
    string CompanyName,
    string Email,
    IEnumerable<PhoneNumberDto> PhoneNumbers,
    string? City,
    string? ZipCode,
    string? Street,
    string? StreetNumber);