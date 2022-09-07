using MyBlog.IRepository;
using MyBlog.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Repository
{

    //BaseRepository已经实现了所有的接口，所以直接继承BaseRepository再实现IWriterInfoRepository就不用再一个一个实现了
    public class WriterInfoRepository : BaseRepository<WriterInfo>,IWriterInfoRepository
    {
        
     
    }
}
