using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyBlog.Model.ViewModels.CMS.BlogNews
{
    public class BlogNewsEdit
    {
        public int Id { get; set; }
        [DefaultValue(null)]
        public String? Title { get; set; }


        [DefaultValue(null)]
        public IFormFile? CoverPhoto { get; set; }


        [DefaultValue(null)]
        public int BrowseCount { get; set; }

        [DefaultValue(null)]
        public int LikeCount { get; set; }


        [DefaultValue(null)]
        public String? Introduction { get; set; }


        [DefaultValue(null)]
        public String? Type { get; set; }

        [DefaultValue(null)]
        [Display(Name = "文章(MarkDown)")]
        public IFormFile? MarkDownFile { get; set; }


        [DefaultValue(null)]
        [Display(Name = "文章组件")]
        public List<IFormFile>? Assets { get; set; }

        [DefaultValue(null)]
        [Display(Name = "文章(Text)")]
        public String? Content { get; set; }


        [DefaultValue(null)]
        [Display(Name = "标签")]
        public String ?Tags_JSON { get; set; }
    }
}
