using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EShop.App.Web.Models;
using EShop.App.Web.Models.Angular.Account;
using EShop.Services.DTO;
using EShop.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace EShop.App.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IAccountService _service;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AccountController(IAccountService service, IConfiguration configuration, IMapper mapper)
        {
            _service = service;
            _configuration = configuration;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result =
                await _service.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    // проверяем, принадлежит ли URL приложению
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Product");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
                
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            // удаляем аутентификационные куки
            await _service.SignOutAsync();
            return RedirectToAction("Index", "Product");
        }

        [HttpPost("api/login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(422)]
        [AllowAnonymous]
        public async Task<ActionResult<UserAngularViewModel>> JwtLogin([FromBody]LoginViewModel model)
        {
           
            var result =
            await _service.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);
            if (result.Succeeded)
            {
                var loggedUser = _mapper.Map<UserAngularViewModel>(await _service.FindByNameAsync(model.UserName));
                loggedUser.Token = await GenerateJwtToken(loggedUser);
                return loggedUser;
            }

            return StatusCode(StatusCodes.Status422UnprocessableEntity, new { Errors = new List<string>{ "Password or login is incorrect" } });
        }

        [HttpPost("api/register")]
        [ProducesResponseType(200)]
        [ProducesResponseType(422)]
        [AllowAnonymous]
        public async Task<object> JwtRegister([FromBody]CreateUserViewModel model)
        {
            UserAngularViewModel user = new UserAngularViewModel
            {
                UserName = model.UserName,
                Email = model.Email
            };
            var result = await _service.CreateUserAsync(_mapper.Map<UserDTO>(user), model.Password);

            if(result.Succeeded)
            {
                return StatusCode(StatusCodes.Status200OK);
            }

            return StatusCode(StatusCodes.Status422UnprocessableEntity, new { Errors = result.Errors.Select(e => e.Description) });
        }

        [HttpPost("api/logoff")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> JwtLogOff()
        {
            await _service.SignOutAsync();
            return Ok();
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

        private async Task<string> GenerateJwtToken(UserAngularViewModel user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
