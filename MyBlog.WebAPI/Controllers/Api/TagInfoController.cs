using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBlog.IService;
using MyBlog.Model;
using MyBlog.Model.ViewModels.Common;
using MyBlog.Model.ViewModels.Tag;
using MyBlog.WebAPI.Controllers.Api.ApiResult;
using SqlSugar;

namespace MyBlog.WebAPI.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagInfoController : ControllerBase
    {


        private readonly ITagInfoService _tagInfoService;

        public TagInfoController(ITagInfoService tagInfoService)
        {
            _tagInfoService =tagInfoService;

        }





        [HttpPost("Create")]
        public async Task<IApiResponse> CreateTag([FromForm]CreateTagInfo createTagInfo)
        {

            var tag=await _tagInfoService.FindByNameAsync(createTagInfo.Name);
            if (tag != null)
            {
                return ApiResponse.Error(Response, "已经存在该Tag，不能重复创建");

            }
            var tagInfo = new TagInfo
            {
                Name =createTagInfo.Name
            };

            bool res = await _tagInfoService.CreateAsync(tagInfo);
            if (!res) return ApiResponse.Error(Response,"添加失败:" + res);
            return ApiResponse.Ok(tagInfo);

        }



        [HttpGet("TagInfoList")]
        [Authorize]
        public async Task<ActionResult<LayUIResponse>> GetTagInfoList(int page = 1, int limit = 8)
        {
            RefAsync<int> total = 0;

            var data = await _tagInfoService.GetTagInfoList(page, limit, total);


            return LayUIResponse.Ok(count: total, data: data);
        }









        [HttpDelete("Delete")]
        public async Task<ApiResponse> DeleteTag(int id)
        {

            bool b = await _tagInfoService.DeleteByIdAsync(id);

            if (!b) return ApiResponse.Error(Response,"没有找到该文章类型");
            return ApiResponse.Ok(b);



        }
        [HttpPost("Edit")]
        public async Task<ApiResponse> EditTagInfo([FromForm]EditTagInfo editTagInfo)
        {

            

            var check = await _tagInfoService.FindByNameAsync(editTagInfo.Name);
            if (check != null)
            {
                return ApiResponse.Error(Response, "已经存在该名称的Tag，修改失败");
            }
            var pre_Tag = await _tagInfoService.FindByIdAsync(editTagInfo.Id);
            if (pre_Tag == null) return ApiResponse.Error(Response,"没有找到对应ID的Tag");

            var update_Tag = new TagInfo
            {
                Id = editTagInfo.Id,
                Name = editTagInfo.Name==null||editTagInfo.Name==""?pre_Tag.Name:editTagInfo.Name,
            };

            bool b = await _tagInfoService.UpdateAsync(update_Tag);
            if (!b) return ApiResponse.Error(Response,"修改失败");

            return ApiResponse.Ok("修改成功");




        }





    }
}
