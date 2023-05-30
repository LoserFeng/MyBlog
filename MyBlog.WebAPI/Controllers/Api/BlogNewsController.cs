using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.Extensions.Hosting;
using MyBlog.IRepository;
using MyBlog.IService;
using MyBlog.Model;
using MyBlog.Model.DTO;
using MyBlog.Model.ViewModels.Blog;
using MyBlog.Model.ViewModels.CMS.BlogNews;
using MyBlog.Model.ViewModels.Register;
using MyBlog.Service;
using MyBlog.WebAPI.Controllers.Api.ApiResult;
using MyBlog.WebAPI.Extensions;
using Newtonsoft.Json;
using Org.BouncyCastle.Crypto.Tls;
using SqlSugar;
using System.Data;
using System.Net;
using System.Security.Claims;
using Utility._AutoMapper;
using Utility.MarkDown;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.Http.Features;
using MyBlog.WebAPI.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;
using System.IO.Compression;
using System.Text;
using Ubiety.Dns.Core.Records.NotUsed;

namespace MyBlog.WebAPI.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class BlogNewsController : ControllerBase
    {


        private readonly IBlogNewsService _blogNewsService;
        private readonly IUserInfoService _userInfoService;
        private readonly IPhotoService _photoService;

        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ITagInfoService _tagService;

        private readonly ViewModelMapper _viewModelMapper;
        private readonly FormOptions _defaultFormOptions;
        private readonly long _fileSizeLimit = 2097152;
        private readonly string _targetFilePath = "C:\\Users\\feng_\\Desktop\\课件\\多媒体\\大作业\\MyBlog\\MyBlog.WebAPI\\wwwroot\\tmp";

        private readonly string[] _permittedExtensions = { ".txt", "mpv4" };

        public BlogNewsController(IBlogNewsService blogNewsService, IUserInfoService userInfoService, IWebHostEnvironment webHostEnvironment, ITagInfoService tagService, IPhotoService photoService, IConfiguration config)
        {
            _blogNewsService = blogNewsService;
            _userInfoService = userInfoService;
            _tagService = tagService;
            _photoService = photoService;
            this.webHostEnvironment = webHostEnvironment;
            _viewModelMapper = new ViewModelMapper();
            _defaultFormOptions = new FormOptions();
            _fileSizeLimit = config.GetValue<long>("FileSizeLimit");

            // To save physical files to a path provided by configuration:
            _targetFilePath = config.GetValue<string>("StoredFilesPath");



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







            String Blog_GUID =creationInfo.Type!="video"? Guid.NewGuid().ToString():creationInfo.Id;


            if (Blog_GUID == null)
            {
                return ApiResponse.Error(Response, "Blog_GUID is null");

            }


            var tags = JsonConvert.DeserializeObject<List<String>>(creationInfo.Tags_JSON);
            if (tags == null)
            {
                Console.WriteLine("tags为空");
            }
            //创建tag对象
            var Tags = new List<TagInfo>();

            foreach (var tag in tags)
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
                var coverPhoto_fs = new FileStream(CoverPhoto_filePath, FileMode.Create);
                await creationInfo.CoverPhoto.CopyToAsync(coverPhoto_fs);
                coverPhoto_fs.Close();

                url = "/photos" + "/" + uniqueFileName;

            }


            //数据验证
            Claim? claim = User.FindFirst("Id");
            int id = Convert.ToInt32(claim?.Value);
            var userInfo = await _userInfoService.FindByIdAsync(id);
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
                Path = Path.Combine("blogs", Blog_GUID),
                CoverPhoto = new Photo
                {
                    Url = url,
                    FileName = creationInfo.CoverPhoto.FileName,
                    CreateTime = DateTime.Now,
                    FilePath = CoverPhoto_filePath
                },
                WriterId = userInfo!.WriterId,
                Admirers = new List<UserInfo>(),
                Tags = Tags
            };











            var (Tmp_Path, Tmp_AssetsPath, Blog_Path, Blog_AssetsPath) = InitDir(Blog_GUID);



            if (creationInfo.Type == "markdown")
            {





                Console.WriteLine("存入assets的照片有:");
                foreach (var photo in creationInfo.Assets)
                {
                    String photoName = photo.FileName;

                    String photoPath = Path.Combine(Tmp_AssetsPath, photoName);
                    if (System.IO.File.Exists(photoPath))
                    {
                        Console.WriteLine(photoName + "该图片已经存在");
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
            } else if (creationInfo.Type == "video")
            {
                Console.WriteLine("it is video");

                var videoName = creationInfo.VideoName;


                if (videoName == null)
                {
                    return ApiResponse.BadRequest("发表文章失败!");
                }




                Console.WriteLine("创建了一个Video{0}", videoName);

                var videoPath = Path.Combine(Tmp_Path, videoName);

                var MDMaker = new Video2MD(videoPath, Tmp_Path,blogNews);
                bool flag=await MDMaker.CoverVideo2MD();


                if (!flag)
                {


                    Console.WriteLine("CoverVideo2MD failed");
                }


                String MarkdownFileName = blogNews.Title + ".md";




/*                String MarkdownFilePath = Path.Combine(Tmp_Path, MarkdownFileName);
                var MD_fs = new FileStream(MarkdownFilePath, FileMode.Create);


                await creationInfo.MarkDownFile.CopyToAsync(MD_fs);
                MD_fs.Close();*/


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


            var processor = new MDProcessor(blogNews, Tmp_Path, Blog_AssetsPath);


            // 导入文章的时候一并导入文章里的图片，并对图片相对路径做替换操作
            blogNews.Content = processor.MarkdownParse();
            blogNews.Summary = processor.GetSummary(200);



            //将tmp文件夹中的所有文件进行删除

            bool res = await _blogNewsService.CreateAsync(blogNews);    //异步的方法本身返回的是Task<bool>类型，因此需要await来等待其运行完成后返回一个bool类型

            DirectoryInfo tmpDir = new DirectoryInfo(Tmp_Path);
            tmpDir.Delete(true);


            if (res == true)
            {
                return ApiResponse.Ok(message: "发表文章成功！");
            }



            return ApiResponse.BadRequest("发表文章失败!");


        }

        [HttpGet("GetById")]
        public async Task<ActionResult<ApiResponse>> GetBlogNewsById(int id)
        {

            var blogNews = await _blogNewsService.FindByIdAsync(id);



            if (blogNews == null)
            {
                return ApiResponse.Error(Response, "无法找到相关页面");
            }


            return ApiResponse.Ok(blogNews);
        }









        string GetTmpChunkDir(string fileName) => HttpContext.Session.TryGetValue(fileName, out byte[] bytes) ? Encoding.Default.GetString(bytes) : "";

        //保存文件

        [HttpPost("SaveChunkFile")]
        public async Task<JsonResult> SaveChunkFile([FromForm] IFormFile chunk, [FromForm] String fileName, [FromForm] int chunkIndex, [FromForm] int chunkCount)
        {


            Console.WriteLine("Save ChunkFIle begin");

            //用于保存的文件夹
            String uploadFolder;
            //目录分隔符，兼容不同系统
            char dirSeparator = Path.DirectorySeparatorChar;

            Console.WriteLine(fileName);
            String blogId = "";







            uploadFolder = Path.Combine(webHostEnvironment.WebRootPath, "tmp");

            try
            {
                if (chunk.Length == 0)
                {
                    return new JsonResult(new
                    {
                        success = false,
                        msg = "File Length 0",
                    });
                }

                if (chunkIndex == 0)
                {
                    //第一次上传时，生成一个随机id,做为保存块的临时文件夹，记录到session
                    HttpContext.Session.Set(fileName, Encoding.Default.GetBytes(Guid.NewGuid().ToString("N")));
                }

                if (!Directory.Exists(uploadFolder))
                    Directory.CreateDirectory(uploadFolder);

                var fullChunkDir = uploadFolder + dirSeparator + GetTmpChunkDir(fileName);
                if (!Directory.Exists(fullChunkDir))
                    Directory.CreateDirectory(fullChunkDir);


                var blob = chunk.FileName;
                var newFileName = blob + chunkIndex + Path.GetExtension(fileName);
                var filePath = fullChunkDir + Path.DirectorySeparatorChar + newFileName;

                //保存文件块
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await chunk.CopyToAsync(stream);
                }


                //所有块上传完成
                if (chunkIndex == chunkCount - 1)
                {
                    //也可以在这合并，在这合并就不用ajax调用CombineChunkFile合并
                    blogId = Guid.NewGuid().ToString();
                    await CombineChunkFile(fileName, uploadFolder, dirSeparator.ToString(), blogId);
                }

                var obj = new
                {
                    success = true,
                    date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    newFileName,
                    originalFileName = fileName,
                    size = chunk.Length,
                    nextIndex = chunkIndex + 1,
                    blogId = blogId,


                };



                return new JsonResult(obj);
            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    success = false,
                    msg = ex.Message
                });
            }
        }

        //合并文件
        public async Task<JsonResult> CombineChunkFile(string fileName, String uploadFolder, String dirSeparator, string blogId)
        {


            Console.WriteLine("Combine");

            try
            {
                return await Task.Run(() =>
                {


                    var tmpDir = GetTmpChunkDir(fileName);
                    var fullChunkDir = uploadFolder + dirSeparator + tmpDir;

                    var beginTime = DateTime.Now;


                    var Tmp_Path = Path.Combine(webHostEnvironment.WebRootPath, "tmp");



                    var destDir = Path.Combine(Tmp_Path, blogId);

                    if (!Directory.Exists(destDir))
                    {

                        Directory.CreateDirectory(destDir);


                    }
                    else
                    {
                        Directory.Delete(destDir);
                    }






                    var destFile = Path.Combine(destDir, fileName);

                    //获取临时文件夹内的所有文件块，排好序
                    var files = Directory.GetFiles(fullChunkDir).OrderBy(x => x.Length).ThenBy(x => x).ToList();
                    using (var destStream = System.IO.File.OpenWrite(destFile))
                    {
                        files.ForEach(chunk =>
                        {
                            using (var chunkStream = System.IO.File.OpenRead(chunk))
                            {
                                chunkStream.CopyTo(destStream);
                            }

                            System.IO.File.Delete(chunk);

                        });
                        Directory.Delete(fullChunkDir);
                    }

                    var totalTime = DateTime.Now.Subtract(beginTime).TotalSeconds;
                    return new JsonResult(new
                    {
                        success = true,
                        destFile = destFile.Replace('\\', '/'),
                        msg = $"combine completed ! {totalTime} s",
                    });
                });

            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    success = false,
                    msg = ex.Message,
                });
            }
            finally
            {
                HttpContext.Session.Remove(fileName);
            }
        }







        [HttpGet("GetAll")]
        public async Task<ActionResult<ApiResponse>> GetAllBlogNews()
        {

            var blogs = await _blogNewsService.QueryAllAsync();




            if (blogs == null)
            {
                return ApiResponse.Error(Response, "有问题。。。。");
            }


            return ApiResponse.Ok(blogs);
        }


        [HttpGet("BlogNewsList")]
        [Authorize]
        public async Task<ActionResult<LayUIResponse>> GetBlogNewsList(int page = 1, int limit = 8)
        {
            RefAsync<int> total = 0;


            var data = await _blogNewsService.GetBlogNewsList(page, limit, total);

            return LayUIResponse.Ok(count: total, data: data);
        }






        [HttpPost("Edit")]
        [Authorize]
        public async Task<ActionResult<ApiResponse>> Edit([FromForm] BlogNewsEdit blogNewsEdit)
        {



            bool res;

            var pre_blogNews = await _blogNewsService.FindByIdAsync(blogNewsEdit.Id);

            if (pre_blogNews == null)
            {
                return ApiResponse.Error(Response, "找不到对应的ID");

            }


            var tags = JsonConvert.DeserializeObject<List<String>>(blogNewsEdit.Tags_JSON);
            if (tags == null)
            {
                Console.WriteLine("tags为空");
            }
            //创建tag对象
            var Tags = new List<TagInfo>();

            foreach (var tag in tags)
            {
                Tags.Add(new TagInfo
                {
                    Name = tag
                });
            }









            //存储Coverphoto

            string CoverPhoto_filePath = String.Empty;   //最重要的东西
            string url = String.Empty;
            if (blogNewsEdit.CoverPhoto != null)
            {

                string uploadFolder = Path.Combine(webHostEnvironment.ContentRootPath, "wwwroot", "photos");
                string uniqueFileName = pre_blogNews.GUID + "_" + "CoverPhoto" + "_" + blogNewsEdit.CoverPhoto.FileName;
                CoverPhoto_filePath = Path.Combine(uploadFolder, uniqueFileName);
                var coverPhoto_fs = new FileStream(CoverPhoto_filePath, FileMode.Create);
                await blogNewsEdit.CoverPhoto.CopyToAsync(coverPhoto_fs);
                coverPhoto_fs.Close();

                url = "/photos" + "/" + uniqueFileName;
                res = await _photoService.DeleteByIdAsync(pre_blogNews.CoverPhotoId);
                if (!res)
                {
                    return ApiResponse.Error(Response, "删除图片失败");

                }

            }



            if (blogNewsEdit.Type != "text" && blogNewsEdit.Type != "markdown")
            {
                blogNewsEdit.Type = null;

            }


            BlogNews blogNews = new BlogNews
            {
                Id = blogNewsEdit.Id,
                GUID = pre_blogNews.GUID,
                Title = blogNewsEdit.Title,
                Introduction = blogNewsEdit.Introduction,
                Content = blogNewsEdit.Type == "text" ? blogNewsEdit.Content : null,
                BrowseCount = blogNewsEdit.BrowseCount,
                LikeCount = blogNewsEdit.LikeCount,
                Type = blogNewsEdit.Type,
                Path = Path.Combine("blogs", pre_blogNews.GUID),
                CoverPhoto = CoverPhoto_filePath == String.Empty ? null : new Photo
                {
                    Url = url,
                    FileName = blogNewsEdit.CoverPhoto.FileName,
                    CreateTime = DateTime.Now,
                    FilePath = CoverPhoto_filePath
                },
                Tags = Tags
            };






            String Tmp_Path, Tmp_AssetsPath, Blog_Path, Blog_AssetsPath;


            (Tmp_Path, Tmp_AssetsPath, Blog_Path, Blog_AssetsPath) = InitDir(pre_blogNews.GUID);


            if (blogNewsEdit.Type == "markdown")
            {





                Console.WriteLine("存入assets的照片有:");
                foreach (var photo in blogNewsEdit.Assets)
                {
                    String photoName = photo.FileName;

                    String photoPath = Path.Combine(Tmp_AssetsPath, photoName);
                    if (System.IO.File.Exists(photoPath))
                    {
                        Console.WriteLine(photoName + "该图片已经存在");
                    }
                    var asset_fs = new FileStream(photoPath, FileMode.Create);
                    await photo.CopyToAsync(asset_fs);
                    asset_fs.Close();

                    Console.WriteLine("{0}", photoName);
                }

                String MarkdownFileName = blogNewsEdit.MarkDownFile.FileName;


                String MarkdownFilePath = Path.Combine(Tmp_Path, MarkdownFileName);
                var MD_fs = new FileStream(MarkdownFilePath, FileMode.Create);
                await blogNewsEdit.MarkDownFile.CopyToAsync(MD_fs);
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
            if (blogNewsEdit.Type != null)
            {
                var processor = new MDProcessor(blogNews, Tmp_Path, Blog_AssetsPath);


                // 导入文章的时候一并导入文章里的图片，并对图片相对路径做替换操作
                blogNews.Content = processor.MarkdownParse();
                blogNews.Summary = processor.GetSummary(200);

            }



            //将tmp文件夹中的所有文件进行删除

            res = await _blogNewsService.UpdateAsync(blogNews);    //异步的方法本身返回的是Task<bool>类型，因此需要await来等待其运行完成后返回一个bool类型

            DirectoryInfo tmpDir = new DirectoryInfo(Tmp_Path);
            tmpDir.Delete(true);


            if (res == true)
            {
                return ApiResponse.Ok(message: "文章修改成功！");
            }



            return ApiResponse.BadRequest("文章修改失败!");


        }










        private (String,String,String, String ) InitDir(String Blog_GUID)
        {
            var Tmp_Path = Path.Combine(webHostEnvironment.WebRootPath, "tmp", Blog_GUID);
            var Tmp_AssetsPath = Path.Combine(Tmp_Path,"assets");
            var Blogs_Path = Path.Combine(webHostEnvironment.WebRootPath,  "blogs");
            var Blog_Path = Path.Combine(Blogs_Path, Blog_GUID);
            var Blog_AssetsPath = Path.Combine(Blog_Path,"assets");
            if (!Directory.Exists(Tmp_Path))
            { Directory.CreateDirectory(Tmp_Path); } 


            if (!Directory.Exists(Tmp_AssetsPath)) 
            {
                Directory.CreateDirectory(Tmp_AssetsPath);
            }
            else
            {
                Directory.Delete(Tmp_AssetsPath, true);
                Directory.CreateDirectory(Tmp_AssetsPath);
            };
            if (!Directory.Exists(Blogs_Path)) Directory.CreateDirectory(Blogs_Path);
            if (!Directory.Exists(Blog_Path))Directory.CreateDirectory(Blog_Path);
            else
            {
                Directory.Delete(Blog_Path, true);
                Directory.CreateDirectory(Blog_Path);
            }
            if (!Directory.Exists(Blog_AssetsPath)) Directory.CreateDirectory(Blog_AssetsPath);
            return (Tmp_Path,Tmp_AssetsPath,Blog_Path, Blog_AssetsPath);
        }



        private static Encoding GetEncoding(MultipartSection section)
        {
            var hasMediaTypeHeader =
                MediaTypeHeaderValue.TryParse(section.ContentType, out var mediaType);

            // UTF-7 is insecure and shouldn't be honored. UTF-8 succeeds in 
            // most cases.
            if (!hasMediaTypeHeader || Encoding.UTF7.Equals(mediaType.Encoding))
            {
                return Encoding.UTF8;
            }

            return mediaType.Encoding;
        }



        [HttpDelete("Delete")]
        public async Task<ApiResponse> Delete(int id)
        {
            var res = await _blogNewsService.DeleteByIdAsync(id);

            if (res)
            {
                return ApiResponse.Ok("删除成功");
            }

            return ApiResponse.Error(Response, "删除失败");

        }







    }
}
