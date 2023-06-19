using MyBlog.IRepository;
using MyBlog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Repository
{
    public class FollowRepository:BaseRepository<Follow>,IFollowRepository
    {
        public async Task<bool>DeleteAsync(int UserId,int WriterId)
        {
            var res=await base.Context
                .Deleteable<Follow>(f => f.UserId == UserId && f.WriterId == WriterId)
                .ExecuteCommandAsync();

            return res == 1;



        }

        public override async Task<bool>CreateAsync(Follow follow)
        {
            var res = await base.Context.Insertable(follow).ExecuteCommandAsync();
            return res == 1;
        }

    }
}
