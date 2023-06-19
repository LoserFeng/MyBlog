using MyBlog.Model;
using MyBlog.Model.ViewModels.Personal;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.IService
{
    public interface IUserInfoService : IBaseService<UserInfo>
    {

        Task<bool> register(UserInfo userInfo);
        //new Task<UserInfo?> FindByIdAsync(int id);


        Task<bool> CheckInfoAsync( String name,String userName);
        Task<List<UserInfo>> QueryAllAsync(int page, int limit, RefAsync<int> total);



        Task<bool> CreateFollowAsync(int UserId,int WriterId);


        Task<bool> DeleteFollowAsync(int UserId,int WriterId);
        Task<bool> CreateFavoriteAsync(int UserId, int BlogId);
        Task<bool> DeleteFavoriteAsync(int UserId,int BlogId);
        Task<bool> CreateLikeAsync(int UserId, int BlogId);
        Task<bool> DeleteLikeAsync(int UserId, int BlogId);
        Task<UserInfo?> FindByWriterIdAsync(int writerId);
        Task<PersonalViewModel> GetPersonalInfo(int id);
    }
}
