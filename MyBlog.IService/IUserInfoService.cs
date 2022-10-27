using MyBlog.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.IService
{
    public interface IUserInfoService : IBaseService<UserInfo>
    {

        Task<bool> register(UserInfo userInfo);

        Task<UserInfo?> FindAsync(int id);


        Task<bool> CheckInfoAsync( String name,String userName);
        Task<List<UserInfo>> QueryAllAsync(int page, int limit, RefAsync<int> total);
    }
}
