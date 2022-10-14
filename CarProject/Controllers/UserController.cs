﻿using AutoMapper;
using CarProject.Data;
using CarProject.Models;
using CarProject.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.IO;
using System.Linq;

using Tazeez.Common.Extensions;
using CarProject.Core.Mangers.Interfaces;
using Microsoft.Extensions.Logging;

namespace CarProject.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class UserController : ApiBaseController
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IUserManager _userManager;

        public UserController(ILogger<WeatherForecastController> logger,IUserManager userManager )
        {
            _logger = logger;
            _userManager = userManager;
        }

        [HttpPost]
        public IActionResult Rigester([FromBody] UserRegisterViewModel vm)
        {
            var res = _userManager.Rigester(vm);
            return Ok(res);
        }

        [HttpPost]
        public IActionResult Login([FromBody] UserLoginViewModel vm)
        {
            var res = _userManager.Login(vm);
            return Ok(res);

        }

        [HttpPut]
        public IActionResult UpdateMyProfile([FromBody] UserModelViewModel vm)
        {
            var user = _userManager.UpdateProfile(LoggedInUser , vm);
            return Ok(user);
        }


        

        [HttpGet]
        public IActionResult Retrive(string filename)
        {
            var folderPath = Directory.GetCurrentDirectory();
            folderPath = $@"{folderPath}\{filename}";
            var byteArray = System.IO.File.ReadAllBytes(folderPath);
            return File(byteArray, "image/jpeg", filename);
        }
       
    }
}
