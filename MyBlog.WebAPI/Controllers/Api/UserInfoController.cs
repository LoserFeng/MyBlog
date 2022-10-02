using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyBlog.IService;
using MyBlog.Model;
using MyBlog.Model.DTO;
using MyBlog.Model.ViewModels.Auth;
using MyBlog.Model.ViewModels.Register;
using MyBlog.Service;
using MyBlog.Utility._MD5;
using System.DirectoryServices;
using System.Security.Claims;

namespace MyBlog.WebAPI.Controllers.Api
{


    [Route("api/[controller]")]
    [ApiController]
    public class UserInfoController : ControllerBase
    {

        private readonly IUserInfoService _userInfoService;


        public UserInfoController(IUserInfoService userInfoService)
        {
            _userInfoService = userInfoService;
        }

        [HttpGet("GetById")]
        public async Task<ActionResult<ApiResponse>> GetById([FromServices] IMapper iMapper, int id)
        {

            var data = await _userInfoService.FindAsync(id);
            //return Ok(data);  //code 200 404 之类的
            if (data == null)
            {
                return ApiResponse.Error(Response, "没有找到相关的用户数据");

            }




            return ApiResponse.Ok(data);
        }




        [HttpGet("GetUserInfo")]
        public async Task<ActionResult<ApiResponse>> GetUserInfo([FromServices] IMapper iMapper)
        {


            Claim ?claim = User.FindFirst("Id");
            if (claim == null)
            {
                return ApiResponse.Error(Response,"找不到此对应Claim的Id");
            }

            int id = Convert.ToInt32(claim?.Value);

            var data = await _userInfoService.FindAsync(id);
            //return Ok(data);  //code 200 404 之类的
            if (data == null)
            {
                return ApiResponse.Error(Response, "没有找到相关的用户数据");

            }




            return ApiResponse.Ok(data);
        }

        [HttpPut("Edit")]
        public async Task<ApiResponse> Edit([FromServices] IMapper iMapper, string name)
        {

            Claim? claim = User.FindFirst("Id");
            int id = Convert.ToInt32(claim?.Value);  //JWT授权。。 
            var user = await _userInfoService.FindByIdAsync(id);
            user.Name = name;
            bool b = await _userInfoService.UpdateAsync(user);

            if (!b) return ApiResponse.Error(Response, "修改失败");
            /*            var writerDTO = iMapper.Map<UserDTO>(user);*/
            return ApiResponse.Ok(user);


        }



        [HttpPost("Create")]
        public async Task<ApiResponse> Create([FromServices] IMapper iMapper, RegisterInfo registerInfo)
        {




            UserInfo user = new UserInfo
            {
                Name = registerInfo.Name,
                UserName = registerInfo.UserName,
                UserPwd = MD5Helper.MD5Encrypt32(registerInfo.UserPwd),
                writerInfo =new WriterInfo()
                {
                    Id=0,
                    WriterName=registerInfo.Name,
                },
                motto = registerInfo.motto,
                MainPagePhoto = registerInfo.MainPagePhoto,
                Concerns = new List<WriterInfo>(),
                Favorites = new List<BlogNews>()

            };





            if (! await _userInfoService.CheckInfoAsync(user.Name,user.UserName))
            {
                return ApiResponse.Ok(false, message: "名字或用户名已存在");
            }

            bool res=await _userInfoService.register(user);

            if (res == true)
            {
                return ApiResponse.Ok(true, message: "注册成功");
            }


            return ApiResponse.Error(Response,"用户数据插入失败!");

        }



    }
}
