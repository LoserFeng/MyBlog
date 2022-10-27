using MyBlog.IRepository;
using MyBlog.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Repository
{
    public class BlogNewsRepository:BaseRepository<BlogNews>,IBlogNewsRepository
    {

/*        public async override Task<List<BlogNews>> QueryAllAsync()
        {
            return await base.Context.Queryable<BlogNews>()
                .Mapper(c => c.TypeInfo, c => c.TypeId, c => c.TypeInfo.Id)
                .Mapper(c => c.WriterInfo, c => c.WriterId, c => c.WriterInfo.Id)
                .ToListAsync();
        }



        public override async Task<List<BlogNews>> QueryAllAsync(Expression<Func<BlogNews, bool>> func)
        {
            return await base.Context.Queryable<BlogNews>()
                .Where(func)
                .Mapper(c => c.TypeInfo, c => c.TypeId, c => c.TypeInfo.Id)
                .Mapper(c => c.WriterInfo, c => c.WriterId, c => c.WriterInfo.Id)
                .ToListAsync();
        }*/

        public async override Task<List<BlogNews>> QueryAsync(int page, int size, RefAsync<int> total)
        {

            /* return await base.QueryAsync(page, size, total);*/

            return await base.Context.Queryable<BlogNews>()
                .Mapper(c => c.WriterInfo, c => c.WriterId, c => c.WriterInfo.Id)
                .ToPageListAsync(page, size,total);

        }
        public async override Task<List<BlogNews>> QueryAsync(Expression<Func<BlogNews, bool>> func, int page, int size, RefAsync<int> total)
        {

            /* return await base.QueryAsync(page, size, total);*/

            return await base.Context.Queryable<BlogNews>()
                .Where(func)
                .Mapper(c=>c.WriterInfo,c=>c.WriterId,c=>c.WriterInfo.Id)
                .ToPageListAsync(page, size, total);

        }




        public override async Task<bool> CreateAsync(BlogNews blogNews)
        {



            List<BlogNews> list = new List<BlogNews>() { blogNews };
            return await base.Context.InsertNav(list)
                .Include(c => c.CoverPhoto)
                .Include(c => c.Tags)
                .Include(c => c.Admirers)
                .ExecuteCommandAsync();
        }



        public async Task<BlogNews?> QueryAsyncById(int id)
        {

            var list = await base.Context.Queryable<BlogNews>()
                                .Includes(c => c.WriterInfo)
                                .Includes(c => c.Tags)
                                .Includes(c => c.CoverPhoto)
                                .Includes(c => c.Admirers)
                                .Where(c => c.Id == id).ToListAsync();



            return list.FirstOrDefault();
        }





        public override async Task<List<BlogNews>> QueryAllAsync()
        {

            var list = await base.Context.Queryable<BlogNews>()
                                .Includes(c => c.WriterInfo)
                                .Includes(c => c.Tags)
                                .Includes(c => c.CoverPhoto)
                                .Includes(c => c.Admirers)
                                .ToListAsync();



            return list;
        }




        public async Task<List<BlogNews>> QueryByTagAsync(int TagId)
        {
            var list = await base.Context.Queryable<BlogNews>()
                    .Includes(c => c.WriterInfo)
                    .Includes(c => c.Tags)
                    .Includes(c => c.CoverPhoto)
                    .Includes(c => c.Admirers)
                    .Where(c=>c.Tags.Any(tag=>tag.Id==TagId))
                    .ToListAsync();



            return list;
        }

        public async Task<List<BlogNews>> QueryByNameAsync(string SearchString)
        {
            var list = await base.Context.Queryable<BlogNews>()
                .Includes(c => c.WriterInfo)
                .Includes(c => c.Tags)
                .Includes(c => c.CoverPhoto)
                .Includes(c => c.Admirers)
                .Where(c=>c.Title.Contains(SearchString))
                .ToListAsync();

            return list;
        }

        public async Task<List<BlogNews>> QueryByTagAsync(int TagId, int CurrentPage, int PageSize, RefAsync<int> total)
        {




            var list = await base.Context.Queryable<BlogNews>()
                .Includes(c => c.WriterInfo)
                .Includes(c => c.Tags)
                .Includes(c => c.CoverPhoto)
                .Includes(c => c.Admirers)
                .Where(c => c.Tags.Any(tag => tag.Id == TagId))
                .ToPageListAsync(CurrentPage,PageSize,total);



            return list;
        }



        public async Task<List<BlogNews>> QueryByNameAsync(string SearchString, int CurrentPage, int PageSize, RefAsync<int> total)
        {

            var list = await base.Context.Queryable<BlogNews>()
                .Includes(c => c.WriterInfo)
                .Includes(c => c.Tags)
                .Includes(c => c.CoverPhoto)
                .Includes(c => c.Admirers)
                .Where(c => c.Title.Contains(SearchString))
                .ToPageListAsync(CurrentPage, PageSize, total);

            return list;

        }

        public async Task<BlogNews> QueryByGUIDAsync(String GUID)
        {
            var list = await base.Context.Queryable<BlogNews>()
                    .Includes(c => c.WriterInfo)
                    .Includes(c => c.Tags)
                    .Includes(c => c.CoverPhoto)
                    .Includes(c => c.Admirers)
                    .Includes(c=>c.Comments,c=>c.UserInfo,c=>c.MainPagePhoto)
                    .Includes(c=>c.Comments,c=>c.Comments,c=>c.UserInfo,c=>c.MainPagePhoto)

                    .Where(c=>c.GUID==GUID).ToListAsync();



            return list.FirstOrDefault();
        }

        public async Task<List<BlogNews>> QueryTopByBrowseCountAsync()
        {

            var list = await base.Context.Queryable<BlogNews>()
                .Includes(c => c.WriterInfo)
                .OrderBy(c=>c.BrowseCount,OrderByType.Desc)
                .Take(10)
                .ToListAsync();

            return list;
        }
    }
}
