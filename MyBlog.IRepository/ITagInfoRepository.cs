using MyBlog.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.IRepository
{
    public interface ITagInfoRepository:IBaseRepository<TagInfo>
    {

        Task<TagInfo?> FindByNameAsync(string name);
      

    }
}
