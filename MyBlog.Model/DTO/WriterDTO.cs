﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Model.DTO
{
    public class WriterDTO   //返回的前端数据没有passwd
    {
        public int Id { get; set; }
        public string Name { get; set; }    
        public string UserName { get; set; }
        
    }
}
