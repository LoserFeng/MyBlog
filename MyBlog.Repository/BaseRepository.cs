using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MyBlog.IRepository;
using MyBlog.Model;
using SqlSugar;
using SqlSugar.IOC;

namespace MyBlog.Repository
{
    public class BaseRepository<TEntity> : SimpleClient<TEntity>, IBaseRepository<TEntity> where TEntity : class, new()
    {


        public BaseRepository(ISqlSugarClient? context =null) : base(context)    //继承父类的构造函数  :base(参数）
        {
            
            base.Context = DbScoped.Sugar;    //base.Context 就是官方栗子的db


            //创建过一次之后就不用重复创建了

            //创建数据库
            base.Context.DbMaintenance.CreateDatabase();
            //创建表
            base.Context.CodeFirst.SetStringDefaultLength(200).InitTables(
                typeof(TagInfo),
                typeof(WriterInfo),
                typeof(BlogNews),
                typeof(UserInfo),
                typeof(Photo),
                typeof(Favorite),
                typeof(Follow),
                typeof(Classify)
                );



        }

        public async Task<bool> CreateAsync(TEntity entity)
        {
            return await base.InsertAsync(entity);
        }

        public virtual async Task<bool> DeleteByIdAsync(int id)
        {
            return await base.DeleteByIdAsync(id);
        }

        public override async Task<bool> UpdateAsync(TEntity entity)
        {
            return await base.UpdateAsync(entity);
        }

        //导航查询
        public virtual async Task<TEntity> FindByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

/*        public async Task<IEnumerable<TEntity>> FindByNameAsync(string name)  
        {
            
        }*/

        public virtual async Task<List<TEntity>> QueryAllAsync()
        {
            return await GetListAsync();
        }

        public virtual async Task<List<TEntity>> QueryAllAsync(Expression<Func<TEntity, bool>> func)
        {
            return await base.GetListAsync(func);
        }

        public virtual async Task<List<TEntity>> QueryAsync(int page, int size, RefAsync<int> total)    //RefAsync<int>可以简单理解为int,这么搞一个类是为了解决ref out 不支持异步的问题
        {
            return await base.Context.Queryable<TEntity>().ToPageListAsync(page,size,total);
        }

        public virtual async Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> func, int page, int size, RefAsync<int> total)
        {
            return await base.Context.Queryable<TEntity>().Where(func).ToPageListAsync(page, size, total);
        }

        public virtual async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> func)
        {
            return await base.GetSingleAsync(func);
        }
    }
}
