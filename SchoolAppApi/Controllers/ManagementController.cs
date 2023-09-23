using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolAppApi.Data.Helpers;

namespace SchoolAppApi.Controllers
{
    [Authorize(Roles = UserRoles.Manager)]

    [Route("api/[controller]")]
    [ApiController]
    public class ManagementController : ControllerBase
    {
        [HttpGet]
        public IActionResult get()
        {
            return Ok("Hello Manager");
        }
    }
}
