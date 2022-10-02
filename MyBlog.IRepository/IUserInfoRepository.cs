using MyBlog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.IRepository
{
    public interface IUserInfoRepository : IBaseRepository<UserInfo>
    {
        new Task<bool> CreateAsync(UserInfo userInfo);
        Task<UserInfo?> QueryAsyncById(int id);


        Task<UserInfo?>FindByNameAsync(string name);


        Task<UserInfo?> FindByUserNameAsync(string userName);






    }
}
