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




        public int UserId { get; set; }







        [SugarColumn(ColumnDataType = "ntext")]
        public string EventContent { get; set; }




        public DateTime Time { get; set; }

        public int Year { get; set; }

        public int Month { get;set; }
        public int Day { get; set; }









    }





}
