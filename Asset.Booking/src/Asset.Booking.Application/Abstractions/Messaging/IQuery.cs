namespace Asset.Booking.Application.Abstractions.Messaging;
using MediatR;
using SharedKernel;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
