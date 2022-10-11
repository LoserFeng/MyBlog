using Microsoft.AspNetCore.Mvc;
using MyBlog.IService;

namespace MyBlog.WebAPI.Controllers
{


    [Route("[controller]/[action]")]
    public class BlogController : Controller
    {

        private readonly IBlogNewsService _blogNewsService;


        public BlogController(IBlogNewsService blogNewsService)
        {
            _blogNewsService = blogNewsService;
        }


        [Route("~/Blog")]
        [Route("~/Blog/BlogPage")]
        public IActionResult BlogPage()
        {


            return View();
        }



        [Route("~/Blog/Creation")]
        public IActionResult Creation()
        {

            return View();
        }
    }
}
