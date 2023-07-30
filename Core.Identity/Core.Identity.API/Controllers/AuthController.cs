using Core.Identity.API.Models;
using Core.Identity.API.Models.Authentication.LogIn;
using Core.Identity.API.Models.Authentication.SignUp;
using Core.Identity.Service.Models;
using Core.Identity.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using NETCore.MailKit.Core;

namespace Core.Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailServices _emailService;
        public AuthController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IEmailServices emailService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] Register register, string role)
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


            if (await _roleManager.RoleExistsAsync(role.Trim()))
            {
                var result = await _userManager.CreateAsync(user, register.Password);
                if (!result.Succeeded)
                {
                    var listOfError = new List<Response> { new Response { Message = "Failed to create user.", Status = "Error" } };
                    foreach (var error in result.Errors)
                    {
                        var response = new Response() { Message = error.Description, Status = error.Code };
                        listOfError.Add(response);
                    }
                    return StatusCode(StatusCodes.Status500InternalServerError, listOfError);
                }

                await _userManager.AddToRoleAsync(user, role.Trim());

                return StatusCode(StatusCodes.Status201Created,
                    new Response { Message = "User created successfully.", Status = "Success" });
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound,
                      new Response { Message = "Role does not exist.", Status = "Error" });
            }
        }

        [HttpGet]
        public IActionResult SendEmail()
        {
            var message = new Message(new string[] { "faisalalamshiblu@gmail.com" }, "Hi", "Content");
            _emailService.SendEmail(message);
            return StatusCode(StatusCodes.Status201Created,
                   new Response { Message = "User created successfully.", Status = "Success" });
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LogIn([FromBody] LogIn user)
        {
            if (user is null)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new Response { Message = "Bad request", Status = "Error" });
            }

            var loggedInUser = _userManager.FindByNameAsync(user.Username);
            //if(loggedInUser != null && await _userManager.CheckPasswordAsync(loggedInUser, user.Password))
            //{
            //}

            return StatusCode(StatusCodes.Status200OK,
                   new Response { Message = "User created successfully.", Status = "Success" });
        }
    }
}
