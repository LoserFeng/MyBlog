using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Model
{
    public class Favorite
    {
        [SugarColumn(IsPrimaryKey = true)]
        public int UserId { get; set; }
        [SugarColumn(IsPrimaryKey = true)]
        public int BlogId { get; set; }
    }
}
