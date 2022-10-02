using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Model
{
    public class WriterInfo:BaseId
    {


        //吐了，必须额外要加一个属性，否则不给过，会报Sequence contains no elements的错误，这个导航插入也太脑残了。。。
        public String WriterName { get; set; }


        [Navigate(typeof(Follow), nameof(Follow.WriterId), nameof(Follow.UserId))]
        public List<UserInfo> Fans { get; set; }

        [Navigate(NavigateType.OneToMany, nameof(BlogNews.WriterId))]
        public List<BlogNews> Blogs { get; set; }

    }
}
