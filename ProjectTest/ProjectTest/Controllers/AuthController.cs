using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectTest.Business;
using ProjectTest.Data.VO;
using ProjectTest.Model;

namespace ProjectTest.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class AuthController : ControllerBase
    {
        private ILoginBusiness _loginBusiness;

        public AuthController(ILoginBusiness loginBusiness)
        {
            _loginBusiness = loginBusiness;
        }

        [HttpPost]
        [Route("signin")]
        public IActionResult Signin([FromBody] UserVO user)
        {
            if (user == null)
                return BadRequest("Invalid client request");

            var token = _loginBusiness.ValidateCredentials(user);

            if (token == null)
                return Unauthorized();

            return Ok(token);
        }

        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh([FromBody] TokenVO tokenVo)
        {
            if (tokenVo == null)
                return BadRequest("Invalid client request");

            var token = _loginBusiness.ValidateCredentials(tokenVo);

            if (token == null)
                return BadRequest("Invalid client request");

            return Ok(token);
        }

        [HttpGet]
        [Route("revoke")]
        [Authorize("Bearer")]
        public IActionResult Revoke([FromBody] TokenVO tokenVo)
        {
            var username = User.Identity.Name;
            var result = _loginBusiness.RevokeToken(username);

            if(!result)
                return BadRequest("Invalid client request");

            return NoContent();
        }
    }
}
