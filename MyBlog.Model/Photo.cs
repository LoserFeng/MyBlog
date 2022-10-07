using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MyBlog.Model;
public class Photo:BaseId {



    /// <summary>
    /// 作品标题
    /// </summary>


    [SugarColumn(ColumnDataType = "nvarchar(30)")]
    public string FileName { get; set; }



    public string Url { get; set; }
    public string FilePath { get; set; }

    /// <summary>
    /// 高度
    /// </summary>

    public DateTime CreateTime { get; set; }


}