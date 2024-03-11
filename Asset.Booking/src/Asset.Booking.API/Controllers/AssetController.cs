namespace Asset.Booking.API.Controllers;

using Application.Assets.Queries;
using Application.Assets.Queries.ViewModels;
using Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AssetController : ControllerBase
{
    private readonly IMediator _mediator;

    public AssetController(IMediator mediator) =>
        _mediator = mediator;

    [HttpGet("GetAll")]
    public async Task<ActionResult<IEnumerable<AssetViewModel>>> Get()
    {
        var result = await _mediator.Send(new GetAssetsQuery());
        return result.ToActionResult();
    }
}
