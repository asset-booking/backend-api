namespace Asset.Booking.Application.Abstractions.Messaging;

using MediatR;
using SharedKernel;

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : ICommand
{
}