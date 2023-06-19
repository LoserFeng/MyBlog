using MyBlog.IRepository;
using MyBlog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Repository
{
    public class LikeRepository : BaseRepository<Like>, ILikeRepository
    {
        public async Task<bool> DeleteAsync(int UserId, int BlogId)
        {
            var res = await base.Context.Deleteable<Like>(l => l.UserId == UserId && l.BlogId == BlogId).ExecuteCommandAsync();

            return res == 1;

        }
    }
}
