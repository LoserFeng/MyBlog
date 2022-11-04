using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Model.ViewModels.Comment_ViewModel
{
    public class EditComment
    {
        public int Id { get; set; }
        public String ?Content { get; set; }

        public int SupportCount { get; set; }  

    }
}
