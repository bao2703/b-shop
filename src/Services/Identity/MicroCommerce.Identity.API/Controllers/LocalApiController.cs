﻿using IdentityServer4;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MicroCommerce.Identity.API.Controllers
{
    [Authorize(IdentityServerConstants.LocalApi.PolicyName)]
    [Route("localApi")]
    public class LocalApiController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hello");
        }
    }
}
