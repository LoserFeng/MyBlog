using MyBlog.IRepository;
using MyBlog.IService;
using MyBlog.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Service
{
    public class UserInfoService : BaseService<UserInfo>, IUserInfoService
    {
        private readonly IUserInfoRepository _userInfoRepository;
        private readonly IWriterInfoRepository _writerInfoRepository;

        private readonly IPhotoRepository _photoRepository;




        public UserInfoService(IUserInfoRepository userInfoRepository,IWriterInfoRepository writerInfoRepository,IPhotoRepository photoRepository)
        {
            base._iBaseRepository = userInfoRepository;
            _userInfoRepository = userInfoRepository;
            _writerInfoRepository = writerInfoRepository;
            _photoRepository = photoRepository; 

            
        }



        public async Task<bool>  register(UserInfo userInfo)
        {


            return await _userInfoRepository.CreateAsync(userInfo);



        }




        public override  async Task<UserInfo?> FindByIdAsync(int id)
        {



            return await _userInfoRepository.QueryAsyncById(id);
        }

        public async Task<bool> CheckInfoAsync(string name, string userName)
        {


            var user1 = await _userInfoRepository.FindByNameAsync(name);
            var user2 = await _userInfoRepository.FindByUserNameAsync(userName);

            if(user1 == null && user2 == null)
            {
                return true;
            }
            return false;
        }

        public async Task<List<UserInfo>> QueryAllAsync(int page, int limit, RefAsync<int> total)
        {

            return await _userInfoRepository.QueryAsync(page, limit, total);

        }


        public override async Task<bool> UpdateAsync(UserInfo userInfo)
        {

            var pre_userInfo=await _userInfoRepository.FindByIdAsync(userInfo.Id);
            var update_userInfo = new UserInfo
            {
                Id = userInfo.Id,
                MainPagePhoto_id = userInfo.MainPagePhoto == null ? pre_userInfo!.MainPagePhoto_id : 0,
                MainPagePhoto = userInfo.MainPagePhoto,
                Name = userInfo.Name == null ? pre_userInfo!.Name : userInfo.Name,
                UserName = userInfo.UserName == null ? pre_userInfo.UserName : userInfo.UserName,
                UserPwd = userInfo.UserPwd == null ? pre_userInfo!.UserPwd : userInfo.UserPwd,
                Motto = userInfo.Motto == null ? pre_userInfo!.Motto : userInfo.Motto,

                WriterId = pre_userInfo!.WriterId


            };



            return await _userInfoRepository.UpdateAsync(update_userInfo);

        }


/*        public override async Task<bool> DeleteByIdAsync (int id)
        {

            return await _userInfoRepository.DeleteByIdAsync(id);
        }*/
    }
}
