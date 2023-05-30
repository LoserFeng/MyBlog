using MyBlog.IRepository;
using MyBlog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Repository
{
    public class FavoriteRepository : BaseRepository<Favorite>, IFavoriteRepository
    {
        public async Task<bool> DeleteByIdAsync(int UserId, int BlogId)
        {
            
                
                
            var res=await base.Context
                .Deleteable<Favorite>(f => f.UserId == UserId && f.BlogId == BlogId)
                .ExecuteCommandAsync();

            return res == 1;

        }
    }
}
