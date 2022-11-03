using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Model.ViewModels.Tag
{
    public class CreateTagInfo
    {
        [Required]
        public string Name { get; set; }
    }
}
