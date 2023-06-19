using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyBlog.Model.ViewModels.CMS.UserInfo
{
    public class UserInfoEdit
    {

        [Required]
        [Display(Name="ID")]
        public int Id { get; set; }


        [Display(Name = "昵称")]
        public String ?Name { get; set; }



        [Display(Name = "用户名")]
        public String ?UserName { get; set; }


        [Display(Name = "密码")]
        public String ?UserPwd { get; set; }


        [Display(Name = "座右铭")]

        public String? Motto { get; set; }






        [Display(Name = "主页图")]
        public IFormFile ? MainPagePhoto { get; set; }

    }
}
