namespace Asset.Booking.API.Controllers;

using Application.Reservations.Commands;
using Application.Reservations.Commands.Dto;
using Application.Reservations.Queries;
using Application.Reservations.Queries.ViewModels;
using Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ReservationController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReservationController(IMediator mediator) =>
        _mediator = mediator;

    [HttpGet("GetById")]
    public async Task<ActionResult<ReservationViewModel>> GetAsync(Guid id)
    {
        var result = await _mediator.Send(new GetReservationByIdQuery(id));
        return result.ToActionResult();
    }

    [HttpPost("CreateReservation")]
    public async Task<ActionResult> PostAsync([FromBody] BookAssetDto reservation)
    {
        var newReservationId = Guid.NewGuid();
        var result = await _mediator.Send(new BookAssetCommand(newReservationId, reservation));
        return result.ToActionResult();
    }
}
