using MyBlog.Model;
using MyBlog.Model.ViewModels.Common;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.IService
{
    public interface ITagInfoService : IBaseService<TagInfo>
    {

        Task<List<TagInfoModel>> GetTagInfoList(int page, int limit, RefAsync<int> total);

        Task<TagInfo?> FindByNameAsync(string name);


        
    }
}
