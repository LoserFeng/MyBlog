using MyBlog.IRepository;
using MyBlog.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyBlog.Repository
{
    public class UserInfoRepository:BaseRepository<UserInfo>,IUserInfoRepository
    {


        public new async Task<bool> CreateAsync(UserInfo userInfo)
        {

            List<UserInfo> list= new List<UserInfo>() { userInfo };
            return await base.Context.InsertNav(list)
                .Include(c=>c.MainPagePhoto)
                .Include(c => c.Favorites)
                .Include(c => c.Concerns)
                .Include(c => c.WriterInfo)
                .ExecuteCommandAsync();

        }



        public async Task<UserInfo?>QueryAsyncById(int id)
        {


            var list = await base.Context.Queryable<UserInfo>()
                                .Includes(c => c.WriterInfo)
                                .Includes(c => c.Concerns)
                                .Includes(c => c.Favorites)
                                .Includes(c => c.MainPagePhoto)
                                .Where(c => c.Id == id).ToListAsync();



            return list.FirstOrDefault();



        }
        public async  Task<UserInfo?> FindByNameAsync(string name)
        {

            var list = await base.Context.Queryable<UserInfo>()
                    .Where(c => c.Name==name).ToListAsync();


            return list.FirstOrDefault();


        }


        public async Task<UserInfo ?>FindByUserNameAsync(string userName){

            var list = await base.Context.Queryable<UserInfo>()
                     .Where(c => c.UserName == userName).ToListAsync();
            return list.FirstOrDefault();
        }










    }
}
