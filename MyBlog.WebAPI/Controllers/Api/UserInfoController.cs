﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using MyBlog.IService;
using MyBlog.Model;
using MyBlog.Model.DTO;
using MyBlog.Model.ViewModels.Auth;
using MyBlog.Model.ViewModels.CMS.UserInfo;
using MyBlog.Model.ViewModels.Register;
using MyBlog.Service;
using MyBlog.Utility._MD5;
using MyBlog.WebAPI.Controllers.Api.ApiResult;
using SqlSugar;
using System.DirectoryServices;
using System.Security.Claims;

namespace MyBlog.WebAPI.Controllers.Api
{


    [Route("api/[controller]")]
    [ApiController]
    public class UserInfoController : ControllerBase
    {

        private readonly IUserInfoService _userInfoService;

        private readonly IWebHostEnvironment webHostEnvironment;
        public UserInfoController(IUserInfoService userInfoService,IWebHostEnvironment webHostEnvironment)
        {
            _userInfoService = userInfoService;
            this.webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("UserInfoList")]
        [Authorize]
        public async Task<ActionResult<LayUIResponse>> GetUserInfoList(int page=1,int limit=8)
        {
            RefAsync<int> total = 0;

            var data = await _userInfoService.QueryAllAsync(page,limit,total);


            return LayUIResponse.Ok(count: total, data: data);
        }





        [HttpGet("GetById")]
        public async Task<ActionResult<ApiResponse>> GetById([FromServices] IMapper iMapper, int id)
        {

            var data = await _userInfoService.FindByIdAsync(id);
            //return Ok(data);  //code 200 404 之类的
            if (data == null)
            {
                return ApiResponse.Error(Response, "没有找到相关的用户数据");

            }




            return ApiResponse.Ok(data);
        }


        [HttpGet("GetUserInfoByWriterId")]
        public async Task<ActionResult<ApiResponse>> GetUserInfoByWriterId(int WriterId)
        {


            Claim? claim = User.FindFirst("Id");
            if (claim == null)
            {
                return ApiResponse.Error(Response, "找不到此对应Claim的Id");
            }

            int id = Convert.ToInt32(claim?.Value);

            var data = await _userInfoService.FindByWriterIdAsync(WriterId);
            //return Ok(data);  //code 200 404 之类的
            if (data == null)
            {
                return ApiResponse.Error(Response, "没有找到相关的用户数据");

            }
            Console.WriteLine(data.MainPagePhoto.FilePath.ToString());




            return ApiResponse.Ok(data);
        }





        [HttpGet("GetUserInfo")]
        public async Task<ActionResult<ApiResponse>> GetUserInfo()
        {


            Claim ?claim = User.FindFirst("Id");
            if (claim == null)
            {
                return ApiResponse.Error(Response,"找不到此对应Claim的Id");
            }

            int id = Convert.ToInt32(claim?.Value);

            var data = await _userInfoService.FindByIdAsync(id);
            //return Ok(data);  //code 200 404 之类的
            if (data == null)
            {
                return ApiResponse.Error(Response, "没有找到相关的用户数据");

            }
            Console.WriteLine(data.MainPagePhoto.FilePath.ToString());




            return ApiResponse.Ok(data);
        }





        [HttpPost("Create")]
        public async Task<ApiResponse> Create([FromServices] IMapper iMapper, [FromForm] RegisterInfo registerInfo)
        {



            if (ModelState.IsValid)
            {


                string filePath =null;
                string url = null;
                if (registerInfo.photo != null)
                {
                   
                    string uploadFolder = Path.Combine(webHostEnvironment.ContentRootPath,"wwwroot", "photos");



                    string uniqueFileName =Guid.NewGuid().ToString()+"_"+registerInfo.photo.FileName;
                    filePath=Path.Combine(uploadFolder ,uniqueFileName);
                    await registerInfo.photo.CopyToAsync(new FileStream(filePath,FileMode.Create));
                    url = "/photos"+"/"+ uniqueFileName;
                    
                }


#pragma warning disable CS8601 // 引用类型赋值可能为 null。
                UserInfo user = new UserInfo
                {
                    Name = registerInfo.Name,
                    UserName = registerInfo.UserName,
                    UserPwd = MD5Helper.MD5Encrypt32(registerInfo.UserPwd),
                    WriterInfo =new WriterInfo()
                    {
                        Id=0,
                        WriterName=registerInfo.Name,
                    },
                    Motto = registerInfo.Motto,
                    MainPagePhoto = filePath!=null? new Photo()
                    {
                        Url=url,
                        FileName=registerInfo.photo.FileName,
                        CreateTime = DateTime.Now,
                        FilePath= filePath,

                    }:null,
                    Concerns = new List<WriterInfo>(),
                    Favorites = new List<BlogNews>(),
                    Events=new List<EventInfo>()

                };
#pragma warning restore CS8601 // 引用类型赋值可能为 null。







                if (! await _userInfoService.CheckInfoAsync(user.Name,user.UserName))
                {
                    return ApiResponse.BadRequest(  "名字或用户名已存在");
                }

                bool res=await _userInfoService.register(user);
                
                if (res == true)
                {
                    return ApiResponse.Ok(true, message: "注册成功！");
                }
            }


            return ApiResponse.BadRequest("用户数据插入失败!");

        }






