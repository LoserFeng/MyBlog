using Microsoft.AspNetCore.Mvc;

namespace MyBlog.WebAPI.Controllers
{

    [Route("[controller]/[action]")]
    public class CMSController : Controller
    {

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


    }
}
