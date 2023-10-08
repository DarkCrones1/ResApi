using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Res.Common.Responses;

[DefaultStatusCode(500)]
public class InternalServerErrorObjectResult : ObjectResult
{
    public InternalServerErrorObjectResult(object value) : base(value)
    {
        //StatusCode = 500;
    }
}