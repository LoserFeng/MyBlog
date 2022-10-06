using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBlog.IService;
using MyBlog.Model;
using MyBlog.Model.ViewModels.Home;
using System.Security.Claims;

namespace MyBlog.WebAPI.Controllers
{


    //Route只是一个防火墙 
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserInfoService _userInfoService;


        public HomeController(IUserInfoService userInfoService, ILogger<HomeController> logger)
        {
            _userInfoService = userInfoService;
            _logger = logger;
        }


        //~/表示的就是localhost:ip 和/没啥区别






        [Route("~/")]
        [Route("~/Home/Index")]

        public IActionResult Index()
        {


            HomeViewModel model = new HomeViewModel()
            {
                motto = "Hello  World!",
                photo=new Photo
                {
                    FileName = "default_homepage",
                    FilePath = "photos/default_homepage.jpeg"
                }


        };

              

            



            Console.WriteLine("一位旅客");
            return View(model);

        }


        [Route("~/Home/Login")]
        public IActionResult Login()
        {
            return View();
        }


        [Route("~/Home/Register")]
        public IActionResult Register()
        {
            return View();
        }



        [Route("~/Home/SkipPage")]
        public IActionResult SkipPage()
        {
            return View();
        }


        /// <summary>
        /// 新增页
        /// </summary>
        /// <returns></returns>
        public IActionResult AddDialog()
        {
            return View();
        }

        /// <summary>
        /// 编辑页
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DataEditDialog(int id)
        {
            ViewBag.id = id;
            return View();
        }

        

        
    }
}
