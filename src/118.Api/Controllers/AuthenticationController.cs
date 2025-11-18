using _118.Api.Services;
using Application.CRUD;
using Application.Dtos.Addresses;
using Application.Dtos.User;
using Application.Dtos.User.Register;
using Domain.Entities;
using Infrustructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;

namespace _118.Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserCrud _userCrud;
        private readonly JWTService _jWTService;

        public AuthenticationController(UserCrud userCrud,JWTService jWTService)
        {
            this._userCrud = userCrud;
            this._jWTService = jWTService;
        }
        [HttpPost]
        public ActionResult<RegisterResponceDto> Register([FromBody] RegisterDto dto) 
        {
            var user = new User
            {
                UserName = dto.UserName,
                PasswordHash = HashPass(dto.Password)
            };
            _userCrud.Add(user);
            return Ok (getRegisterDto(user));
        }

        private RegisterResponceDto getRegisterDto(User user)
        {
            return new RegisterResponceDto
            {
                id = user.Id,
                IsResponce = true,
            };
        }
        
        private string HashPass(string pass)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(pass);
            var hashBytes = sha256.ComputeHash(bytes);
            return Convert.ToHexString(hashBytes);
        }
        [HttpPost]
        public ActionResult<LoginResponceDto> Login([FromBody] LoginDto dto)
        {

            var user = _userCrud.GetUserByUserName(dto.UserName);

            if (user == null || user.PasswordHash != HashPass(dto.Password))
            {
                return Ok(new LoginResponceDto
                {
                    IsResponce = false,
                    ErorMessage = "User not Found or password is wrong"
                });
            }

            var result = getLoginDto(user);
            result.IsResponce = true;
            result.ErorMessage = "valid User";
            result.Token = _jWTService.GenerateToken(user);

            return Ok(result);
     
        }
        private LoginResponceDto getLoginDto(User user)
        {
            return new LoginResponceDto
            {
                id = user.Id,
                IsResponce = true,
            };
        }
    }
}
