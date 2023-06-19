using Microsoft.AspNetCore.Http;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyBlog.Model.ViewModels.Blog
{
    public class CreationInfo
    {


        [Display(Name = "文章(GUID)")]
        public String? Id { get; set; }


        [Required]
        [Display(Name = "标题")]
        public string Title { get; set; }


        [Required]
        [Display(Name = "封面")]
        public IFormFile CoverPhoto { get; set; }



        [Required]
        [Display(Name = "介绍")]
        public string Introduction { get; set; }


        [Required]
        [Display(Name ="类型")]
        public string Type { get; set; }





        [Display(Name = "文章(MarkDown)")]
        public IFormFile ? MarkDownFile { get; set; }



        [Display(Name = "文章组件")]
        public List<IFormFile>? Assets { get; set; }


        [Display(Name = "文章(Text)")]
        public String ?Content { get; set; }



        [Display(Name="标签")]
        public string Tags_JSON { get; set; }


        [Display(Name = "视频")]
        public String? VideoName { get; set; }

    }
}
