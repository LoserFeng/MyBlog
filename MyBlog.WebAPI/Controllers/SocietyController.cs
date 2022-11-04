using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBlog.IService;
using MyBlog.Model.ViewModels.Blog;
using MyBlog.Model.ViewModels.Society;
using MyBlog.Service;
using MyBlog.WebAPI.Controllers.Api.ApiResult;
using SqlSugar;
using System.Reflection.Metadata;
using Utility._AutoMapper;

namespace MyBlog.WebAPI.Controllers
{

   



    [Route("[controller]/[action]")]
    public class SocietyController : Controller
    {

        private readonly ICommentService _commentService;

        public SocietyController(ICommentService commentService)
        {
            _commentService = commentService;
        }   

        [Route("~/Society")]
        [Route("~/Society/Index")]
        public async Task<IActionResult> Index()
        {
            var comments = await _commentService.GetSocietyAsync();

            SocietyViewModel model = new SocietyViewModel
            {
                Comments = comments
            };
            return View(model);
        }





    }
}
