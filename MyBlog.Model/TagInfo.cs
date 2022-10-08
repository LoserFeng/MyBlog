using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;


namespace MyBlog.Model
{
    public class TagInfo:BaseId
    {
        [SugarColumn(ColumnDataType = "nvarchar(12)")]
        public string Name { get; set; }


        [Navigate(NavigateType.OneToMany, nameof(BlogNews.TypeId))]
        public List<BlogNews> blogs { get; set; }
        

    }
}
