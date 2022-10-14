using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using MyBlog.IService;
using MyBlog.Model;
using MyBlog.Model.ViewModels.Blog;
using SqlSugar;
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
/*        [Route("~/Blog/BlogPage/Page={Page}&TagId={TagId}&SearchString={SearchString}")]
        [Route("~/Blog/BlogPage/Page={Page}&SearchString={SearchString}")]
        [Route("~/Blog/BlogPage/Page={Page}&&TagId={TagId}")]
        [Route("~/Blog/BlogPage/TagId={TagId}&&SearchString={SearchString}")]
        [Route("~/Blog/BlogPage/TagId={TagId}")]
        [Route("~/Blog/BlogPage/SearchString={SearchString}")]
*/

        public async Task<IActionResult> BlogPage(int Page=1,int TagId=0,string SearchString=null)
        {


            Console.WriteLine(SearchString);
            RefAsync<int> total=0;
            List<BlogNews> blogNewsList = null;
            if (TagId == 0 && SearchString == null)
            {
                //不对Tag类别进行筛选
                blogNewsList = await _blogNewsService.QueryAllAsync();


            }
            else if (TagId != 0)
            {
                blogNewsList = await _blogNewsService.QueryByTagAsync(TagId,Page,PageSize,total);
            }
            else if (SearchString != "")
            {
                blogNewsList = await _blogNewsService.QueryByNameAsync(SearchString,Page,PageSize,total);
            }

            var tags = await _tagService.QueryAllAsync();





            var Blog_Views = new List<BlogViewModel>();
            foreach (var blog in blogNewsList)
            {
                Blog_Views.Add(_viewModelMapper.GetBlogViewModel(blog));


            }

            String url = Request.GetDisplayUrl();
            Console.WriteLine(url);

            BlogPageModel model = new BlogPageModel
            {
                Blogs = Blog_Views,
                CurrentTag = await _tagService.FindByIdAsync(TagId),
                Tags = tags,
                CurrentPage = Page,
                SearchString = SearchString,
                TagId=TagId,
                TotalPage = blogNewsList.Count == 0 ? 0 : (blogNewsList.Count() - 1) / PageSize + 1
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
