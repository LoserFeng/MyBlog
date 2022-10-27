using MyBlog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.IService
{
    public interface ICommentService:IBaseService<Comment>
    {
        Task<bool> DeleteByGUIDAsync(string GUID);

        Task<Comment>QueryByGUIDAsync(String GUID);


        Task<List<Comment>> GetSocietyAsync();

    }
}
