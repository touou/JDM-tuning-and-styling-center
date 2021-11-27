using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using JDMTune.DataRepo;
using JDMTune.Models;
using JDMTune.Options;
using JDMTune.Requests;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using static JDMTune.Security.PassHashing;


namespace JDMTune.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IOptions<AuthOptions> _authOptions;
        private readonly DataRepository _data;

        public AuthController(IOptions<AuthOptions> options, DataRepository data)
        {
            _authOptions = options;
            _data = data;
        }

        [Route("registration")]
        [HttpPost]
        public IActionResult Registration([FromBody] RegRequest request)
        {
            if (!ModelState.IsValid)
            {
                //returns some view which will added later
            }
            //try to Authenticate User
            var pass = HashPassword(request.Password);
            var user = AuthenticateUser(request.Email, pass);
            if (user != null)
            {
                return Ok("this account exists");
            }
            
            user = new User()
            {
                Id = Guid.NewGuid(),
                Email = request.Email,
                Password = HashPassword(request.Password),
                Role = "User",
            };
            
            _data.AccountCreate(user);

            return Ok("zaebic' ti zaregan muzhik");
        }


        [Route("login")]
        [HttpPost]
        public IActionResult Login([FromBody] AuthRequest request)
        {
            
            var user = AuthenticateUser(request.Email, request.Password);
            if (user != null)
            {
                var token = JwtGenerate(user);
                return Ok(new
                {
                    access_token=token,
                    request.Email,
                    request.Password,
                });
            }

            return Unauthorized();
        }


        public User AuthenticateUser(string email, string password)
        {
            return _data.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
        }

        private string JwtGenerate(User user)
        {
            var authParams = _authOptions.Value;

            var securityKey = authParams.GetSymmetricSecurityKey();

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub,user.Password),
                
            };
            
            var token = new JwtSecurityToken(
                authParams.Issuer,
                authParams.Audience,
                claims,
                expires: DateTime.Now.AddSeconds(authParams.TokenLifeTime),
                signingCredentials: credentials
            );
            
            return new JwtSecurityTokenHandler().WriteToken(token);
            
        }
    }
}