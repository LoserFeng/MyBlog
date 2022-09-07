using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBlog.IRepository;
using MyBlog.IService;

namespace MyBlog.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogNewsController : ControllerBase
    {


        private readonly IBlogNewsService _iBlogNewsService;
        public BlogNewsController(IBlogNewsService iBlogNewsService)
        {
            this._iBlogNewsService = iBlogNewsService;  
        }
        [HttpGet("BlogNews")]
        public async Task<ActionResult> GetBlogNews()
        {
            var data = await _iBlogNewsService.QueryAllAsync();
            return Ok(data);
        }
    }
}
