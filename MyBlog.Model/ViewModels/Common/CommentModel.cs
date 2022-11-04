using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Model.ViewModels.Common
{
    public class CommentModel
    {

        public int Id { get; set; }
        public string GUID { get; set; }
        public string Content { get; set; }


        public int UserId { get; set; }


        public int SupportCount { get; set; }


        public int BlogId { get; set; }

        public int FatherId { get; set; }



        public String TargetName { get; set; }




        public DateTime Time { get; set; }

    }

}