        [Authorize]
        [HttpPost("Edit")]
        public async Task<ApiResponse> Edit([FromServices] IMapper iMapper, [FromForm] UserInfoEdit userInfoEdit)
        {



            if (ModelState.IsValid)
            {


                string filePath = null;
                string url = null;
                if (userInfoEdit.MainPagePhoto != null)
                {

                    string uploadFolder = Path.Combine(webHostEnvironment.ContentRootPath, "wwwroot", "photos");



                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + userInfoEdit.MainPagePhoto.FileName;
                    filePath = Path.Combine(uploadFolder, uniqueFileName);
                    await userInfoEdit.MainPagePhoto.CopyToAsync(new FileStream(filePath, FileMode.Create));
                    url = "/photos" + "/" + uniqueFileName;

                }




#pragma warning disable CS8601 // 引用类型赋值可能为 null。
                UserInfo user = new UserInfo
                {
                    Id=userInfoEdit.Id,
                    Name = userInfoEdit.Name,
                    UserName = userInfoEdit.UserName,
                    UserPwd = userInfoEdit.UserPwd!=null? MD5Helper.MD5Encrypt32(userInfoEdit.UserPwd):null,
                    Motto = userInfoEdit.Motto,
                    MainPagePhoto = userInfoEdit.MainPagePhoto != null? new Photo()
                    {
                        Url = url,
                        FileName = userInfoEdit.MainPagePhoto.FileName,
                        CreateTime = DateTime.Now,
                        FilePath = filePath,

                    } :null

                };
#pragma warning restore CS8601 // 引用类型赋值可能为 null。



                if (!await _userInfoService.CheckInfoAsync(user.Name, user.UserName))
                {
                    return ApiResponse.BadRequest("名字或用户名已存在");
                }

                bool res = await _userInfoService.UpdateAsync(user);

                if (res == true)
                {
                    return ApiResponse.Ok(true, message: "更新用户数据成功！");
                }
            }


            return ApiResponse.BadRequest("用户数据更新失败!");

        }
        [Authorize]
        [HttpDelete("Delete")]
        public async Task<ApiResponse> Delete(int id)
        {
           var res=await _userInfoService.DeleteByIdAsync(id);

            if (res)
            {
                return ApiResponse.Ok("删除成功");
            }

            return ApiResponse.Error(Response, "删除失败");

        }

        [Authorize]
        [HttpPost("Follow")]
        public async Task<ApiResponse>Follow(int WriterId)
        {
            Claim? claim = User.FindFirst("Id");
            if (claim == null)
            {
                return ApiResponse.Error(Response, "找不到此对应Claim的Id");
            }

            int userId = Convert.ToInt32(claim?.Value);


            var res = await _userInfoService.CreateFollowAsync(userId, WriterId);
            if (res)
            {
                return ApiResponse.Ok("关注成功");
            }

            return ApiResponse.Error(Response, "关注失败");

        }


        [Authorize]
        [HttpDelete("Unfollow")]
        public async Task<ApiResponse> Unfollow(int WriterId)
        {
            Claim? claim = User.FindFirst("Id");
            if (claim == null)
            {
                return ApiResponse.Error(Response, "找不到此对应Claim的Id");
            }

            int userId = Convert.ToInt32(claim?.Value);


            var res = await _userInfoService.DeleteFollowAsync(userId, WriterId);

            if (res)
            {
                return ApiResponse.Ok("取消关注成功");
            }

            return ApiResponse.Error(Response, "取消关注失败");
        }




        [Authorize]
        [HttpPost("Like")]
        public async Task<ApiResponse> Like(int BlogId)
        {

            Claim? claim = User.FindFirst("Id");
            if (claim == null)
            {
                return ApiResponse.Error(Response, "找不到此对应Claim的Id");
            }

            int userId = Convert.ToInt32(claim?.Value);
            var res = await _userInfoService.CreateLikeAsync(userId,BlogId);


            if (res)
            {
                return ApiResponse.Ok(res);
            }
            return ApiResponse.Error(Response, "点赞失败");
        }


        [Authorize]
        [HttpDelete("Unlike")]
        public async Task<ApiResponse> Unlike(int BlogId)
        {

            Claim? claim = User.FindFirst("Id");
            if (claim == null)
            {
                return ApiResponse.Error(Response, "找不到此对应Claim的Id");
            }

            int userId = Convert.ToInt32(claim?.Value);
            var res = await _userInfoService.DeleteLikeAsync(userId,BlogId);


            if (res)
            {
                return ApiResponse.Ok(res);
            }
            return ApiResponse.Error(Response, "取消点赞失败");
        }





        [Authorize]
        [HttpPost("Favorite")]
        public async Task<ApiResponse> Favorite(int BlogId)
        {

            Claim? claim = User.FindFirst("Id");
            if (claim == null)
            {
                return ApiResponse.Error(Response, "找不到此对应Claim的Id");
            }

            int userId = Convert.ToInt32(claim?.Value);

            var res = await _userInfoService.CreateFavoriteAsync(userId, BlogId);

            if (res)
            {
                return ApiResponse.Ok("收藏成功");
            }

            return ApiResponse.Error(Response, "收藏失败");
        }


        [Authorize]
        [HttpDelete("Unfavorite")]
        public async Task<ApiResponse> Unfavorite(int BlogId)
        {

            Claim? claim = User.FindFirst("Id");
            if (claim == null)
            {
                return ApiResponse.Error(Response, "找不到此对应Claim的Id");
            }

            int userId = Convert.ToInt32(claim?.Value);

            var res = await _userInfoService.DeleteFavoriteAsync(userId,BlogId);


            if (res)
            {
                return ApiResponse.Ok("取消收藏成功");
            }

            return ApiResponse.Error(Response, "取消收藏失败");
        }

    }



}
