using AspNetCore_SmartPay_Web_API.DTO.Accounts;
using AspNetCore_SmartPay_Web_API.Models;
using AspNetCore_SmartPay_Web_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;

namespace AspNetCore_SmartPay_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly JwtServices _jwtServices;
        private readonly SignInManager<User> _SignInManager;
        private readonly UserManager<User> _userManager;



         public AccountController(JwtServices jwtServices, SignInManager<User> signManager, UserManager<User> userManager)
        {
            _jwtServices = jwtServices;
            _SignInManager = signManager;
            _userManager = userManager;
        }

        
         [Authorize]
         [HttpGet("refresh-UserToken")]
         public async Task<ActionResult<UserDto>> UserToken()
         {
             var user = await _userManager.FindByNameAsync(User.FindFirst(ClaimTypes.Email).Value);

               return CreateUserDTO(user);
         }

    
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>>Login( [FromBody] LoginDto model)
        {
            var user = await _userManager.FindByNameAsync(model.Email);
            
            
            if (user == null) return Unauthorized("Invaild username or password");

            if (user.EmailConfirmed == false) return Unauthorized("Please confirm your email");

            //later i will change this to true so that if the user enter an incorrect password for x3 I  lock it for security purpose 
            var  results = await _SignInManager.CheckPasswordSignInAsync(user, model.Password ,false);

            if (!results.Succeeded)
            {
                return Unauthorized("Invaild username or password");
            }


            return CreateUserDTO(user);
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            if(await CheckEmailExist(model.Email))
            {
                return BadRequest(new { message = $"The email address {model.Email} is already in use. Please try another email address." });
            }


            var newUser = new User
            {


                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Email,
                Email = model.Email,
                EmailConfirmed = true,
                CreatedDate = DateTime.UtcNow,

            };


            var result = await _userManager.CreateAsync(newUser,model.Password);

            if (!result.Succeeded)
            {
                // Return errors in a JSON format
                return BadRequest(new { message = "Account creation failed", errors = result.Errors.Select(e => e.Description) });
            }

            // Return a success response with a proper JSON format
            return Ok(new { message = "You successfully created an account." });
        }







        [HttpGet("CheckEmail")]
        public async Task<bool> CheckEmailExist(string email)
        {
            return await _userManager.Users.AnyAsync(x => x.Email == email);
        }



        #region Private Helper method

        private UserDto CreateUserDTO(User user)
        {
            return new UserDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Jwt = _jwtServices.CreateJwt(user)

            };
        }

       
        #endregion
    }
}
