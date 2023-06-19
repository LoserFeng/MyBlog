using MyBlog.IRepository;
using MyBlog.IService;
using MyBlog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Service
{
    public class EventInfoService : BaseService<EventInfo>, IEventInfoService
    {

        private readonly IEventInfoRepository _eventInfoRepository;   //readonly的值只能在构造方法内修改，但是不能在别的地方进行修改


        public EventInfoService(IEventInfoRepository eventInfoRepository)
        {
            base._iBaseRepository= eventInfoRepository;
            _eventInfoRepository= eventInfoRepository;


        }
        public async Task<int> CreateReturnIdAsync(EventInfo eventInfo)
        {
            eventInfo.Year = eventInfo.Time.Year;
            eventInfo.Month = eventInfo.Time.Month;
            eventInfo.Day = eventInfo.Time.Day;
            return await _eventInfoRepository.CreateReturnIdAsync(eventInfo);   
        }

        public override async Task<bool> CreateAsync(EventInfo eventInfo)
        {

            eventInfo.Year = eventInfo.Time.Year;
            eventInfo.Month = eventInfo.Time.Month;
            eventInfo.Day = eventInfo.Time.Day;

            return await _eventInfoRepository.CreateAsync(eventInfo);
        }
    }
}
