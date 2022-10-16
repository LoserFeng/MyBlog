using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Model.ViewModels.Blog
{
    public class BlogViewModel
    {
        public int Id { get; set; }

        public string GUID { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public int BrowseCount { get; set; }
        public string ContentHtml { get; set; }
        public string Path { get; set; }
        public string? Url { get; set; }
        public DateTime CreationTime { get; set; }
        public List<TagInfo> Tags { get; set; }

        public Photo CoverPhoto { get; set; }

        public WriterInfo WriterInfo { get; set; }


        public List<Comment> Comments { get; set; }
    }
}
