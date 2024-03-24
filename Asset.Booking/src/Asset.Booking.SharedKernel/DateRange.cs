namespace Asset.Booking.SharedKernel;
using System;
using Exceptions;

public class DateRange : ValueObject
{
    private DateRange() { }
    
    public DateRange(Date start, Date end)
    {
        StartDate = start.ToDateTime();
        EndDate = end.ToDateTime();

        if (StartDate > EndDate)
        {
            throw new AssetBookingException($"{nameof(EndDate)} cannot be after {nameof(StartDate)}");
        }
    }

    public DateRange(
        int startYear, int startMonth, int startDay,
        int endYear, int endMonth, int endDay)
        : this(new Date(startYear, startMonth, startDay), new Date(endYear, endMonth, endDay)){ }

    public DateRange(DateTime start, DateTime end)
        : this(start.Year, start.Month, start.Day,
            end.Year, end.Month, end.Day) { }

    public DateRange(Date start, TimeSpan interval)
        : this (start.ToDateTime(), interval) { }

    public DateRange(int startYear, int startMonth, int startDay, TimeSpan interval)
        : this(new DateTime(startYear, startMonth, startDay), interval) { }

    public DateRange(DateTime start, TimeSpan interval)
        : this(start, start.Add(interval)) { }

    public DateRange(Date start, int intervalDays)
        : this(start, new TimeSpan(intervalDays, 0, 0, 0)) { }

    public DateRange(int startYear, int startMonth, int startDay, int intervalDays)
        : this(startYear, startMonth, startDay, new TimeSpan(intervalDays, 0, 0, 0)) { }

    public DateRange(DateTime start, int intervalDays)
        : this(start, new TimeSpan(intervalDays, 0, 0, 0)) { }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return StartDate;
        yield return EndDate;
    }

    public DateTime StartDate { get; }

    public DateTime EndDate { get; }

    public DateRange NewStart(DateTime newStart) => new(newStart, EndDate);

    public DateRange NewEnd(DateTime newEnd) => new(StartDate, newEnd);

    public bool Overlaps(DateRange other) => StartDate < other.EndDate && EndDate > other.StartDate;

    public bool Overlaps(DateRange other, bool excludeEdges) => excludeEdges
        ? StartDate <= other.EndDate && EndDate >= other.StartDate
        : Overlaps(other);

    public int IntervalNights => (EndDate - StartDate).Days;

    public override string ToString() =>
        $"{StartDate.ToShortDateString()} - {EndDate.ToShortDateString()}";
}

public record Date(int Year, int Month, int Day)
{
    public DateTime ToDateTime() => new (Year, Month, Day);
}