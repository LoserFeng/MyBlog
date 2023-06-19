using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Model.ViewModels.Common
{
    public class TagInfoModel
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public int Browse_Total { get; set; }
        public int Blog_Total { get; set; }
        public int Like_Total { get; set; }

    }
}
