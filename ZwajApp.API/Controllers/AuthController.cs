
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using ZwajApp.API.Data;
using ZwajApp.API.Domain;
using ZwajApp.API.DTOs;


namespace ZwajApp.API.Controllers
{
    [ApiController]
    [Route("api/{controller}")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        private readonly ILogger<AuthController> _loggger;

        public AuthController(IAuthRepository repo, IConfiguration config,
        ILogger<AuthController> loggger)
        {
            _config = config;
            _loggger = loggger;
            _repo = repo;

        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDTO useforregister)
        {
            useforregister.Username = useforregister.Username.ToLower();
            if (await _repo.UserExist(useforregister.Username))
                return BadRequest("User Already taken");
            var creatingUser = new User
            {
                UserName = useforregister.Username
            };
            var createdUser = await _repo.Register(creatingUser, useforregister.Password);

            return StatusCode(201);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDTO loginDto)
        {
            //throw new  Exception("Custom Exceptios");
            // try
            // {
                var userExist = await _repo.Login(loginDto.Username.ToLower(), loginDto.Password);
                if (userExist == null)
                    return Unauthorized();
                var claims = new[]{
                    new Claim(ClaimTypes.NameIdentifier,userExist.Id.ToString()),
                    new Claim(ClaimTypes.Name,userExist.UserName)
                };
                 _loggger.LogInformation( _config.GetSection("AppSettings:Token").Value);

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
                _loggger.LogInformation(key.ToString());
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
                var tokenDiscriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = creds
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDiscriptor);
                return Ok(new
                {
                    token = tokenHandler.WriteToken(token)
                });

            // }
            // catch (System.Exception ex)
            // {

            //     _loggger.LogInformation(ex.Message);
            //     return BadRequest();
            // }

        }
    }
}