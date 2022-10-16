using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Model
{
    public class Comment:BaseId
    {

        [SugarColumn(ColumnDataType = "ntext")]
        public string Content { get; set; }


        public string GUID { get; set; }





        public int UserId { get; set; }

        [Navigate(NavigateType.OneToOne, nameof(UserId))]
        public UserInfo UserInfo { get; set; }

        public int SupportCount { get; set; }


        public int BlogId { get; set; }
        public int FatherId { get; set; }

        [Navigate(NavigateType.OneToMany, nameof(Comment.FatherId))]
        public List<Comment> Comments { get; set; }


        [SugarColumn(IsNullable  = true)]
        public String TargetName { get; set; }




        public DateTime Time { get; set; }


    }
}
