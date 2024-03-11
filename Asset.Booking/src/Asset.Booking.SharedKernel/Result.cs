namespace Asset.Booking.SharedKernel;

public class Result
{
    protected Result(bool isSuccess, Error? error = null)
    {
        IsSuccess = isSuccess;
        Error = error ?? Error.None;
    }

    public bool IsSuccess { get; }

    public Error Error { get; }

    public static Result Success() => new(true);

    public static Result Failure(Error error) => new(false, error);

    public static implicit operator Result(Error error) => Failure(error);
}

public class Result<TValue> : Result
{
    private Result(TValue value) : base(true) =>
        Value = value;

    private Result(Error error) : base(false, error) =>
        Value = default;

    public TValue? Value { get; }

    public static Result<TValue> Success(TValue value) =>  new (value);
    public static new Result<TValue> Failure(Error error) =>  new (error);

    public static implicit operator Result<TValue>(TValue value) => Success(value);
    public static implicit operator Result<TValue>(Error error) => new(error);
}

public sealed record Error(string Code, string? Message = null)
{
    public static readonly Error None = new (string.Empty);
}
