using MyBlog.IRepository;
using MyBlog.IService;
using MyBlog.Model;
using MyBlog.Model.ViewModels.Common;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Service
{
    public class TagInfoService:BaseService<TagInfo>,ITagInfoService
    {

        private readonly ITagInfoRepository _tagInfoRepository;   //readonly的值只能在构造方法内修改，但是不能在别的地方进行修改

        public TagInfoService(ITagInfoRepository tagInfoRepository)
        {
            base._iBaseRepository = tagInfoRepository;
            _tagInfoRepository = tagInfoRepository;


        }

        public async Task<TagInfo?> FindByNameAsync(string name)
        {
            return await _tagInfoRepository.FindByNameAsync(name);

        }

        public async Task<List<TagInfoModel>> GetTagInfoList(int page, int limit, RefAsync<int> total)
        {
            var TagInfoList=new List<TagInfoModel>();

            var tagInfos = await _tagInfoRepository.QueryAsync(page, limit, total);


            foreach(var tag in tagInfos)
            {
                TagInfoList.Add(new TagInfoModel
                {
                    Blog_Total = tag.Blogs.Count(),
                    Browse_Total = tag.Blogs.Sum(b => b.BrowseCount),
                    Id = tag.Id,
                    Like_Total = tag.Blogs.Sum(b => b.LikeCount),
                    Name = tag.Name
                });

            }

            return TagInfoList;
            
        }
    }
}
