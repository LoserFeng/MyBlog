using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBlog.IService;
using MyBlog.Model;
using MyBlog.WebAPI.Utility._MD5;
using MyBlog.WebAPI.Utility.ApiResult;
using Org.BouncyCastle.Crypto.Tls;
using System.Net.WebSockets;

namespace MyBlog.WebAPI.Controllers
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




        [HttpPost("Create")]
        public async Task<ApiResult>Create(string name,string username,string userpwd)
        {
            WriterInfo writerInfo = new WriterInfo
            {
                Name = name,
                UserPwd = MD5Helper.MD5Encrypt32(userpwd),
                UserName = username
            };

            var oldWriter =await _writerInfoService.FindAsync(_writerInfo => _writerInfo.UserName == username);
            if (oldWriter != null)return ApiResultHelper.Error("账号已经存在 用户："+oldWriter);

            bool b=await _writerInfoService.CreateAsync(writerInfo);
            if (!b) return ApiResultHelper.Error("添加失败");
            return ApiResultHelper.Success(writerInfo);

        }

        [HttpGet("GetAll")]
        public async Task<ApiResult> GetAll()
        {
            var AllWriterInfo = await _writerInfoService.QueryAllAsync();

            return ApiResultHelper.Success(AllWriterInfo);

        }

/*
        [HttpDelete("Delete")]
        public async Task<ApiResult> Delete(int id)
        {
            var 
           
            
        }*/



        [HttpPut("Edit")]
        public async Task<ApiResult> Edit(string name)
        {
            int id = Convert.ToInt32(this.User.FindFirst("Id").Value);  //JWT授权。。 

            return ApiResultHelper.Error("还没有做完...");

            
        }

    }
}
