using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBlog.IService;
using MyBlog.Model;
using MyBlog.Model.DTO;
using MyBlog.Utility._MD5;
using MyBlog.Utility.ApiResult;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Tls;
using System.Net.WebSockets;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;

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
        public async Task<ApiResult>Create([FromServices] IMapper iMapper,string name,string username,string userpwd)
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
            var writerDTO = iMapper.Map<WriterDTO>(writerInfo);
            return ApiResultHelper.Success(writerDTO);

        }

        [HttpGet("GetAll")]
        public async Task<ApiResult> GetAll([FromServices] IMapper iMapper)
        {
            List<WriterInfo> AllWriterInfo = await _writerInfoService.QueryAllAsync();
            List<WriterDTO> WriterDTOList;
            WriterDTOList = AllWriterInfo.ConvertAll<WriterDTO>(w => iMapper.Map<WriterDTO>(w));
            return ApiResultHelper.Success(WriterDTOList);

        }

/*
        [HttpDelete("Delete")]
        public async Task<ApiResult> Delete(int id)
        {
            var 
           
            
        }*/



        [HttpPut("Edit")]
        public async Task<ApiResult> Edit([FromServices] IMapper iMapper,string name)
        {

            Claim ?claim = this.User.FindFirst("Id");
            int id = Convert.ToInt32(claim?.Value);  //JWT授权。。 
            var writer = await _writerInfoService.FindByIdAsync(id);
            writer.Name = name;
            bool b= await _writerInfoService.UpdateAsync(writer);

            if(!b)return ApiResultHelper.Error("修改失败");
            var writerDTO = iMapper.Map<WriterDTO>(writer);
            return ApiResultHelper.Success(writerDTO);

            
        }


        [HttpGet("FindById")]
        public async Task<ApiResult> FindById([FromServices]IMapper iMapper,int id)
        {
            var writerInfo= await _writerInfoService.FindByIdAsync(id);
            var writerDTO=iMapper.Map<WriterDTO>(writerInfo);
            return ApiResultHelper.Success(writerDTO);

        }

    }
}
