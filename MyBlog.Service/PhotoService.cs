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
    public class PhotoService:BaseService<Photo>,IPhotoService
    {

        private readonly IPhotoRepository _photoRepository;

        public PhotoService(IPhotoRepository photoRepository)
        {
           
            base._iBaseRepository = photoRepository;
            _photoRepository = photoRepository;
            
        }
    }
}
