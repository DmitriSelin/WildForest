using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WildForest.Api.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ExceptionsController : ControllerBase
    {
        [Route("error")]
        public IActionResult Error()
        {
            Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

            switch (exception)
            {
                case UserException userException:
                    return Problem(title: userException.MessageForUser, statusCode: userException.StatusCode);
                default:
                    return Problem(title: exception?.Message, statusCode: (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}