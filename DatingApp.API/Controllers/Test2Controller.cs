using System;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace DatingApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Test2Controller : ControllerBase
    {


        [HttpPost("register")]
        public IActionResult Register2(UserForRegisterDto userForRegisterDto)
        {
            //validate the request in DTO

            return StatusCode(201);//trick
        }

        [HttpPost("login")]
        public IActionResult Login2(UserForLoginDto userForLoginDto)
        {
            return StatusCode(201);//trick
        }
    }
}