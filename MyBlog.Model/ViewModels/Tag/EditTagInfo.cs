using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Model.ViewModels.Tag
{
    public class EditTagInfo
    {



        [Required]
        public int Id { get; set; }
        [DefaultValue(null)]
        public String ?Name { get; set; }

    }
}
