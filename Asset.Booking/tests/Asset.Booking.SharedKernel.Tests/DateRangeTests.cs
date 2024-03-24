namespace Asset.Booking.SharedKernel.Tests;

public class DateRangeTests
{
    [Fact]
    public void Overlaps_WhenUsingSameDates_ReturnsTrue()
    {
        var dateRange1 = new DateRange(new Date(2024, 1, 5), new Date(2024, 1, 10));
        var dateRange2 = new DateRange(new Date(2024, 1, 5), new Date(2024, 1, 10));

        var overlaps = dateRange1.Overlaps(dateRange2);
        
        Assert.True(overlaps);
    }
    
    [Fact]
    public void Overlaps_WhenUsingDifferentIntervals_ReturnsFalse()
    {
        var dateRange1 = new DateRange(new Date(2024, 1, 5), new Date(2024, 1, 10));
        var dateRange2 = new DateRange(new Date(2024, 1, 15), new Date(2024, 1, 20));

        var overlaps = dateRange1.Overlaps(dateRange2);
        
        Assert.False(overlaps);
    }
    
    [Fact]
    public void Overlaps_WhenUsingOverlappingDates_ReturnsTrue()
    {
        var dateRange1 = new DateRange(new Date(2024, 1, 5), new Date(2024, 1, 15));
        var dateRange2 = new DateRange(new Date(2024, 1, 10), new Date(2024, 1, 20));

        var overlaps = dateRange1.Overlaps(dateRange2);
        
        Assert.True(overlaps);
    }
    
    [Fact]
    public void Overlaps_WhenRangeStartsOnOtherRangeEnd_ReturnsFalse()
    {
        var dateRange1 = new DateRange(new Date(2024, 1, 5), new Date(2024, 1, 15));
        var dateRange2 = new DateRange(new Date(2024, 1, 15), new Date(2024, 1, 20));

        var overlaps = dateRange1.Overlaps(dateRange2);
        
        Assert.False(overlaps);
    }
    
    [Fact]
    public void Overlaps_WhenRangeEndsOnOtherRangeStart_ReturnsFalse()
    {
        var dateRange1 = new DateRange(new Date(2024, 1, 15), new Date(2024, 1, 20));
        var dateRange2 = new DateRange(new Date(2024, 1, 10), new Date(2024, 1, 15));

        var overlaps = dateRange1.Overlaps(dateRange2);
        
        Assert.False(overlaps);
    }
}