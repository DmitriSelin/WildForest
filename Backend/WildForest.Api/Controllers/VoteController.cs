using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WildForest.Api.Controllers;

[Authorize(Roles = "User, Admin")]
[Route("api/weather/votes")]
public sealed class VoteController : ApiController
{
    [HttpPost]
    public async Task<IActionResult> Vote()
    {
        return Ok();
    }
}