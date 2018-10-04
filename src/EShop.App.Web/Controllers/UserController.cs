using AutoMapper;
using EShop.App.Web.Models.Angular.User;
using EShop.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.App.Web.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IAccountService _service;
        private readonly IMapper _mapper;

        public UserController(IAccountService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        /// <summary>
        /// Get list of users
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns list of users</response>
        /// <response code="401">Unauthorized user request</response>
        [HttpGet("api/users")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<IEnumerable<UserInfoAngularViewModel>> GetUsers()
        {
            return _mapper.Map<IEnumerable<UserInfoAngularViewModel>>(await _service.GetUsersAsync());
        }

        /// <summary>
        /// Delete user by username
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        /// <response code="200">User was deleted</response>
        /// <response code="401">Unauthorized user request</response>
        /// <response code="422">User with such name is not found</response>
        [HttpDelete("api/users/{userName}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(422)]
        [ProducesResponseType(401)]
        public async Task<ActionResult> DeleteUser([FromRoute]string userName)
        {
            var result = await _service.DeleteUserByNameAsync(userName);
            if(result != null)
            {
               return StatusCode(StatusCodes.Status200OK);
            }

            return StatusCode(StatusCodes.Status422UnprocessableEntity);
        }
    }
}
