using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Model.ViewModels.Register
{
    public class RegisterInfo
    {

        public string Name { get; set; }


        public string UserName { get; set; }

        public string UserPwd { get; set; }




/*
       public WriterInfo writerInfo { get; set; }
*/



        //接下来是一些和自己主页主题相关的东西

        public string motto { get; set; }

        public Photo MainPagePhoto { get; set; }


    }
}
