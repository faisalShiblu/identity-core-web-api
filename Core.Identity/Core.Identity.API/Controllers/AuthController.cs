using Core.Identity.API.Models;
using Core.Identity.API.Models.Authentication.SignUp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Core.Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        public AuthController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] Register register, string? role)
        {
            if (register is null)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new Response { Message = "Bad request", Status = "Error" });
            }

            var isEmailExist = await _userManager.FindByEmailAsync(register.Email);
            if (isEmailExist != null)
            {
                return StatusCode(StatusCodes.Status409Conflict,
                    new Response { Message = "Email already exists.", Status = "Error" });
            }

            IdentityUser user = new() { Email = register.Email, SecurityStamp = Guid.NewGuid().ToString(), UserName = register.Username };

            var result = await _userManager.CreateAsync(user, register.Password);
            if (!result.Succeeded)
            {
                var listOfError = new List<Response>
                {
                    new Response { Message = "Failed to create user.", Status = "Error" }
                };

                foreach (var error in result.Errors)
                {
                    var response = new Response() { Message = error.Description, Status = error.Code };
                    listOfError.Add(response);
                }

                return StatusCode(StatusCodes.Status500InternalServerError,
                    listOfError
                  );
            }
            else
            {
                return StatusCode(StatusCodes.Status201Created,
                    new Response { Message = "User created successfully.", Status = "Success" });
            }
        }
    }
}
