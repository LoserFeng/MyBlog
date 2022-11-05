using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBlog.IService;
using MyBlog.Model.ViewModels.Tag;
using MyBlog.Model;
using MyBlog.WebAPI.Controllers.Api.ApiResult;
using SqlSugar;
using System.Security.Claims;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MyBlog.Model.ViewModels.CMS.EventInfo;

namespace MyBlog.WebAPI.Controllers.Api
{

    [Route("api/[controller]")]
    [ApiController]
    public class EventInfoController : ControllerBase
    {




        private readonly IEventInfoService _eventInfoService;

        public EventInfoController(IEventInfoService eventInfoService)
        {
            _eventInfoService = eventInfoService;   

        }




        [Authorize]
        [HttpPost("Create")]
        public async Task<IApiResponse> CreateEvent([FromForm]EventInfoCreate eventInfoCreate)
        {
            Claim? claim = User.FindFirst("Id");
            if(claim == null)
            {
                return ApiResponse.Error(Response, "Authorization验证不通过");

            }
            int userId = Convert.ToInt32(claim?.Value);

            var eventInfo = new EventInfo()
            {
                EventContent = eventInfoCreate.EventContent,
                Time = DateTime.Now,
                UserId=userId
            };




            var id = await _eventInfoService.CreateReturnIdAsync(eventInfo);
            eventInfo.Id = id;

            return ApiResponse.Ok(eventInfo,"eventInfo添加成功");


        }


/*
        [HttpGet("TagInfoList")]
        [Authorize]
        public async Task<ActionResult<LayUIResponse>> GetTagInfoList(int page = 1, int limit = 8)
        {
            RefAsync<int> total = 0;

            var data = await _tagInfoService.GetTagInfoList(page, limit, total);


            return LayUIResponse.Ok(count: total, data: data);
        }

*/







        [HttpDelete("Delete")]
        public async Task<ApiResponse> DeleteEvent(int id)
        {

            bool b = await _eventInfoService.DeleteByIdAsync(id);

            if (!b) return ApiResponse.Error(Response, "没有找到ID对应的Event");
            return ApiResponse.Ok("删除Event Success");



        }
        [HttpPost("Edit")]
        public async Task<ApiResponse> EditEvent([FromForm]EventInfoEdit eventInfoEdit)
        {




            var pre_Event = await _eventInfoService.FindByIdAsync(eventInfoEdit.Id);
            if (pre_Event == null) return ApiResponse.Error(Response, "没有找到对应ID的Event");

            var update_Event = new EventInfo
            {
                Id = pre_Event.Id,
                EventContent = eventInfoEdit.EventContent,
                UserId = pre_Event.UserId,
                Time = pre_Event.Time,
            };


            bool b = await _eventInfoService.UpdateAsync(update_Event);
            if (!b) return ApiResponse.Error(Response, "修改失败");

            return ApiResponse.Ok("修改成功");




        }
    }
}
