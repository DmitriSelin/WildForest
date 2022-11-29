using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace WildForest.Api.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public sealed class ExceptionsController : ControllerBase
    {
        [Route("error")]
        public IActionResult Error()
        {
            return Problem();
        }
    }
}