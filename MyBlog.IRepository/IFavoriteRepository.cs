﻿using MyBlog.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.IRepository
{
    public interface IFavoriteRepository:IBaseRepository<Favorite>
    {
        Task<bool> DeleteByIdAsync(int UserId,int BlogId);
    }
}
