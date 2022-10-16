using MyBlog.IRepository;
using MyBlog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Repository
{
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {


        public async Task<bool> DeleteByGUIDAsync(string GUID)
        {
            var res = await base.Context.Deleteable<Comment>()
                    .Where(c => c.GUID == GUID).ExecuteCommandAsync();

            if (res != 1)
            {
                return false;
            }


            return true;
        }

        public async Task<Comment> QueryByGUIDAsync(string GUID)
        {
            var list = await base.Context.Queryable<Comment>()
                .Includes(c=>c.UserInfo,c=>c.MainPagePhoto)
                .Includes(c=>c.Comments,c=>c.UserInfo,c=>c.MainPagePhoto)
                .Where(c => c.GUID == GUID)
                .ToListAsync();



            return list.FirstOrDefault();
        }




    }
}
