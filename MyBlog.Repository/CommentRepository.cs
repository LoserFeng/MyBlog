using MyBlog.IRepository;
using MyBlog.Model;
using SqlSugar;
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
            var res = await base.Context.DeleteNav<Comment>(c => c.GUID == GUID)
                    .Include(c => c.Comments)
                    .ExecuteCommandAsync();


            return res;
        }



        public override async Task<bool> DeleteByIdAsync(int id)
        {
            var res = await base.Context.DeleteNav<Comment>(c => c.Id==id)
                    .Include(c => c.Comments)
                    .ExecuteCommandAsync();


            return res;
        }

        public async Task<List<Comment>> GetCommentsByBlogId(int BlogId)
        {
            var list = await base.Context.Queryable<Comment>()
                .Includes(c => c.UserInfo, c => c.MainPagePhoto)
                .Includes(c => c.Comments, c => c.UserInfo, c => c.MainPagePhoto)
                .Where(c => c.BlogId == BlogId)
                .ToListAsync();



            return list;
        }

        public async Task<List<Comment>> QueryAsync(int blogId, int page, int size, RefAsync<int> total)
        {
            var list = await base.Context.Queryable<Comment>()
                .Includes(c => c.UserInfo, c => c.MainPagePhoto)
                .Includes(c => c.Comments, c => c.UserInfo, c => c.MainPagePhoto)
                .Where(c => c.BlogId == blogId)
                .ToPageListAsync(page, size, total);    



            return list;
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
