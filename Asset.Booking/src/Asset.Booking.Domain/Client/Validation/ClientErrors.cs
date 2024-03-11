namespace Asset.Booking.Domain.Client.Validation;
using SharedKernel;

public static class ClientErrors
{
    public static Error ClientRegistration = new("Client.Register", "Could not register a new client.");
    public static Error MissingCompanyName = new("Client.CompanyName", "Company name is required.");
    public static Error InvalidPhoneNumberType = new("Client.PhoneNumberType", "An invalid phone number type passed.");
    public static Error InvalidPhoneNumberEmpty = new("Client.PhoneNumberEmpty", "The phone number must be specified.");
    public static Error InvalidPhoneNumberChars = new("Client.PhoneNumberChars", "Phone number contains invalid characters.");
    public static Error PhoneNumberAlreadyAdded = new("Client.PhoneNumberAdded", "This phone number is already added.");
    public static Error InvalidEmailAddress = new("Client.EmailAddress", "The email address is not in correct format.");
    public static Error UpdateWithSameValue = new("Client.SameValuesForUpdate", "The update attempt used the same value as the current ones.");
    public static Error InvalidPhoneNumberCharsFor(string phoneNumber) =>
        new("Client.PhoneNumberChars", $"Phone number contains invalid characters: {phoneNumber}");

}
