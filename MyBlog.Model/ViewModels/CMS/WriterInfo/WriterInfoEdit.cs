using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyBlog.Model.ViewModels.CMS.WriterInfo
{
    public class WriterInfoEdit
    {

        [Required]
        [Display(Name = "ID")]
        public int Id { get; set; }


        [Display(Name = "作者名称")]
        public String? WriterName { get; set; }
    }
}
