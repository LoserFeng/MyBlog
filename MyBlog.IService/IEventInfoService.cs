using MyBlog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.IService
{
    public interface IEventInfoService:IBaseService<EventInfo>
    {
        Task<int>CreateReturnIdAsync(EventInfo eventInfo);   
    }
}
