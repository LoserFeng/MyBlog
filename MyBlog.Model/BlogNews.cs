using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace MyBlog.Model
{
    public class BlogNews : BaseId
    {
        //nvarchar 带中文比较好
        [SugarColumn(ColumnDataType ="nvarchar(30)")]
        public String ?Title { get; set; }
        [SugarColumn(ColumnDataType ="text")]
        public String? Content { get; set; }
        public DateTime? Time { get; set; }
        public int BrowseCount { get; set; }
        public int LikeCount { get; set; }


        public int TypeId { get; set; }
        public int WriiterId { get; set; }




        /// <summary>
        /// 导航查询，不映射到数据库
        /// </summary>
        [Navigate(NavigateType.OneToOne,nameof(Id))]
        public TypeInfo TypeInfo { get; set; }


        [Navigate(NavigateType.OneToOne,nameof(Id))]
        public WriterInfo WriterInfo { get; set; }

        



    }
}
