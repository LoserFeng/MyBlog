using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Model.DTO
{
    public class UserInfoDTO
    {

        public string Name { get; set; }


        public string UserName { get; set; }

        public string UserPwd { get; set; }



        public WriterInfo writerInfo { get; set; }


        public List<WriterInfo> Concerns { get; set; }

        public List<BlogNews> Favorites { get; set; }



        //接下来是一些和自己主页主题相关的东西

        public String motto;

        public Photo MainPagePhoto { get; set; }
    }
}
