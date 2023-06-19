using Microsoft.AspNetCore.Mvc;
using MyBlog.IService;
using MyBlog.Model.ViewModels.Society;

namespace MyBlog.WebAPI.Controllers
{

    [Route("[controller]/[action]")]
    public class DiaryController : Controller
    {

        private readonly IUserInfoService _userInfoService;

        public DiaryController(IUserInfoService userInfoService)
        {
            _userInfoService= userInfoService;
        }

        [Route("~/Diary")]
        [Route("~/Diary/Index")]
        public  IActionResult Index()
        {
            
            return View();
        }





    }
}
