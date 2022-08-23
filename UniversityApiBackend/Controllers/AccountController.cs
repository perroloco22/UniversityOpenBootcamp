﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniversityApiBackend.Helpers;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;

        public AccountController(JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;

        }

        //Example users
        //TODO: change by real users in DB
        private IEnumerable<User> Logins = new List<User>()
        {
            new User()
            {
                Id=1,
                Email="martin@imaginagroup.com",
                Name="Admin",
                Password="Admin"
            },
            new User()
            {
                Id=2,
                Email="pepe@imaginagroup.com",
                Name="User 1",
                Password="pepe"
            }
        };

        [HttpPost]
        public IActionResult GetToken(UserLogins userLogins)
        {
            try
            {
                var Token = new UserTokens();
                var valid = Logins.Any(user => user.Name.Equals(userLogins.UserName, StringComparison.OrdinalIgnoreCase));
                if (valid)
                {
                    var user = Logins.FirstOrDefault(user => user.Name.Equals(userLogins.UserName, StringComparison.OrdinalIgnoreCase));
                    Token = JwtHelpers.GenTokenKey(new UserTokens()
                    {
                        UserName = user.Name,
                        EmailId = user.Email,
                        Id = user.Id,
                        GuidId = Guid.NewGuid(),


                    },_jwtSettings);

                }
                else
                {
                    return BadRequest("Wrong Password");
                }
                return Ok(Token);

            }
            catch (Exception ex)
            {

                throw new Exception("GetToken Error", ex);
            }
        }

        [HttpGet(Name = "GetUsersList")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admninistrator")]
        public IActionResult GetUsersList()
        {
            return Ok(Logins);
        }


    }
}
