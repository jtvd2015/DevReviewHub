using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Mvc;

namespace DevReviewHub.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpGet("login")]
        public IActionResult Login()
        {
            var redirectUrl = Url.Action("GitHubResponse", "Auth");
            return Challenge(new AuthenticationProperties { RedirectUri = redirectUrl }, "GitHub");
        }

        [HttpGet("GitHubResponse")]
        public IActionResult GitHubResponse()
        {
            // At this point, the user is authenticated.
            // You can access user info via User.Claims
            return Ok(new
            {
                message = "GitHub authentication successful!",
                user = User.Identity.Name,
                claims = User.Claims.Select(c => new { c.Type, c.Value })
            });
        }

        [HttpGet("logout")]
        public async Task<IActionResult> logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok(new { message = "Logged out" });
        }
    }
}
