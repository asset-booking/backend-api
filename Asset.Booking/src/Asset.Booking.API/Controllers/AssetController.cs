namespace Asset.Booking.API.Controllers;

using Application.Assets.Queries;
using Application.Assets.Queries.ViewModels;
using Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

[ApiController]
[Route("api/[controller]")]
public class AssetController(IMediator mediator) : ControllerBase
{
    [HttpGet("GetAll")]
    public async Task<ActionResult<IEnumerable<AssetViewModel>>> Get()
    {
        Result<IEnumerable<AssetViewModel>> result = await mediator.Send(new GetAssetsQuery());
        return result.ToActionResult();
    }
}