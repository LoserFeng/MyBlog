using MyBlog.IRepository;
using MyBlog.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Repository
{
    public class TagInfoRepository : BaseRepository<TagInfo>, ITagInfoRepository
    {
        public async Task<TagInfo?> FindByNameAsync(string name)
        {

            var list = await base.Context.Queryable<TagInfo>()
                    .Where(c => c.Name == name).ToListAsync();


            return list.FirstOrDefault();
        }


        public override  async Task<List<TagInfo>>QueryAsync(int page,int limit, RefAsync<int> total)
        {
            var list=await base.Context.Queryable<TagInfo>()
                .Includes(t=>t.Blogs)
                .ToPageListAsync(page, limit, total);
            return list;
        }



    }
}
