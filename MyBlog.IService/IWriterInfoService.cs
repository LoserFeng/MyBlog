using MyBlog.Model;
using MyBlog.Model.ViewModels.Common;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.IService
{
    public interface IWriterInfoService:IBaseService<WriterInfo>
    {
        Task<List<WriterInfoModel>> GetWriterInfoList(int page, int limit,RefAsync<int>total);

    }
}
