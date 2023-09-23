using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolAppApi.Data.Helpers;

namespace SchoolAppApi.Controllers
{
    [Authorize(Roles = UserRoles.Student)]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [HttpGet]
        public IActionResult get()
        {
            return Ok("Hello Student");
        }
    }
}
