namespace Asset.Booking.API.Controllers;

using Application.Clients.Commands;
using Application.Clients.Commands.Dto;
using Application.Clients.Queries;
using Application.Clients.Queries.ViewModels;
using Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

[Route("api/[controller]")]
[ApiController]
public class ClientController(IMediator mediator) : ControllerBase
{
    [HttpGet("GetByCompanyNameWithReservations")]
    public async Task<ActionResult<IEnumerable<ClientSearchResultViewModel>>> GetWithReservationData(string companyName)
    {
        Result<IEnumerable<ClientSearchResultViewModel>> result =
            await mediator.Send(new GetClientsByCompanyNameForSearchQuery(companyName));
        return result.ToActionResult();
    }

    [HttpGet("GetByCompanyName")]
    public async Task<ActionResult<IEnumerable<ClientViewModel>>> Get(string companyName)
    {
        Result<IEnumerable<ClientViewModel>>
            result = await mediator.Send(new GetClientsByCompanyNameQuery(companyName));
        return result.ToActionResult();
    }

    [HttpPost("Register")]
    public async Task<ActionResult<Guid>> PostAsync([FromBody] RegisterClientDto client)
    {
        Guid newClientId = Guid.NewGuid();
        Result result = await mediator.Send(new RegisterClientCommand(newClientId, client));
        if (result.IsSuccess)
        {
            return Result<Guid>.Success(newClientId).ToActionResult();
        }

        return result.ToActionResult();
    }
}