using System;

using Microsoft.AspNetCore.Mvc;

using TableTracker.Application.CQRS;
using TableTracker.Domain.Enums;

namespace TableTracker.Helpers
{
    public static class ReturnResultHelper
    {
        public static IActionResult ReturnCommandResult<T>(CommandResponse<T> response) where T : class
        {
            return response.Result switch
            {
                CommandResult.Success => new OkObjectResult(response),
                CommandResult.Failure => new BadRequestObjectResult(new { Object = response, response.ErrorMessage }),
                CommandResult.NotFound => new NotFoundObjectResult(new { Object = response, response.ErrorMessage }),
                _ => throw new NotSupportedException(),
            };
        }

        public static IActionResult ReturnCommandResult(CommandResponse response)
        {
            return response.Result switch
            {
                CommandResult.Success => new OkObjectResult(response),
                CommandResult.Failure => new BadRequestObjectResult(new { Object = response, response.ErrorMessage }),
                CommandResult.NotFound => new NotFoundObjectResult(new { Object = response, response.ErrorMessage }),
                _ => throw new NotSupportedException(),
            };
        }

        public static IActionResult ReturnQueryResult<T>(T response) where T : class
        {
            return response switch
            {
                null => new NotFoundObjectResult(new { Object = response, ErrorMessage = "Object could not be found" }),
                _ => new OkObjectResult(response),
            };
        }

        public static IActionResult ReturnQueryResult<T>(T[] response) where T : class
        {
            return response.Length switch
            {
                0 => new NotFoundObjectResult(new { Object = response, ErrorMessage = "Objects could not be found" }),
                _ => new OkObjectResult(response),
            };
        }
    }
}
