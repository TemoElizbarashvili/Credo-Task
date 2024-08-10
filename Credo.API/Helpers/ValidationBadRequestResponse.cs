using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace Credo.API.Helpers;

public class ValidationBadRequestResponse : ObjectResult
{
    public ValidationBadRequestResponse(ValidationResult? validationResult, ILogger logger) : base(validationResult)
    {
        logger.LogError("Errors Occurred while processing request. Errors: {@Error}", validationResult?.ToDictionary());
        StatusCode = StatusCodes.Status400BadRequest;
        Value = validationResult?.ToDictionary();
    }
}
