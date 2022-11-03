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

        public override async Task<bool> DeleteByIdAsync(int id)
        {
            var photo= await _photoRepository.FindByIdAsync(id);

            if (photo != null)
            {
                if (File.Exists(photo.FilePath))
                {
                    File.Delete(photo.FilePath);
                }
                else
                {
                    Console.WriteLine("找不到对应的图片");

                }
            }
            else
            {
                Console.WriteLine("数据库中未存在该图片");
                return true;

            }
            return await _photoRepository.DeleteByIdAsync(id);

        }
    }
}
