using Microsoft.AspNetCore.Mvc;
using MyBlog.IService;
using MyBlog.Model.ViewModels.CMS.UserInfo;
using MyBlog.Model.ViewModels.Common;

namespace MyBlog.WebAPI.Controllers
{





    [Route("[controller]/[action]")]
    public class CMSController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IUserInfoService _userInfoService;


        public CMSController(IUserInfoService userInfoService, ILogger<HomeController> logger)
        {
            _userInfoService = userInfoService;
            _logger = logger;
        }

        [Route("~/CMS")]
        [Route("~/CMS/Index")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("~/CMS/UserInfoList")]
        public IActionResult UserInfoList()
        {
            return View();
        }
        [Route("~/CMS/WriterInfoList")]
        public IActionResult WriterInfoList()
        {
            return View();
        }
        [Route("~/CMS/BlogNewsList")]
        public IActionResult BlogNewsList()
        {
            return View();
        }
        [Route("~/CMS/TagInfoList")]
        public IActionResult TagInfoList()
        {
            return View();
        }
        [Route("~/CMS/CommentList")]
        public IActionResult CommentList()
        {
            return View();
        }
        [Route("~/CMS/SocietyList")]
        public IActionResult SocietyList()
        {
            return View();
        }



        [Route("~/CMS/EditUserInfo")]
        public async Task<IActionResult> EditUserInfo(int id)
        {
            var userInfo= await _userInfoService.FindByIdAsync(id);

            var model = new UserInfoModel
            {
                Id = id,
                Name = userInfo.Name,
                UserName = userInfo.UserName,
                UserPwd = userInfo.UserPwd,
                Motto = userInfo.Motto,
                MainPagePhoto = userInfo.MainPagePhoto
            };


            return View(model);




        }



        [Route("~/CMS/AddUserInfo")]
        public async Task<IActionResult> AddUserInfo(int id)
        {
          
            return View();




        }

    }
}
