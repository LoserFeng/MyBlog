using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBlog.IRepository;
using MyBlog.IService;
using MyBlog.Model;
using MyBlog.Model.DTO;
using SqlSugar;
using System.Data;
using System.Security.Claims;

namespace MyBlog.WebAPI.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class BlogNewsController : ControllerBase
    {


        private readonly IBlogNewsService _blogNewsService;


        public BlogNewsController(IBlogNewsService blogNewsService)
        {
            _blogNewsService = blogNewsService;
        }
        /*  [HttpGet("BlogNews")]
          public async Task<ActionResult<ApiResult>> GetBlogNews([FromServices] IMapper iMapper)
          {
              Claim? claim = User.FindFirst("Id");
              int id = Convert.ToInt32(claim?.Value);
              var data = await _blogNewsService.QueryAllAsync(c => c.WriterId == id);
              //return Ok(data);  //code 200 404 之类的
              if (data.Count == 0)
              {
                  return ApiResultHelper.Success(data);
              }

              List<BlogNewsDTO> BlogNewsDTOList = data.AsQueryable().Select(c => iMapper.Map<BlogNewsDTO>(c)).ToList();




              return ApiResultHelper.Success(BlogNewsDTOList);
          }



          /// <summary>
          /// 添加文章标题
          /// </summary>
          /// <param name="title"></param>
          /// <param name="content"></param>
          /// <returns></returns>
          [HttpPost("Create")]
          public async Task<ActionResult<ApiResult>> Create(string title, string content, int typeId)
          {
              //数据验证
              Claim? claim = User.FindFirst("Id");
              int id = Convert.ToInt32(claim?.Value);

              BlogNews blogNews = new BlogNews
              {
                  BrowseCount = 0,
                  Content = content,
                  LikeCount = 0,
                  Time = DateTime.Now,
                  Title = title,
                  TypeId = typeId,
                  WriterId = id
              };

              bool b = await _blogNewsService.CreateAsync(blogNews);    //异步的方法本身返回的是Task<bool>类型，因此需要await来等待其运行完成后返回一个bool类型


              if (!b)
              {
                  return ApiResultHelper.Error("添加失败");

              }
              return ApiResultHelper.Success(blogNews);



          }

          [HttpDelete("Delete")]
          public async Task<ActionResult<ApiResult>> Delete(int id)
          {
              bool b = await _blogNewsService.DeleteByIdAsync(id);

              if (!b) return ApiResultHelper.Error("删除失败");
              return ApiResultHelper.Success(b);
          }


          [HttpPut("Edit")]
          public async Task<ActionResult<ApiResult>> Edit(int id, string title, string content, int typeId)
          {

              BlogNews blogNews = await _blogNewsService.FindByIdAsync(id);
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

          [HttpGet("BlogNewsPage")]

          public async Task<ApiResult> GetBlogNewsPage([FromServices] IMapper iMapper, int page, int size)
          {

              RefAsync<int> total = 0;
              var blogNewsList = await _blogNewsService.QueryAsync(page, size, total);   //这里total的值会被传入,这个方法中total是ref的方式传入的
              List<BlogNewsDTO> blogNewsDTO;
              try
              {
                  blogNewsDTO = iMapper.Map<List<BlogNewsDTO>>(blogNewsList);

              }
              catch (Exception)
              {
                  return ApiResultHelper.Error("映射错误");
              }

              return ApiResultHelper.Success(blogNewsDTO, total);



          }*/
    }
}
