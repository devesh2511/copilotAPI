using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authentication.OAuth;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using API1.Services;
using System.Xml.Linq;
using Microsoft.AspNetCore.Identity;
using API1.Models;
using API1.DTOs;

namespace API1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserService userService;

        public AuthController(IConfiguration configuration , UserService userService)
        {
            _configuration = configuration;
            this.userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {
            System.Diagnostics.Debug.WriteLine(request.Username);
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User
            {
                Username = request.Username,
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            await this.userService.CreateAsync(user);

            return Ok(user);

        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDto request)
        {
            var userFound = (await this.userService.GetAsync(request.Username));
            if (userFound.Username != request.Username)
            {
                return BadRequest("!User not found");
            }

            if(!VerifyPasswordHash(request.Password, userFound.PasswordHash, userFound.PasswordSalt))
            {
                return BadRequest("Wrong Password");
            }


            string token = CreateToken(userFound);

            var message = new MessageDto
            {
                message = token
            };
            return Ok(message);

        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
       
    }
}
