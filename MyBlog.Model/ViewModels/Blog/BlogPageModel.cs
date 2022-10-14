using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Model.ViewModels.Blog
{
    public class BlogPageModel
    {


        public int CurrentPage { get; set; }

        public int TotalPage { get; set; }
        

        public TagInfo? CurrentTag { get; set; }
        public List<BlogViewModel> Blogs { get; set; }

        public List<TagInfo> Tags { get; set; }

        public string SearchString { get; set; }

        public string Url { get; set; }
        public int TagId { get; set; }

    }
}
