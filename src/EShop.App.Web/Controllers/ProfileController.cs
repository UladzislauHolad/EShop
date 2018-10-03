using AutoMapper;
using EShop.App.Web.Models.Angular.Profile;
using EShop.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.App.Web.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IAccountService _service;
        private readonly IMapper _mapper;
        public ProfileController(IAccountService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        /// <summary>
        /// Get profile by user id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Returns users profile information</response>
        /// <response code="204">Returns no content</response>
        [HttpGet("api/profiles/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(204)]
        public async Task<ProfileAngularViewModel> GetProfileAsync(string id)
        {
            var profile = _mapper.Map<ProfileAngularViewModel>(await _service.FindByIdAsync(id));
            profile.HasPassword = await _service.HasPasswordAsync(id);

            return profile;
        }
    }
}
