using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBlog.IRepository;
using MyBlog.IService;
using MyBlog.Model;
using MyBlog.WebAPI.Utility.ApiResult;
using System.Data;

namespace MyBlog.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogNewsController : ControllerBase
    {


        private readonly IBlogNewsService _blogNewsService;

        public BlogNewsController(IBlogNewsService blogNewsService)
        {
            this._blogNewsService = blogNewsService;  
        }
        [HttpGet("BlogNews")]
        public async Task<ActionResult<ApiResult>> GetBlogNews()
        {
            var data = await _blogNewsService.QueryAllAsync();
            //return Ok(data);  //code 200 404 之类的
            if (data == null)
            {
                return ApiResultHelper.Error("没有更多的文章");
            }
            return ApiResultHelper.Success(data);
        }



        /// <summary>
        /// 添加文章标题
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        public async Task<ActionResult<ApiResult>> Create(string title,string content,int typeId)
        {
            //数据验证

            BlogNews blogNews = new BlogNews
            {
                BrowseCount = 0,
                Content = content,
                LikeCount = 0,
                Time = DateTime.Now,
                Title = title,
                TypeId = typeId,
                WriterId = 1
            };

            bool b = await _blogNewsService.CreateAsync(blogNews);    //异步的方法本身返回的是Task<bool>类型，因此需要await来等待其运行完成后返回一个bool类型


            if (!b)
            {
                return ApiResultHelper.Error("添加失败");

            }
            return ApiResultHelper.Success(blogNews);



        }

        [HttpDelete("Delete")]
        public async Task<ActionResult<ApiResult>>Delete(int id)
        {
            bool b = await _blogNewsService.DeleteByIdAsync(id);

            if (!b) return ApiResultHelper.Error("删除失败");
            return ApiResultHelper.Success(b);
        }


        [HttpPut("Edit")]
        public async Task<ActionResult<ApiResult>>Edit(int id,string title, string content, int typeId)
        {

            BlogNews blogNews =await _blogNewsService.FindByIdAsync(id);
            if (blogNews == null)
            {
                return ApiResultHelper.Error("找不到该id所对应的文章,修改失败");
            }

            blogNews.Title = title;
            blogNews.Content = content;
            blogNews.TypeId = typeId;




            bool b = await _blogNewsService.UpdateAsync(blogNews);


            if (!b) return ApiResultHelper.Error("修改失败");
            return ApiResultHelper.Success(blogNews);
        }
    }
}
