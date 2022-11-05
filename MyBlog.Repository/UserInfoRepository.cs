using MyBlog.IRepository;
using MyBlog.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
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
                .Include(c=>c.Events)
                .ExecuteCommandAsync();

        }


        public override async  Task<List<UserInfo>> QueryAsync(int page, int size, RefAsync<int> total)
        {
            var list = await base.Context.Queryable<UserInfo>()
                    .Includes(c => c.MainPagePhoto)
                    .ToPageListAsync(page,size,total);
            return list;

        }



        public async Task<UserInfo?>QueryAsyncById(int id)
        {


            var list = await base.Context.Queryable<UserInfo>()
                                .Includes(c => c.WriterInfo)
                                .Includes(c => c.Concerns)
                                .Includes(c => c.Favorites)
                                .Includes(c => c.MainPagePhoto)
                                .Includes(c=>c.Events)
                                .Where(c => c.Id == id).ToListAsync();

            var userInfo = list.FirstOrDefault();
            if (userInfo == null)
            {
                return null;
            }
            userInfo.Events = userInfo.Events.OrderByDescending(e => e.Time).ToList();

            return userInfo;



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


        public override async Task<bool> UpdateAsync(UserInfo userInfo)
        {


            var res = await base.Context.UpdateNav<UserInfo>(userInfo)
                .Include(c => c.MainPagePhoto).ExecuteCommandAsync();

            return res;
        }

        public override  async Task<bool> DeleteByIdAsync(int id)
        {
        
            var res = await base.Context.DeleteNav<UserInfo>(u => u.Id == id)
                .Include(c => c.MainPagePhoto)
                .Include(c=>c.WriterInfo)
                .Include(c=>c.WriterInfo)
                .ThenInclude(c=>c.Fans,new DeleteNavOptions()
                {
                    ManyToManyIsDeleteA=true
                })
                .Include(c=>c.Favorites,new DeleteNavOptions()
                {
                    ManyToManyIsDeleteA= true
                })
                .Include(c=>c.Concerns,new DeleteNavOptions()
                {
                    ManyToManyIsDeleteA=true
                })
                .Include(c=>c.Events)
                .ExecuteCommandAsync();
            return res;

        }







    }
}
