﻿using EncDecService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EncDecService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IJwtTokenManager _tokenManager;
        public TokenController(IJwtTokenManager jwtTokenManager)
        {
            _tokenManager = jwtTokenManager;
        }

        [AllowAnonymous] //yetkili olmayanda kullanabilir yoksa 
        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody] UserCredential credential)
        {
            var token = _tokenManager.Authenticate(credential.UserName, credential.Password);
            if (string.IsNullOrEmpty(token))
                return Unauthorized();

            return Ok(token);
        }
    }
}
