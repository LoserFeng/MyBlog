using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyBlog.IRepository;
using MyBlog.IService;
using MyBlog.Model;
using MyBlog.Model.Config;
using MyBlog.Model.ViewModels.Auth;

namespace MyBlog.Service;

public class AuthService {
    private readonly SecuritySettings _securitySettings;
    private readonly IUserInfoRepository _userInfoRepository;

    public AuthService(IOptions<SecuritySettings> options, IUserInfoRepository userRepo) {
        _securitySettings = options.Value;
        _userInfoRepository = userRepo;
    }

    public LoginToken GenerateLoginToken(UserInfo user) {
        var claims = new List<Claim> {
                new Claim("Id",user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),   //type   value
                
                new Claim("UserName", user.UserName)
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_securitySettings.Token.Key));
        var signCredential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var jwtToken = new JwtSecurityToken(
            issuer: _securitySettings.Token.Issuer,
            audience: _securitySettings.Token.Audience,
            claims: claims,
            expires: DateTime.Now.AddDays(7),
            signingCredentials: signCredential);

        return new LoginToken {
            Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
            Expiration = TimeZoneInfo.ConvertTimeFromUtc(jwtToken.ValidTo, TimeZoneInfo.Local)
        };
    }

    public async Task<UserInfo>? GetUserById(int userId) {
        UserInfo user=await _userInfoRepository.FindByIdAsync(userId);
        return user;
    }







}