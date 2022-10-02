using Microsoft.AspNetCore.Mvc;

namespace MyBlog.WebAPI.Controllers
{


    //Route只是一个防火墙 
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        //~/表示的就是localhost:ip 和/没啥区别
        [Route("~/")]
        [Route("/Home")]
        [Route("~/Home/Index")]

        public IActionResult Index()
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
