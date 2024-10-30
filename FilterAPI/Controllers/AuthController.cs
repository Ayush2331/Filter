using FilterAPI.Models.Requests;
using FilterAPI.Models.Responses;
using FilterAPI.Repositories.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FilterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(
            UserManager<IdentityUser> userManager,
            ITokenRepository tokenRepository
        )
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequest.Username,
                Email = registerRequest.Username
            };

            var identityResult = await userManager.CreateAsync(
                identityUser,
                registerRequest.Password
            );

            if (identityResult.Succeeded)
            {
                if (registerRequest.Roles != null && registerRequest.Roles.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(
                        identityUser,
                        registerRequest.Roles
                    );

                    if (identityResult.Succeeded)
                    {
                        return Ok("User was registered! Please login.");
                    }
                }
            }

            return BadRequest("Something went wrong");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var user = await userManager.FindByEmailAsync(loginRequest.Username);

            if (user != null)
            {
                var checkPasswordResult = await userManager.CheckPasswordAsync(
                    user,
                    loginRequest.Password
                );

                if (checkPasswordResult)
                {
                    var roles = await userManager.GetRolesAsync(user);

                    if (roles != null)
                    {
                        var jwtToken = tokenRepository.CreateJWTToken(user, roles.ToList());

                        var response = new LoginResponse { JwtToken = jwtToken };

                        return Ok(response);
                    }
                }
            }

            return BadRequest("Username or password incorrect");
        }
    }
}
