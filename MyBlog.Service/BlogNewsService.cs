using MyBlog.IRepository;
using MyBlog.IService;
using MyBlog.Model;
using MyBlog.Model.ViewModels.Common;
using NetTaste;
using Newtonsoft.Json;
using Org.BouncyCastle.Crypto.Generators;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MyBlog.Service
{
    public class BlogNewsService:BaseService<BlogNews>,IBlogNewsService
    {
        private readonly IBlogNewsRepository _iBlogNewsRepository;   //readonly的值只能在构造方法内修改，但是不能在别的地方进行修改
        private readonly ITagInfoRepository _tagRepository;

        public BlogNewsService(IBlogNewsRepository iBlogNewsRepository,ITagInfoRepository tagRepository)
        {
            base._iBaseRepository = iBlogNewsRepository;
            _iBlogNewsRepository = iBlogNewsRepository;
            _tagRepository = tagRepository;



        }

        public  async Task<List<BlogNews>?> QueryByTagAsync(int TagId)
        {
            return await _iBlogNewsRepository.QueryByTagAsync(TagId);
        }



        public override  async Task<bool> CreateAsync(BlogNews blogNews)
        {

            var TagList = new List<TagInfo>();
            foreach(var tag in blogNews.Tags)
            {
                var Tag= await _tagRepository.FindByNameAsync(tag.Name);
                if(Tag == null)
                {
                    TagList.Add(tag);
                }
                else
                {
                    TagList.Add(Tag);
                }

            }
            blogNews.Tags = TagList;


            return await _iBlogNewsRepository.CreateAsync(blogNews);



        }

        public async Task<List<BlogNews>> QueryByNameAsync(string search)
        {
            return await _iBlogNewsRepository.QueryByNameAsync(search);
        }

        public async Task<List<BlogNews>> QueryByNameAsync(string SearchString, int CurrentPage, int PageSize, RefAsync<int> total)
        {
            return await _iBlogNewsRepository.QueryByNameAsync(SearchString, CurrentPage, PageSize, total);
        }

        public async Task<List<BlogNews>> QueryByTagAsync(int TagId, int CurrentPage, int PageSize, RefAsync<int> total)
        {
            return await _iBlogNewsRepository.QueryByTagAsync(TagId, CurrentPage, PageSize, total); 
        }

        public async Task<BlogNews> QueryByGUIDAsync(string GUID)
        {
            return await _iBlogNewsRepository.QueryByGUIDAsync(GUID);
        }

        public async Task<List<BlogNews>> QueryTopByBrowseCountAsync()
        {


            return await _iBlogNewsRepository.QueryTopByBrowseCountAsync();
        }

        public async Task<List<BlogNewsModel>> GetBlogNewsList(int page, int limit, RefAsync<int> total)
        {


            var blogNewss=await _iBlogNewsRepository.QueryAsync(page, limit, total);
            var blogNewsList = new List<BlogNewsModel>();
            foreach(var blogNews in blogNewss)
            {


                List<String> tagList=new List<String>();
                foreach (TagInfo tagInfo in blogNews.Tags)
                {
                    tagList.Add(tagInfo.Name);
                }
               
                var blogNewsModel = new BlogNewsModel
                {


                    Id = blogNews.Id,
                    GUID = blogNews.GUID,
                    Title = blogNews.Title,
                    Summary = blogNews.Summary,
                    Admirer_Total = blogNews.Admirers.Count(),
                    BrowseCount = blogNews.BrowseCount,
                    Comment_Total = blogNews.Comments.Count(),
                    CoverPhoto = blogNews.CoverPhoto,
                    Content = blogNews.Content,
                    Introduction = blogNews.Introduction,
                    LikeCount = blogNews.LikeCount,
                    Path = blogNews.Path,
                    Tags=String.Join(",", tagList),
                    Time=blogNews.Time,
                    Type=blogNews.Type,
                    WriterId=blogNews.WriterId

                };
                blogNewsList.Add(blogNewsModel);

            }
            return blogNewsList;
        }


        public override async Task<bool> UpdateAsync(BlogNews blogNews)
        {

            var pre_blogNews = await _iBlogNewsRepository.FindByIdAsync(blogNews.Id);

            var TagList = new List<TagInfo>();
            foreach (var tag in blogNews.Tags)
            {
                var Tag = await _tagRepository.FindByNameAsync(tag.Name);
                if (Tag == null)
                {
                    TagList.Add(tag);
                }
                else
                {
                    TagList.Add(Tag);
                }

            }
            blogNews.Tags = TagList;
            var update_blogNews = new BlogNews
            {
                Id = blogNews.Id,
                Summary = blogNews.Summary==null?pre_blogNews.Summary:blogNews.Summary,
                Content=blogNews.Content==null?pre_blogNews.Content:blogNews.Content,
                BrowseCount = blogNews.BrowseCount==null?pre_blogNews.BrowseCount:blogNews.BrowseCount,
                CoverPhotoId=blogNews.CoverPhoto==null?pre_blogNews.CoverPhotoId:blogNews.CoverPhotoId,
                CoverPhoto=blogNews.CoverPhoto==null?null:blogNews.CoverPhoto,
                LikeCount=blogNews.LikeCount==null?pre_blogNews.LikeCount:blogNews.LikeCount,
                Path=blogNews.Path==null?pre_blogNews.Path:blogNews.Path,
                Title=blogNews.Title==null?pre_blogNews.Title:blogNews.Title,
                Tags=blogNews.Tags==null?pre_blogNews.Tags:blogNews.Tags,
                Introduction=blogNews.Introduction==null?pre_blogNews.Introduction:blogNews.Introduction,
                Type=blogNews.Type==null?pre_blogNews.Type:blogNews.Type,
                Time = pre_blogNews.Time,
                WriterId =pre_blogNews.WriterId,
                GUID = pre_blogNews.GUID



            };



            return await _iBlogNewsRepository.UpdateAsync(update_blogNews);

        }

    }
}
