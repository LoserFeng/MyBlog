using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MyBlog.IRepository;
using SqlSugar;
using SqlSugar.IOC;

namespace MyBlog.Repository
{
    public class BaseRepository<TEntity> : SimpleClient<TEntity>, IBaseRepository<TEntity> where TEntity : class, new()
    {

        public BaseRepository()
        {

        }

        public BaseRepository(ISqlSugarClient? context =null) : base(context)    //继承父类的构造函数  :base(参数）
        {
            base.Context = DbScoped.Sugar;

        }

        public async Task<bool> CreateAsync(TEntity entity)
        {
            return await base.InsertAsync(entity);
        }

        public Task<bool> DeleteByIdAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> FindByNameAsync(string name)  
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> QueryAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> func)
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> QueryAsync(int page, int size, RefAsync<int> total)
        {
            throw new NotImplementedException();
        }

        public Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> func, int page, int size, RefAsync<int> total)
        {
            throw new NotImplementedException();
        }
    }
}
