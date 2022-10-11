﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.Extensions.Hosting;
using MyBlog.IRepository;
using MyBlog.IService;
using MyBlog.Model;
using MyBlog.Model.DTO;
using MyBlog.Model.ViewModels.Blog;
using MyBlog.Model.ViewModels.Register;
using MyBlog.Service;
using Newtonsoft.Json;
using Org.BouncyCastle.Crypto.Tls;
using SqlSugar;
using System.Data;
using System.Security.Claims;
using Utility.MarkDown;
namespace MyBlog.WebAPI.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class BlogNewsController : ControllerBase
    {


        private readonly IBlogNewsService _blogNewsService;
        private readonly IUserInfoService _userInfoService;

        private readonly IWebHostEnvironment webHostEnvironment;


        public BlogNewsController(IBlogNewsService blogNewsService,IUserInfoService userInfoService, IWebHostEnvironment webHostEnvironment)
        {
            _blogNewsService = blogNewsService;
            _userInfoService = userInfoService;
            this.webHostEnvironment = webHostEnvironment;
        }





        /// <summary>
        /// 添加文章标题
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        [Authorize]
        public async Task<ActionResult<ApiResponse>> Create([FromForm] CreationInfo creationInfo)
        {







            String Blog_GUID=Guid.NewGuid().ToString();

            var tags = JsonConvert.DeserializeObject<List<String>>(creationInfo.Tags_JSON);
            if (tags == null)
            {
                Console.WriteLine("tags为空");
            }
            //创建tag对象
            var Tags = new List<TagInfo>();

            foreach(var tag in tags)
            {
                Tags.Add(new TagInfo
                {
                    Name = tag
                });
            }


            






            //存储Coverphoto

            string CoverPhoto_filePath = String.Empty;
            string url = String.Empty;
            if (creationInfo.CoverPhoto != null)
            {

                string uploadFolder = Path.Combine(webHostEnvironment.ContentRootPath, "wwwroot", "photos");
                string uniqueFileName = Blog_GUID + "_" + "CoverPhoto" + "_" + creationInfo.CoverPhoto.FileName;
                CoverPhoto_filePath = Path.Combine(uploadFolder, uniqueFileName);
                var coverPhoto_fs=new FileStream(CoverPhoto_filePath, FileMode.Create);
                await creationInfo.CoverPhoto.CopyToAsync(coverPhoto_fs);
                coverPhoto_fs.Close();

                url = "/photos" + "/" + uniqueFileName;

            }


            //数据验证
            Claim? claim = User.FindFirst("Id");
            int id = Convert.ToInt32(claim?.Value);
            var userInfo = await _userInfoService.FindAsync(id);
            if (userInfo == null)
            {
                return ApiResponse.Error(Response, "没有找到对应的用户身份！");
            }
            BlogNews blogNews = new BlogNews
            {
                GUID = Blog_GUID,
                Title = creationInfo.Title,
                Introduction = creationInfo.Introduction,
                Content = creationInfo.Type == "text" ? creationInfo.Content : null,
                Time = DateTime.Now,
                BrowseCount = 0,
                LikeCount = 0,
                Type = creationInfo.Type,
                Path = Path.Combine("blogs",Blog_GUID),
                CoverPhoto = new Photo
                {
                    Url = url,
                    FileName = creationInfo.CoverPhoto.FileName,
                    CreateTime = DateTime.Now,
                    FilePath = CoverPhoto_filePath
                },
                WriterId = userInfo!.WriterId,
                Admirers = new List<UserInfo>(),
                Tags=Tags
            };











            var (Tmp_Path,Tmp_AssetsPath,Blog_Path, Blog_AssetsPath) = InitDir(Blog_GUID);



            if (creationInfo.Type == "markdown")
            {





                Console.WriteLine("存入assets的照片有:");
                foreach(var photo in creationInfo.Assets)
                {
                    String photoName = photo.FileName;
                
                    String photoPath = Path.Combine(Tmp_AssetsPath, photoName);
                    if (System.IO.File.Exists(photoPath))
                    {
                        Console.WriteLine(photoName+"该图片已经存在");
                    }
                    var asset_fs = new FileStream(photoPath, FileMode.Create);
                    await photo.CopyToAsync(asset_fs);
                    asset_fs.Close();

                    Console.WriteLine("{0}", photoName);
                }

                String MarkdownFileName = creationInfo.MarkDownFile.FileName;


                String MarkdownFilePath = Path.Combine(Tmp_Path, MarkdownFileName);
                var MD_fs = new FileStream(MarkdownFilePath, FileMode.Create);
                await creationInfo.MarkDownFile.CopyToAsync(MD_fs);
                MD_fs.Close();


                var TmpDir = new DirectoryInfo(Tmp_Path);
                var files = TmpDir.GetFiles("*.md");
                Console.WriteLine(files.Length);
                var mdFile = files[0];
                var reader = mdFile.OpenText();
                var MarkdownContent = reader.ReadToEnd();
                reader.Close();

                blogNews.Content = MarkdownContent;


                Console.WriteLine("tmp文件夹中内容已经存放完毕");
            }


            var processor = new MDProcessor(blogNews,Tmp_Path, Blog_AssetsPath);


            // 导入文章的时候一并导入文章里的图片，并对图片相对路径做替换操作
            blogNews.Content = processor.MarkdownParse();
            blogNews.Summary = processor.GetSummary(200);



            //将tmp文件夹中的所有文件进行删除

            bool res= await _blogNewsService.CreateAsync(blogNews);    //异步的方法本身返回的是Task<bool>类型，因此需要await来等待其运行完成后返回一个bool类型

            DirectoryInfo tmpDir = new DirectoryInfo(Tmp_Path);
            tmpDir.Delete(true);


            if (res == true)
            {
                return ApiResponse.Ok(message:"发表文章成功！");
            }
        


            return ApiResponse.BadRequest("发表文章失败!");


        }

         [HttpGet("GetById")]
        public async Task<ActionResult<ApiResponse>> GetBlogNewsById(int id)
        {

            var blogNews=await _blogNewsService.FindByIdAsync(id);



            if(blogNews == null)
            {
                return ApiResponse.Error(Response, "无法找到相关页面");
            }


            return ApiResponse.Ok(blogNews);
        }





        [HttpGet("GetAll")]
        public async Task<ActionResult<ApiResponse>> GetAllBlogNews()
        {

            var blogs = await _blogNewsService.QueryAllAsync();




            if (blogs== null)
            {
                return ApiResponse.Error(Response, "有问题。。。。");
            }


            return ApiResponse.Ok(blogs);
        }








        private (String,String,String, String ) InitDir(String Blog_GUID)
        {
            var Tmp_Path = Path.Combine(webHostEnvironment.WebRootPath, "tmp");
            var Tmp_AssetsPath = Path.Combine(Tmp_Path,"assets");
            var Blogs_Path = Path.Combine(webHostEnvironment.WebRootPath,  "blogs");
            var Blog_Path = Path.Combine(Blogs_Path, Blog_GUID);
            var Blog_AssetsPath = Path.Combine(Blog_Path,"assets");
            if (!Directory.Exists(Tmp_Path)) Directory.CreateDirectory(Tmp_Path);
            if (!Directory.Exists(Tmp_AssetsPath)) Directory.CreateDirectory(Tmp_AssetsPath);
            if (!Directory.Exists(Blogs_Path)) Directory.CreateDirectory(Blogs_Path);
            if (!Directory.Exists(Blog_Path)) Directory.CreateDirectory(Blog_Path);
            if (!Directory.Exists(Blog_AssetsPath)) Directory.CreateDirectory(Blog_AssetsPath);
            return (Tmp_Path,Tmp_AssetsPath,Blog_Path, Blog_AssetsPath);
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
