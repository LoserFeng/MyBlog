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
    public interface IBlogNewsService : IBaseService<BlogNews>
    {
        Task<List<BlogNews>> QueryByNameAsync(string search);
        Task<List<BlogNews>> QueryByTagAsync(int TagId);


        Task<List<BlogNews>> QueryByNameAsync(string SearchString, int CurrentPage, int PageSize, RefAsync<int> total);
        Task<List<BlogNews>> QueryByTagAsync(int TagId, int CurrentPage, int PageSize, RefAsync<int> total);
        Task<BlogNews> QueryByGUIDAsync(String GUID);
        Task<List<BlogNews>> QueryTopByBrowseCountAsync();
        Task <List<BlogNewsModel>> GetBlogNewsList(int page, int limit, RefAsync<int> total);
    }
}
