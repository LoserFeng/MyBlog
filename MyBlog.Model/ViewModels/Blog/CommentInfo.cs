using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Model.ViewModels.Blog
{
	public class CommentInfo
	{

       
        public int BlogId { get; set; }

		public int  FatherId { get; set; }

        public String? TargetName { get; set; }

        [Required]
        public string Content { get; set; }







	}
}
