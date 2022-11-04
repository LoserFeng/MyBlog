using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Model
{
    public class EventInfo:BaseId
    {











        [SugarColumn(ColumnDataType = "ntext")]
        public string EventContent { get; set; }




        public DateTime Time { get; set; }







    }





}
