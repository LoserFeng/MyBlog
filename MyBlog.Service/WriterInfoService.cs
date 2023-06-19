using MyBlog.IRepository;
using MyBlog.IService;
using MyBlog.Model;
using MyBlog.Model.ViewModels.Common;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Service
{
    public class WriterInfoService:BaseService<WriterInfo>,IWriterInfoService
    {
        private readonly IWriterInfoRepository _iWriterInfoRepository;   //readonly的值只能在构造方法内修改，但是不能在别的地方进行修改

        public WriterInfoService(IWriterInfoRepository iWriterInfoRepository)
        {
            base._iBaseRepository = iWriterInfoRepository;
            _iWriterInfoRepository = iWriterInfoRepository;




        }
        public async Task<List<WriterInfoModel>> GetWriterInfoList(int page, int limit, RefAsync<int> total)
        {
            var writerInfos=await _iWriterInfoRepository.QueryAsync(page, limit, total);


            var writerInfoList = new List<WriterInfoModel>();


            foreach(var writerInfo in writerInfos)
            {

                WriterInfoModel model = new WriterInfoModel
                {
                    Id = writerInfo.Id,
                    WriterName = writerInfo.WriterName,
                    Blog_Total = writerInfo.Blogs.Count(),
                    Fan_Total = writerInfo.Fans.Count(),
                    Browse_Total = writerInfo.Blogs.Sum(blog => blog.BrowseCount)
                };
                writerInfoList.Add(model);



            }



            return writerInfoList;

        }

        public override async Task<bool> UpdateAsync(WriterInfo writerInfo)
        {

            var pre_writerInfo = await _iWriterInfoRepository.FindByIdAsync(writerInfo.Id);
            var update_writerInfo = new WriterInfo
            {
                Id=writerInfo.Id,
                WriterName=writerInfo.WriterName==null?pre_writerInfo.WriterName:writerInfo.WriterName
            };



            return await _iWriterInfoRepository.UpdateAsync(update_writerInfo);

        }



    }
}
