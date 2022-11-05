using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Model
{
    public class UserInfo:BaseId
    {

        [SugarColumn(ColumnDataType = "nvarchar(32)")]
        public string Name { get; set; }

        [SugarColumn(ColumnDataType = "nvarchar(32)")]
        public string UserName { get; set; }
        [SugarColumn(ColumnDataType = "nvarchar(64)")]
        public string UserPwd { get; set; }

        public int WriterId { get; set; }

        [Navigate(NavigateType.OneToOne, nameof(WriterId))]
        public WriterInfo  WriterInfo{ get; set; }

        [Navigate(typeof(Follow), nameof(Follow.UserId), nameof(Follow.WriterId))]
        public List<WriterInfo> Concerns { get; set; }


        [Navigate(typeof(Favorite), nameof(Favorite.UserId), nameof(Favorite.BlogId))]
        public List<BlogNews> Favorites { get; set; }



        //接下来是一些和自己主页主题相关的东西

        public String Motto { get; set; }


        public int MainPagePhoto_id { get; set; }

        [Navigate(NavigateType.OneToOne, nameof(MainPagePhoto_id))]
        public Photo MainPagePhoto { get; set; }




        [Navigate(NavigateType.OneToMany, nameof(EventInfo.UserId))]
        public List<EventInfo> Events { get; set; }



    }
}
