using MyBlog.IRepository;
using MyBlog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Repository
{
    public class EventInfoRepository:BaseRepository<EventInfo>, IEventInfoRepository
    {


        public async Task<int> CreateReturnIdAsync(EventInfo eventInfo)
        {

            return await base.Context
                .Insertable<EventInfo>(eventInfo)
                .ExecuteReturnIdentityAsync();

        }
    }
}
