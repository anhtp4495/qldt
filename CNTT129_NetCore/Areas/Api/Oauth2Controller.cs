using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CNTT129_NetCore.Attributes;
using CNTT129_NetCore.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;
using CNTT129_NetCore.Models.Api;

namespace CNTT129_NetCore.Areas.Api
{
    [ApiRoute()]
    [ApiController]
    public class Oauth2Controller : ControllerBase
    {
        private IConfiguration _configuration;

        public Oauth2Controller(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        // POST: <api>/<version>/oauth2/token
        [Route("token")]
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Token([FromBody] UserLoginModel user)
        {
            GIANG_VIEN gv = new GIANG_VIEN();
            var kq = gv.login(user.UserName, user.Password);
            if (kq.Count == 0)
            {
                return Unauthorized(JsonSerializer.Serialize<dynamic>(new
                {
                    error_message = "Tài Khoản bạn khóa hoặc sai mật mật khẩu! Vui lòng liên hệ admin để đc giải quyết"
                }));
            }

            string accessToken = GenerateAccessToken(kq[0]);
            return Ok(JsonSerializer.Serialize<dynamic>(new
            {
                access_token = accessToken
            }));
        }

        private string GenerateAccessToken(GIANG_VIEN gvModel)
        {
            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(this._configuration["Jwt:Key"])
            );
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, gvModel.TENGV),
                new Claim(ClaimTypes.Email, gvModel.SDT),
                new Claim(ClaimTypes.GivenName, gvModel.TENGV),
                new Claim(ClaimTypes.Surname, gvModel.TENGV),
                new Claim(ClaimTypes.Role, gvModel.vai_tro_name)
            };

            var token = new JwtSecurityToken(
                this._configuration["Jwt:Issuer"],
                this._configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
