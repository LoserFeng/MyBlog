using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Model.ViewModels.Common
{
    public class BlogNewsModel
    {
        public int Id { get; set; }
        public string GUID { get; set; }

        public String Title { get; set; }

        public String Introduction { get; set; }

        public String Summary { get; set; }

        public String Content { get;set; }

        public DateTime Time { get; set; }

        public int BrowseCount { get; set; }

        public int LikeCount { get; set; }

        public String Type { get; set; }


        public String Path { get; set;  }

        public Photo CoverPhoto { get; set; }

        public int WriterId { get; set; }

        public String Tags { get; set; }

        public int Admirer_Total { get; set; }

        public int Comment_Total { get; set; }





    }
}
