using Microsoft.AspNetCore.Mvc;
using MyBlog.IService;
using MyBlog.Model.ViewModels.Auth;
using MyBlog.Service;
using MyBlog.Utility._MD5;

namespace MyBlog.WebAPI.Controllers.Api
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthorizeController : ControllerBase
    {


        private readonly IUserInfoService _userInfoService;

        private readonly AuthService _authService;

        public AuthorizeController(IUserInfoService userInfoService,AuthService authService)
        {
            _userInfoService = userInfoService;
            _authService = authService;

        }

        [HttpPost("Login")]
        public async Task<ApiResponse> Login(string username, string password)
        {
            //数据校验
            string userpwd = MD5Helper.MD5Encrypt32(password.ToString());
            var user = await _userInfoService.FindAsync(usr => usr.UserName == username && usr.UserPwd == userpwd);
            LoginToken userToken;
            if (user == null)
            {
                return ApiResponse.Error(Response,"登录失败，账号或密码错误");
            }
            else
            {
                //登录成功
                userToken = _authService.GenerateLoginToken(user);
            }


            return ApiResponse.Ok(userToken);

        }
    }
}
