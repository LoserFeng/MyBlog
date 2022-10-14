using MyBlog.Model;
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
    }
}
