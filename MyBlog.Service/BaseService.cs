using MyBlog.IRepository;
using MyBlog.IService;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Service
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class, new()
    {

        //从子类的构造函数中传入
        protected IBaseRepository<TEntity> _iBaseRepository;

        public async Task<bool> CreateAsync(TEntity entity)
        {
            return await _iBaseRepository.CreateAsync(entity);
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            return await _iBaseRepository.DeleteByIdAsync(id);
        }


        //这里用虚方法，还要做修改
        public async Task<TEntity> FindByIdAsync(int id)
        {

            //Dto(Data Transport object) -> 用户数据，不能把密码也返回到前端 导航查询
            return await _iBaseRepository.FindByIdAsync(id);
        }

        public async Task<IEnumerable<TEntity>> QueryAllAsync()
        {
            return await _iBaseRepository.QueryAllAsync();
        }

        public async Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> func)
        {
            return await _iBaseRepository.QueryAsync(func);
        }

        public async Task<List<TEntity>> QueryAsync(int page, int size, RefAsync<int> total)
        {
            return await _iBaseRepository.QueryAsync(page, size, total);    
        }

        public async Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> func, int page, int size, RefAsync<int> total)
        {
            return await _iBaseRepository.QueryAsync(func, page, size, total);
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            return await _iBaseRepository.UpdateAsync(entity);
        }
    }
}
