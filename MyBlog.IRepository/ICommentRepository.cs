using MyBlog.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.IRepository
{
	public interface ICommentRepository : IBaseRepository<Comment>
	{
		Task<bool> DeleteByGUIDAsync(string GUID);
        Task<List<Comment>> GetCommentsByBlogId(int BlogId);

        Task<Comment> QueryByGUIDAsync(string GUID);

        Task<List<Comment>> QueryAsync(int blogId,int page, int size, RefAsync<int> total);


    }
}
