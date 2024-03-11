namespace Asset.Booking.Domain.AssetSchedule;
using SharedKernel;

public class Status(string name, int id) : Enumeration(name, id)
{
    public static readonly Status Open = new("Open", 1);
    public static readonly Status DownPayment = new("Down Payment", 2);
    public static readonly Status Paid = new("Paid", 3);
    public static readonly Status Enquiry = new("Enquiry", 4);
    public static readonly Status Reserved = new("Reserved", 5);
    public static readonly Status Renewal = new("Renewal", 6);
}