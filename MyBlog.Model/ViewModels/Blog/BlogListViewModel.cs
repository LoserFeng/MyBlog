using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Model.ViewModels.Blog
{
    public class BlogListViewModel
    {
        public TagInfo? CurrentTag{ get; set; }
        public List<BlogViewModel> Blogs { get; set; }

        public List<TagInfo> Tags { get; set; }


        public int Page;
        public int Total;
        public int PageSize;
        public int PageCount;


    }
}
