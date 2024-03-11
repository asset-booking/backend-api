namespace Asset.Booking.API.Extensions;

using Microsoft.AspNetCore.Mvc;
using SharedKernel;

public static class ResultExtensions
{
    public static ActionResult<T> ToActionResult<T>(this Result<T> result)
    {
        if (result.IsSuccess)
            return new OkObjectResult(result.Value);

        if (result.Error.Code.Equals(GenericErrors.EntityNotFoundCode))
            return new NotFoundObjectResult(result.Error);

        return new BadRequestObjectResult(result.Error);
    }

    public static ActionResult ToActionResult(this Result result)
    {
        if (result.IsSuccess)
            return new OkResult();

        if (result.Error.Code.Equals(GenericErrors.EntityNotFoundCode))
            return new NotFoundObjectResult(result.Error);

        return new BadRequestObjectResult(result.Error);
    }
}
