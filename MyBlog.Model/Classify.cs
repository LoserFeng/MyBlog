using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Model
{
    public class Classify
    {

        [SugarColumn(IsPrimaryKey = true)]
        public int TagId { get; set; }
        [SugarColumn(IsPrimaryKey = true)]
        public int BlogId { get; set; }
    }
}
