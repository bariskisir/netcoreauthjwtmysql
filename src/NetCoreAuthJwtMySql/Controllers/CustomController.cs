using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace NetCoreAuthJwtMySql.Controllers
{
    [ApiController]
    [Route("api/Custom/")]
    public class CustomController : ControllerBase
    {
        [Authorize]
        [HttpGet]
        [Route("Authorize")]
        public ActionResult<string> Authorize()
        {
            var userEmail = User.Identity.Name;
            return string.Format("Hello {0}",userEmail);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        [Route("AuthorizeAdmin")]
        public ActionResult<string> AuthorizeAdmin()
        {
            var userEmail = User.Identity.Name;
            return string.Format("Hello {0}", userEmail);
        }

        [Authorize(Roles = "USER")]
        [HttpGet]
        [Route("AuthorizeUser")]
        public ActionResult<string> AuthorizeUser()
        {
            var userEmail = User.Identity.Name;
            return string.Format("Hello {0}", userEmail);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("AllowAnonymous")]
        public ActionResult<string> AllowAnonymous()
        {
            return "Hello anonymous";
        }
    }
}