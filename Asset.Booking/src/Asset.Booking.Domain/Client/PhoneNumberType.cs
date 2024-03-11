namespace Asset.Booking.Domain.Client;
using SharedKernel;

public class PhoneNumberType(string type, int id) : Enumeration(type, id)
{
    public static PhoneNumberType Company = new("Company", 1);
    public static PhoneNumberType Coordinator = new("Coordinator", 2);
}
