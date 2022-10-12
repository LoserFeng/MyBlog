using MyBlog.IRepository;
using MyBlog.IService;
using MyBlog.Model;

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
        private readonly ITagRepository _tagRepository;

        public BlogNewsService(IBlogNewsRepository iBlogNewsRepository,ITagRepository tagRepository)
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











    }
}
