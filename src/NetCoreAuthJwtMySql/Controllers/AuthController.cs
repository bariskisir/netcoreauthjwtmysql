using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NetCoreAuthJwtMySql.Models.Db;
using NetCoreAuthJwtMySql.Models.Requests;
using NetCoreAuthJwtMySql.Models.Responses;
using NetCoreAuthJwtMySql.Utils;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
namespace NetCoreAuthJwtMySql.Controllers
{
    [Route("api/Auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public ActionResult<ResponseLogin> Login(RequestLogin requestLogin)
        {
            var responseLogin = new ResponseLogin();
            using (var db = new NetCoreAuthJwtMySqlContext())
            {
                var existingUser = db.User.SingleOrDefault(x => x.Email == requestLogin.Email);
                if (existingUser != null)
                {
                    var isPasswordVerified = CryptoUtil.VerifyPassword(requestLogin.Password, existingUser.Salt, existingUser.Password);
                    if (isPasswordVerified)
                    {
                        var claimList = new List<Claim>();
                        claimList.Add(new Claim(ClaimTypes.Name, existingUser.Email));
                        claimList.Add(new Claim(ClaimTypes.Role, existingUser.Role));
                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"]));
                        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var expireDate = DateTime.UtcNow.AddDays(1);
                        var timeStamp = DateUtil.ConvertToTimeStamp(expireDate);
                        var token = new JwtSecurityToken(
                            claims: claimList,
                            notBefore: DateTime.UtcNow,
                            expires: expireDate,
                            signingCredentials: creds);
                        responseLogin.Success = true;
                        responseLogin.Token = new JwtSecurityTokenHandler().WriteToken(token);
                        responseLogin.ExpireDate = timeStamp;
                    }
                    else
                    {
                        responseLogin.MessageList.Add("Password is wrong");
                    }
                }
                else
                {
                    responseLogin.MessageList.Add("Email is wrong");
                }
            }
            return responseLogin;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public ActionResult<ResponseRegister> Register(RequestRegister requestRegister)
        {
            var responseRegister = new ResponseRegister();
            using (var db = new NetCoreAuthJwtMySqlContext())
            {
                if (!db.User.Any(x => x.Email == requestRegister.Email))
                {
                    var email = requestRegister.Email;
                    var salt = CryptoUtil.GenerateSalt();
                    var password = requestRegister.Password;
                    var hashedPassword = CryptoUtil.HashMultiple(password, salt);
                    var user = new User();
                    user.Email = email;
                    user.Salt = salt;
                    user.Password = hashedPassword;
                    user.Role = "USER";
                    db.User.Add(user);
                    db.SaveChanges();
                    responseRegister.Success = true;
                }
                else
                {
                    responseRegister.MessageList.Add("Email is already in use");
                }
            }
            return responseRegister;
        }
    }
}