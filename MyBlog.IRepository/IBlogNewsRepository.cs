using MyBlog.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.IRepository
{
    public interface IBlogNewsRepository : IBaseRepository<BlogNews>
    {
        Task<List<BlogNews>>  QueryByNameAsync(string SearchString);
        Task<List<BlogNews>> QueryByTagAsync(int TagId);
        Task<List<BlogNews>> QueryByNameAsync(string SearchString,int CurrentPage,int PageSize,RefAsync<int>total);
        Task<List<BlogNews>> QueryByTagAsync(int TagId,int CurrentPage,int PageSize,RefAsync<int>total);

        Task<BlogNews> QueryByGUIDAsync(String GUID);

        Task<List<BlogNews>> QueryTopByBrowseCountAsync();

        Task<bool> UpdateLikeCountAsync(BlogNews update_blogNews);
    }
}
