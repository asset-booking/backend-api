namespace Asset.Booking.SharedKernel.Exceptions;
using System;

[Serializable]
public class AssetBookingException : Exception
{
    public AssetBookingException() { }

    public AssetBookingException(string message)
        : base(message) { }

    public AssetBookingException(string message, Exception innerException)
        :base(message, innerException) { }

    public AssetBookingException(Error error) =>
        Error = error;

    public Error? Error { get; }
 }
