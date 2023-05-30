using MyBlog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.IRepository
{
    public interface ILikeRepository:IBaseRepository<Like>
    {

        Task<bool> DeleteAsync(int UserId, int BlogId);
    }
}
