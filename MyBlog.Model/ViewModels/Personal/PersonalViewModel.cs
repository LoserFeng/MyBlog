using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Model.ViewModels.Personal
{
    public class PersonalViewModel
    {
        public string Name { get; set; }


        public string UserName { get; set; }


        public int WriterId { get; set; }


        public WriterInfo WriterInfo { get; set; }


        public List<UserInfo> Concerns { get; set; }


        public List<BlogNews> Favorites { get; set; }



        //接下来是一些和自己主页主题相关的东西

        public String Motto { get; set; }


        public int MainPagePhoto_id { get; set; }

        public Photo MainPagePhoto { get; set; }




    }
}
