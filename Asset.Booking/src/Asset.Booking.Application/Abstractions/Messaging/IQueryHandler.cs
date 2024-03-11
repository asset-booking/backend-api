namespace Asset.Booking.Application.Abstractions.Messaging;
using MediatR;
using SharedKernel;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}
