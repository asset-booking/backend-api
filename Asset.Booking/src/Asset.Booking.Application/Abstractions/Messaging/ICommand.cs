namespace Asset.Booking.Application.Abstractions.Messaging;

using MediatR;
using SharedKernel;

public interface ICommand : IRequest<Result>
{
}