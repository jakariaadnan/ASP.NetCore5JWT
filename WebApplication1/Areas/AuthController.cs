using Auth.DBContext;
using Auth.Model;
using Auth.Services.JWT.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Areas
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtFactoryService _jwtFactory;
        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IJwtFactoryService jwtFactory)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtFactory = jwtFactory;
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LogIn([FromBody] LogInViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var obj = new ReturnObject();
            var user = await _userManager.FindByNameAsync(model.Name);
            obj.message = "Password Not Valid";
            if (user != null && (await _userManager.CheckPasswordAsync(user, model.Password)))
            {
                var roles = await _userManager.GetRolesAsync(user);
                
                var jwt = JsonConvert.SerializeObject(await _jwtFactory.GenerateToken(user.UserName,roles));

                obj = new ReturnObject
                {
                    jwt = jwt.Replace("\"", ""),
                    userInfo = user
                };

                return new OkObjectResult(obj);
            }
            else if (user == null)
            {
                obj.message = "User Not Found";
            }
            return BadRequest(obj);
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var obj = new ReturnObject();
            try
            {
                ApplicationUser user = await _userManager.FindByNameAsync(model.UserName);
                string name = string.Empty;

                if (user != null)
                {
                    obj = new ReturnObject
                    {
                        message = "User Exists",
                        userInfo = user
                    };
                    return new OkObjectResult(obj);
                }

                string returnResult = "Success";
                var jwt = new object();

                var newUser = new ApplicationUser
                {
                    UserName = model.UserName,
                    PhoneNumber ="0"+ model.PhoneNumber,
                    Email = model.Email
                };
                var result = await _userManager.CreateAsync(newUser, model.Password);
                if (result.Succeeded)
                {
                    var result2=await _userManager.AddToRoleAsync(newUser, model.Role);
                    if (result2.Succeeded)
                    {
                        var roles = await _userManager.GetRolesAsync(newUser);

                        jwt = JsonConvert.SerializeObject(await _jwtFactory.GenerateToken(newUser.UserName, roles));
                    }
                    else
                    {
                        returnResult = "error";
                        var delete = await _userManager.DeleteAsync(newUser);
                    }
                }
                else
                {
                    returnResult = "error";
                }

                obj = new ReturnObject
                {
                    jwt = jwt.ToString().Replace("\"", ""),
                    message = returnResult,
                    userInfo = user
                };


                return new OkObjectResult(obj);

            }
            catch (Exception ex)
            {
                ApplicationUser user = await _userManager.FindByNameAsync(model.UserName);
                var delete = await _userManager.DeleteAsync(user);
                obj = new ReturnObject
                {
                    message=ex.Message
                };
                return BadRequest(obj);
            }


        }
        [HttpGet]
        [Authorize(Roles = "General User")]
        public async Task<IActionResult> GetUser()
        {
            return Ok(User.Identity.Name);
        }
    }
}
