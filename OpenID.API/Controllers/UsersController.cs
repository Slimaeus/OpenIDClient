using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OpenID.API.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UsersController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(User.Claims.Select(x => new { x.Type, x.Value }).ToList());
    }

    class ApplicationUser
    {

    }
}
