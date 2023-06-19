using MyBlog.IRepository;
using MyBlog.IService;
using MyBlog.Model;
using MyBlog.Model.ViewModels.Personal;
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


        private readonly IFollowRepository _followRepository;

        private readonly IFavoriteRepository _favoriteRepository;

        private readonly ILikeRepository _likeRepository;





        public UserInfoService(IUserInfoRepository userInfoRepository,IWriterInfoRepository writerInfoRepository,IPhotoRepository photoRepository,IFollowRepository followRepository,IFavoriteRepository favoriteRepository,ILikeRepository likeRepository)
        {
            base._iBaseRepository = userInfoRepository;
            _userInfoRepository = userInfoRepository;
            _writerInfoRepository = writerInfoRepository;
            _photoRepository = photoRepository; 
            _followRepository = followRepository;   
            _favoriteRepository = favoriteRepository;
            _likeRepository = likeRepository;

            
        }



        public async Task<bool>  register(UserInfo userInfo)
        {


            return await _userInfoRepository.CreateAsync(userInfo);



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

        public Task<bool> CreateFollowAsync( int UserId,int WriterId)
        {
            var follow = new Follow
            {
                UserId = UserId,
                WriterId = WriterId
            };
            return _followRepository.CreateAsync(follow);

        }

        public Task<bool> DeleteFollowAsync( int UserId,int WriterId)
        {

            return _followRepository.DeleteAsync(UserId,WriterId);

        }





        public async Task<bool> CreateLikeAsync(int UserId,int BlogId)
        {

            var like = new Like
            {
                UserId = UserId,
                BlogId = BlogId
            };
            


            return  await _likeRepository.CreateAsync(like);
        }

        public async Task<bool> DeleteLikeAsync(int UserId,int BlogId)
        {



            return await _likeRepository.DeleteAsync(UserId, BlogId);

        }




        public Task<bool> CreateFavoriteAsync(int UserId,int BlogId)
        {
            var favorite = new Favorite
            {
                UserId = UserId,
                BlogId = BlogId
            };
            return _favoriteRepository.CreateAsync(favorite);

        }

        public Task<bool> DeleteFavoriteAsync(int UserId,int BlogId)
        {

            return _favoriteRepository.DeleteByIdAsync(UserId, BlogId);

        }

        public Task<UserInfo?> FindByWriterIdAsync(int writerId)
        {
            return _userInfoRepository.FindByWriterIdAsync(writerId);

        }

        public async Task<PersonalViewModel> GetPersonalInfo(int id)
        {
            var userInfo=await _userInfoRepository.FindByIdAsync(id);
            if (userInfo == null)
            {
                var NotFoundModel = new PersonalViewModel
                {
                    Name = "该用户不存在",
                    Concerns = new List<UserInfo>(),
                    Favorites = new List<BlogNews>(),
                    MainPagePhoto = new Photo
                    {
                        Url = "~/photos/default_headphoto.png"
                    },
                    Motto = "这个人什么也没说呢_(:з」∠)_",
                    UserName = "该用户不存在",
                    WriterInfo = new WriterInfo
                    {
                        Blogs = new List<BlogNews>(),
                        Fans = new List<UserInfo>(),
                        WriterName = "该用户不存在"
                    },
                    WriterId = 0,
                    MainPagePhoto_id = 0
                };
                Console.WriteLine("该用户不存在");


                return NotFoundModel;
            }


            var concerns = new List<UserInfo>();
            foreach(var writerInfo in userInfo.Concerns)
            {
                var concern_user=await _userInfoRepository.FindByWriterIdAsync(writerInfo.Id);
                if(concern_user == null)
                {
                    concern_user = new UserInfo
                    {
                        Name = "NOT FOUND",
                        MainPagePhoto = new Photo
                        {
                            Url = "~/photos/default_headphoto.png"
                        },
                        WriterInfo = writerInfo,
                    };

                }
                concerns.Add(concern_user);

            }

            var model = new PersonalViewModel
            {
                Name = userInfo.Name,
                Concerns = concerns,
                Favorites = userInfo.Favorites,
                MainPagePhoto = userInfo.MainPagePhoto,
                Motto = userInfo.Motto,
                UserName = userInfo.UserName,
                WriterInfo =userInfo.WriterInfo,
                WriterId = userInfo.WriterId,
                MainPagePhoto_id = userInfo.MainPagePhoto_id,
            };


            return model;


        }
    }
}
