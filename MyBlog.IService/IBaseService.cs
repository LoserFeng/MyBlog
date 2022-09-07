﻿using MyBlog.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.IService
{
    public interface IBaseService<TEntity>:IBaseRepository<TEntity> where TEntity:class,new() 
    {


    }
}
