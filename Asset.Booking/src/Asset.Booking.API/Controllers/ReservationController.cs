namespace Asset.Booking.API.Controllers;

using Application.Reservations.Commands;
using Application.Reservations.Commands.Dto;
using Application.Reservations.Queries;
using Application.Reservations.Queries.Dto;
using Application.Reservations.Queries.ViewModels;
using Dto;
using Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

[ApiController]
[Route("api/[controller]")]
public class ReservationController(IMediator mediator) : ControllerBase
{
    [HttpGet("GetById")]
    public async Task<ActionResult<ReservationViewModel>> GetAsync(Guid id)
    {
        Result<ReservationViewModel> result = await mediator.Send(new GetReservationByIdQuery(id));
        return result.ToActionResult();
    }

    [HttpGet("GetReservationCost")]
    public async Task<ActionResult<CostsViewModel>> GetCostAsync([FromQuery] CostDto cost)
    {
        Result<CostsViewModel> result = await mediator.Send(new GetReservationCostQuery(cost));
        return result.ToActionResult();
    }

    [HttpPost("CreateReservation")]
    public async Task<ActionResult> CreateAsync([FromBody] BookAssetDto reservation)
    {
        Result result = await mediator.Send(new BookAssetCommand(reservation));
        return result.ToActionResult();
    }
    
    [HttpPut("cancelReservation/{reservationId}")]
    public async Task<ActionResult> CancelAsync([FromRoute] Guid reservationId)
    {
        Result result = await mediator.Send(new CancelReservationCommand(reservationId));
        return result.ToActionResult();
    }

    [HttpPut("RescheduleReservation")]
    public async Task<ActionResult> RescheduleAsync([FromBody] UpdateReservationIntervalDto dto)
    {
        DateRange oldDateRange = new DateRange(dto.OldStart, dto.OldEnd);
        DateRange newDateRange = new DateRange(dto.NewStart, dto.NewEnd);
        Result result = await mediator.Send(new RescheduleCommand(dto.ReservationId, oldDateRange, newDateRange));
        return result.ToActionResult();
    }

    [HttpPut("ChangePrice")]
    public async Task<ActionResult> ChangePriceAsync([FromBody] ChangePriceDto dto)
    {
        Result result = await mediator.Send(new ChangePriceCommand(dto.ReservationId, dto.NewPrice));
        return result.ToActionResult();
    }

    [HttpPut("ChangeStatus")]
    public async Task<ActionResult> ChangeStatusAsync([FromBody] ChangeStatusDto dto)
    {
        Result result = await mediator.Send(new ChangeStatusCommand(dto.ReservationId, dto.NewStatusId));
        return result.ToActionResult();
    }
}