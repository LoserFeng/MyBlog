using MyBlog.IRepository;
using MyBlog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Repository
{
    public class TagRepository : BaseRepository<TagInfo>, ITagRepository
    {
        public async Task<TagInfo?> FindByNameAsync(string name)
        {

            var list = await base.Context.Queryable<TagInfo>()
                    .Where(c => c.Name == name).ToListAsync();


            return list.FirstOrDefault();
        }
    }
}
