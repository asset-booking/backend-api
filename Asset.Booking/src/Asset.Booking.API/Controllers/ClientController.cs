// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Asset.Booking.API.Controllers;

using Application.Clients.Commands;
using Application.Clients.Commands.Dto;
using Application.Clients.Queries;
using Application.Clients.Queries.ViewModels;
using Asset.Booking.Application.Reservations.Commands.Dto;
using Asset.Booking.Application.Reservations.Commands;
using Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class ClientController : ControllerBase
{
    private readonly IMediator _mediator;

    public ClientController(IMediator mediator) => _mediator = mediator;

    [HttpGet("GetByCompanyNameWithReservations")]
    public async Task<ActionResult<IEnumerable<ClientSearchResultViewModel>>> GetWithReservationData(string companyName)
    {
        var result = await _mediator.Send(new GetClientsByCompanyNameForSearchQuery(companyName));
        return result.ToActionResult();
    }


    [HttpGet("GetByCompanyName")]
    public async Task<ActionResult<IEnumerable<ClientViewModel>>> Get(string companyName)
    {
        var result = await _mediator.Send(new GetClientsByCompanyNameQuery(companyName));
        return result.ToActionResult();
    }

    [HttpPost("Register")]
    public async Task<ActionResult> PostAsync([FromBody] RegisterClientDto client)
    {
        var newClientId = Guid.NewGuid();
        var result = await _mediator.Send(new RegisterClientCommand(newClientId, client));
        return result.ToActionResult();
    }
}
