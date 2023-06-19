using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyBlog.IService;
using MyBlog.Model;
using MyBlog.Model.ViewModels.CMS.UserInfo;

using MyBlog.Model.ViewModels.Comment_ViewModel;
using MyBlog.Model.ViewModels.Common;
using MyBlog.Model.ViewModels.Error;
using System.Security.Claims;

namespace MyBlog.WebAPI.Controllers
{





    [Route("[controller]/[action]")]
    public class CMSController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IUserInfoService _userInfoService;
        private readonly IWriterInfoService _writerInfoService;

        private readonly IBlogNewsService _blogNewsService;

        private readonly ITagInfoService _tagInfoService;

        private readonly ICommentService _commentService;   

        public CMSController(IUserInfoService userInfoService,IWriterInfoService writerInfoService,IBlogNewsService blogNewsService,ITagInfoService tagInfoService ,ICommentService commentService,ILogger<HomeController> logger)
        {
            _userInfoService = userInfoService;
            _writerInfoService = writerInfoService;
            _blogNewsService = blogNewsService;
            _tagInfoService = tagInfoService;
            _commentService = commentService;
            _logger = logger;
        }

        [Route("~/CMS")]
        [Route("~/CMS/Index")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("~/CMS/UserInfoList")]
        public IActionResult UserInfoList()
        {
            return View();
        }
        [Route("~/CMS/WriterInfoList")]
        public IActionResult WriterInfoList()
        {
            return View();
        }
        [Route("~/CMS/BlogNewsList")]
        public IActionResult BlogNewsList()
        {
            return View();
        }
        [Route("~/CMS/TagInfoList")]
        public IActionResult TagInfoList()
        {
            return View();
        }
        [Route("~/CMS/CommentList")]
        public IActionResult CommentList()
        {
            return View();
        }
        [Route("~/CMS/SocietyList")]
        public IActionResult SocietyList()
        {
            return View();
        }



        [Route("~/CMS/EditUserInfo")]
        public async Task<IActionResult> EditUserInfo(int id)
        {
            var userInfo= await _userInfoService.FindByIdAsync(id);

            var model = new UserInfoModel
            {
                Id = id,
                Name = userInfo.Name,
                UserName = userInfo.UserName,
                UserPwd = userInfo.UserPwd,
                Motto = userInfo.Motto,
                MainPagePhoto = userInfo.MainPagePhoto
            };


            return View( "UserInfo/EditUserInfo",model);




        }


        [Route("~/CMS/EditSelfUserInfo")]
        [Authorize]
        public async Task<IActionResult> EditSelfUserInfo()
        {

            Claim? claim = User.FindFirst("Id");
            if (claim == null)
            {
                var E_model = new ErrorInfo
                {
                    Message = "出错了",
                    Error = "没有找到Claim对应的Id"
                };
                return View("/Views/Error/Index", E_model);
            }

            int id = Convert.ToInt32(claim?.Value);
            var userInfo = await _userInfoService.FindByIdAsync(id);

            var model = new UserInfoModel
            {
                Id = id,
                Name = userInfo.Name,
                UserName = userInfo.UserName,
                UserPwd = userInfo.UserPwd,
                Motto = userInfo.Motto,
                MainPagePhoto = userInfo.MainPagePhoto
            };


            return View("UserInfo/EditUserInfo", model);




        }



        [Route("~/CMS/AddUserInfo")]
        public IActionResult AddUserInfo()
        {
          
            return View("UserInfo/AddUserInfo");
        }


        public async Task<IActionResult> EditWriterInfo(int id)
        {
            var writerInfo = await _writerInfoService.FindByIdAsync(id);
            if(writerInfo == null)
            {

                var E_model = new ErrorInfo
                {
                    Message = "出错了",
                    Error = "没有找到ID所对应的WriterInfo信息"
                };
                return View("/Views/Error/Index",E_model);

            }
            var model = new WriterInfoModel
            {
                Id = id,
                WriterName=writerInfo.WriterName
            };


            return View("WriterInfo/EditWriterInfo", model);




        }


        public async Task<IActionResult> EditBlogNews(int id)
        {
            var blogNews = await _blogNewsService.FindByIdAsync(id);
            if (blogNews == null)
            {

                var E_model = new ErrorInfo
                {
                    Message = "出错了",
                    Error = "没有找到ID所对应的WriterInfo信息"
                };
                return View("/Views/Error/Index", E_model);

            }
            


            return View("BlogNews/EditBlogNews", blogNews);




        }



        [Route("~/CMS/AddBlogNews")]
        public IActionResult AddBlogNews()
        {

            return View("BlogNews/AddBlogNews");
        }


        public async Task<IActionResult> EditTagInfo(int id)
        {
            var tagInfo = await _tagInfoService.FindByIdAsync(id);
            if (tagInfo == null)
            {

                var E_model = new ErrorInfo
                {
                    Message = "出错了",
                    Error = "没有找到ID所对应的WriterInfo信息"
                };
                return View("/Views/Error/Index", E_model);

            }
            var model = new TagInfoModel
            {
                Id = id,
                Name = tagInfo.Name
            };



            return View("TagInfo/EditTagInfo",model );




        }



        [Route("~/CMS/AddTagInfo")]
        public IActionResult AddTagInfo()
        {

            return View("TagInfo/AddTagInfo");
        }



        public async Task<IActionResult> EditComment(int id)
        {
            var comment= await _commentService.FindByIdAsync(id);
            if (comment == null)
            {

                var E_model = new ErrorInfo
                {
                    Message = "出错了",
                    Error = "没有找到ID所对应的WriterInfo信息"
                };
                return View("/Views/Error/Index", E_model);

            }
            var model = new CommentModel
            {
                Id = id,
                Content = comment.Content,
                SupportCount=comment.SupportCount
               
            };


            return View("Comment/EditComment", model);




        }



        [Route("~/CMS/AddComment")]
        public IActionResult AddComment()
        {

            return View("Comment/AddComment");
        }

    }
}
