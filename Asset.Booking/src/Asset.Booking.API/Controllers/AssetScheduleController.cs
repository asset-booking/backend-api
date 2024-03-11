// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Asset.Booking.API.Controllers;

using Application.AssetSchedules.Queries;
using Asset.Booking.Application.AssetSchedules.Queries.ViewModels;
using Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

[Route("api/[controller]")]
[ApiController]
public class AssetScheduleController(IMediator mediator) : ControllerBase
{
    [HttpGet("GetForAllAssets")]
    public async Task<ActionResult<IEnumerable<AssetWithScheduleViewModel>>> GetAllAsync(
        [FromQuery] DateTime start,
        [FromQuery] DateTime end)
    {
        var dateRange = new DateRange(start, end);
        var result = await mediator.Send(new GetAssetsWithSchedulesQuery(dateRange));

        return result.ToActionResult();
    }

    [HttpGet("GetById")]
    public async Task<ActionResult<AssetWithScheduleViewModel>> GetAsync(
        int id,
        [FromQuery] DateTime start,
        [FromQuery] DateTime end)
    {
        var dateRange = new DateRange(start, end);
        var result = await mediator.Send(new GetAssetScheduleQuery(id, dateRange));

        return result.ToActionResult();
    }

    [HttpGet("GetByAssetId")]
    public async Task<ActionResult<AssetWithScheduleViewModel>> GetByAssetIdAsync(
        int assetId,
        [FromQuery] DateTime start,
        [FromQuery] DateTime end)
    {
        var dateRange = new DateRange(start, end);
        var result = await mediator.Send(new GetAssetScheduleByAssetIdQuery(assetId, dateRange));

        return result.ToActionResult();
    }

    [HttpGet("GetByAssetIds")]
    public async Task<ActionResult<IEnumerable<AssetWithScheduleViewModel>>> GetByAssetIdsAsync(
        [FromQuery] IEnumerable<int> assetIds,
        [FromQuery] DateTime start,
        [FromQuery] DateTime end)
    {
        var dateRange = new DateRange(start, end);
        var result = await mediator.Send(new GetAssetScheduleByAssetIdsQuery(assetIds, dateRange));

        return result.ToActionResult();
    }
}
