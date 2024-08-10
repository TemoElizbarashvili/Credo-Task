using Microsoft.AspNetCore.Mvc;

namespace Credo.API.Helpers;

public class CustomForbidResult : ObjectResult
{
    public CustomForbidResult(string message)
        : base(new { Message = message })
    {
        StatusCode = StatusCodes.Status403Forbidden;
    }
}