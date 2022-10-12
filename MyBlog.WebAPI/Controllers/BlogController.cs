using Microsoft.AspNetCore.Mvc;
using MyBlog.IService;
using MyBlog.Model;
using MyBlog.Model.ViewModels.Blog;
using Utility._AutoMapper;
namespace MyBlog.WebAPI.Controllers
{


    

    [Route("[controller]/[action]")]
    public class BlogController : Controller
    {

        private readonly IBlogNewsService _blogNewsService;
        private readonly ITagService _tagService;
        private readonly ViewModelMapper _viewModelMapper;
        
        private readonly static int PageSize = 8;

        public BlogController(IBlogNewsService blogNewsService,ITagService tagService)
        {
            _blogNewsService = blogNewsService;
            _viewModelMapper = new ViewModelMapper();
            _tagService=tagService;
        }



        //规定一页只能放10篇文章
        [Route("~/Blog")]
        [Route("~/Blog/BlogPage")]
        [Route("~/Blog/BlogPage/TagId={TagId}&Page={Page}")]
        [Route("~/Blog/BlogPage/Page={Page}")]
        [Route("~/Blog/BlogPage/TagId={TagId}")]
        public async Task<IActionResult> BlogPageAsync(int TagId=0,int Page=1)
        {


            List<BlogNews>blogNewsList = null;
            if (TagId == 0)
            {
                //不对Tag类别进行筛选
                blogNewsList = await _blogNewsService.QueryAllAsync();


            }
            else
            {
                blogNewsList = await _blogNewsService.QueryByTagAsync(TagId);

            }
            var tags = await _tagService.QueryAllAsync();





            var Blog_Views=new List<BlogViewModel>();
            foreach(var blog in blogNewsList)
            {
                Blog_Views.Add(_viewModelMapper.GetBlogViewModel(blog));


            }

            BlogListViewModel model = new BlogListViewModel
            {
                Blogs = Blog_Views,
                CurrentTag = await _tagService.FindByIdAsync(TagId),
                Tags = tags

            };




            return View(model);
        }



        [Route("~/Blog/Creation")]
        public IActionResult Creation()
        {

            return View();
        }




    }





}
