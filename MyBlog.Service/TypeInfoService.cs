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
    public class TypeInfoService:BaseService<TagInfo>,ITypeInfoService
    {

        private readonly ITypeInfoRepository _iTypeInfoRepository;   //readonly的值只能在构造方法内修改，但是不能在别的地方进行修改

        public TypeInfoService(ITypeInfoRepository iTypeInfoRepository)
        {
            base._iBaseRepository = iTypeInfoRepository;
            _iTypeInfoRepository = iTypeInfoRepository;


        }
    }
}
