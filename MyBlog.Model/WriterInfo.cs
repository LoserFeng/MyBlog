using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;
namespace MyBlog.Model
{
    public class WriterInfo:BaseId
    {
        [SugarColumn(ColumnDataType ="nvarchar(12)")]
        public String? Name { get; set; }

        [SugarColumn(ColumnDataType ="nvarchar(16)")]
        public String? UserName { get; set; }
        [SugarColumn(ColumnDataType ="nvarchar(64)")]
        public String  UserPwd { get; set; }


    }
}
