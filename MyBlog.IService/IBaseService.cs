﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.IService
{
    public interface IBaseService<TEntity>where TEntity:class,new() 
    {


    }
}
