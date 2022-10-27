using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBlog.IService;
using MyBlog.Model;
using MyBlog.Model.ViewModels.Blog;
using MyBlog.Service;
using MyBlog.WebAPI.Controllers.Api.ApiResult;
using System.Security.Claims;

namespace MyBlog.WebAPI.Controllers.Api
{

    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
	{


        private readonly ICommentService _commentService;
        private readonly IUserInfoService _userInfoService; 


        public CommentController(ICommentService commentService, IUserInfoService userInfoService)
        {
            _commentService = commentService;
            _userInfoService = userInfoService;
        }


        [HttpPost("Create")]
        [Authorize]
        public async Task<ActionResult<ApiResponse>> CreateComment([FromForm]CommentInfo commentInfo)
        {


            Claim? claim = User.FindFirst("Id");
            int id = Convert.ToInt32(claim?.Value);
            var userInfo = await _userInfoService.FindAsync(id);
            if (userInfo == null)
            {
                return ApiResponse.Error(Response,"没有找到对应的用户");
            }
            Comment comment = new Comment {
                GUID = Guid.NewGuid().ToString(),
                BlogId = commentInfo.BlogId,
                UserId=id,
                SupportCount = 0,
                Content = commentInfo.Content,
                Time=DateTime.Now
            };





            await _commentService.CreateAsync(comment);


            comment = await  _commentService.QueryByGUIDAsync(comment.GUID);




            return ApiResponse.Ok(comment);
        }



        [HttpPost("CreateReply")]
        [Authorize]
        public async Task<ActionResult<ApiResponse>> CreateReplyComment([FromForm] CommentInfo commentInfo)
        {

            Claim? claim = User.FindFirst("Id");
            int id = Convert.ToInt32(claim?.Value);
            var userInfo = await _userInfoService.FindAsync(id);
            if (userInfo == null)
            {
                return ApiResponse.Error(Response, "没有找到对应的用户身份！");
            }

            Comment comment = new Comment
            {
                GUID = Guid.NewGuid().ToString(),
                FatherId = commentInfo.FatherId,
                BlogId= commentInfo.BlogId,
                UserId = id,
                TargetName = commentInfo.TargetName,
                SupportCount = 0,
                Content = commentInfo.Content,
                Time = DateTime.Now
            };





            await _commentService.CreateAsync(comment);


            comment = await _commentService.QueryByGUIDAsync(comment.GUID);



            return ApiResponse.Ok(comment);
        }


        [HttpPost("Delete")]
        [Authorize]
        public async Task<ActionResult<ApiResponse>> DeleteComment(String GUID)
        {



            var res = await _commentService.DeleteByGUIDAsync(GUID);



            if (res == false)
            {
                ApiResponse.Error(Response,message: "删除失败");
            }



            return ApiResponse.Ok("删除成功");
        }


    }
}
