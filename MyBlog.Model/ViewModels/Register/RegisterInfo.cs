using Microsoft.AspNetCore.Http;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Model.ViewModels.Register
{
    public class RegisterInfo
    {
        [Required]
        [Display (Name="昵称")]
        public string Name { get; set; }


        [Required]
        [Display(Name = "用户名")]
        public string UserName { get; set; }


        [Required]
        [Display(Name = "密码")]
        public string UserPwd { get; set; }


        [Display(Name = "座右铭")]

        public string motto { get; set; }

        /*        public Photo MainPagePhoto { get; set; }*/




        [Display(Name="主页图")]
        public IFormFile photo { get; set; }
    }
}
