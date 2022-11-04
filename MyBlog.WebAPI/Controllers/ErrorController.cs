using Microsoft.AspNetCore.Mvc;
using MyBlog.IService;
using MyBlog.Model.ViewModels.Error;
using MyBlog.Model;

namespace MyBlog.WebAPI.Controllers
{
    //Route只是一个防火墙 
    [Route("[controller]/[action]")]
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> _logger;
        private readonly IUserInfoService _userInfoService;


        public ErrorController(IUserInfoService userInfoService, ILogger<ErrorController> logger)
        {
            _userInfoService = userInfoService;
            _logger = logger;
        }


        //~/表示的就是localhost:ip 和/没啥区别






        [Route("~/Error/Index")]

        public IActionResult Index()
        {
            return View();
        }


    }
}