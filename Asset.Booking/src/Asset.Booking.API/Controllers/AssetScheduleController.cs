namespace Asset.Booking.API.Controllers;

using Application.AssetSchedules.Queries;
using Application.AssetSchedules.Queries.ViewModels;
using Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

[Route("api/[controller]")]
[ApiController]
public class AssetScheduleController(IMediator mediator) : ControllerBase
{
    [HttpGet("GetByAssetIds")]
    public async Task<ActionResult<IEnumerable<AssetWithScheduleViewModel>>> GetByAssetIdsAsync(
        [FromQuery] IEnumerable<int> assetIds,
        DateTime start,
        DateTime end)
    {
        DateRange dateRange = new DateRange(start, end);
        Result<IEnumerable<AssetWithScheduleViewModel>> result =
            await mediator.Send(new GetAssetScheduleByAssetIdsQuery(assetIds, dateRange));

        return result.ToActionResult();
    }
}