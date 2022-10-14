using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Model.ViewModels.Page
{
    public class PageModel
    {
        public int PageTotal { get; set; }
        public int CurrentPage { get; set; }
        public string Url { get;set; }

        public int TagId { get; set; }

        public string SearchString { get; set; }    

    }
}
