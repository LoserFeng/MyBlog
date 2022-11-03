using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBlog.IService;
using MyBlog.Model;
using MyBlog.Model.DTO;
using MyBlog.Model.ViewModels.Common;
using MyBlog.Service;
using MyBlog.Utility._MD5;
using MyBlog.WebAPI.Controllers.Api.ApiResult;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Tls;
using SqlSugar;
using System.Net.WebSockets;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;

namespace MyBlog.WebAPI.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class WriterInfoController : ControllerBase
    {

        private readonly IWriterInfoService _writerInfoService;
        public WriterInfoController(IWriterInfoService writerInfoService)
        {
            _writerInfoService = writerInfoService;
        }

        [HttpGet("WriterInfoList")]
        [Authorize]
        public async Task<ActionResult<LayUIResponse>> GetWriterInfoList(int page = 1, int limit = 8)
        {
            RefAsync<int> total = 0;

            var data = await _writerInfoService.GetWriterInfoList(page, limit, total);


            return LayUIResponse.Ok(count: total, data: data);
        }




        [HttpPost("Edit")]
        public async Task<ActionResult<ApiResponse>>Edit([FromForm] WriterInfoModel writerInfoModel)
        {

            WriterInfo writerInfo = new WriterInfo
            {
                Id = writerInfoModel.Id,
                WriterName = writerInfoModel.WriterName
            };



            var res = await _writerInfoService.UpdateAsync(writerInfo);
            if (res)
            {
                return ApiResponse.Ok("更新成功");

            }

            return ApiResponse.Error(Response, "更新失败");
        }



        [HttpDelete("Delete")]
        public async Task<ApiResponse> Delete(int id)
        {
            var res = await _writerInfoService.DeleteByIdAsync(id);

            if (res)
            {
                return ApiResponse.Ok("删除成功");
            }

            return ApiResponse.Error(Response, "删除失败");

        }






    }
}
