using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using MyBlog.IRepository;
using MyBlog.IService;
using MyBlog.JWT.Services;
using MyBlog.Model;
using MyBlog.Utility._MD5;


namespace MyBlog.JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {

        private readonly IWriterInfoService _writerInfoService;

        private readonly JWTService _JWTService;

        public AuthorizeController(IWriterInfoService writerInfoService)
        {
            _writerInfoService = writerInfoService;
            _JWTService = new JWTService();
        }

        [HttpPost("Login")]
        public async Task<ApiResult>Login(string username,string password)
        {
            //数据校验
            string userpwd = MD5Helper.MD5Encrypt32(password.ToString());
            var writer=await _writerInfoService.FindAsync(usr => usr.UserName == username && usr.UserPwd == userpwd);
            String writerToken ;
            if (writer == null)
            {
                return ApiResultHelper.Error("登录失败，账号或密码错误");
            }
            else
            {
                //登录成功
                writerToken = _JWTService.authorize(writer);
            }


            return ApiResultHelper.Success(writerToken);

        }
    }
}
