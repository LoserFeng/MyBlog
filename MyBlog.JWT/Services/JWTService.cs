using Microsoft.IdentityModel.Tokens;
using MyBlog.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyBlog.JWT.Services
{
    public class JWTService
    {


        public string authorize(WriterInfo writer)
        {

            var claims = new Claim[]        //类似于身份证这样的东西。。。。
                {
                new Claim(ClaimTypes.Name, writer.Name),   //type   value
                new Claim("Id", writer.Id.ToString()),
                new Claim("UserName", writer.UserName)
                //不能放敏感信息
                };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SDMC-CJAS1-SAD-DFSFA-SADHJVF-VF"));//WebApi的密钥一样
            //issuer代表颁发Token的Web应用程序，audience是Token的受理者
            var token = new JwtSecurityToken(
                issuer: "http://localhost:6060",  //代表了颁发Token的Web应用程序地址
                audience: "http://localhost:5000", //audience是Token的受理者
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddHours(1),   //1h后过期
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );
            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return jwtToken;   //返回一个JWT字符串

        }
    }
}
