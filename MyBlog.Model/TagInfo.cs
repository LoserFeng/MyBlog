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


        [Navigate(typeof(Classify), nameof(Classify.TagId), nameof(Classify.BlogId))]
        public List<BlogNews> Blogs { get; set; }
        

    }
}
