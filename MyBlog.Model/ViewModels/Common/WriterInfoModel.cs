using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyBlog.Model.ViewModels.Common
{
    public class WriterInfoModel
    {

        public int Id { get; set; }
        public String WriterName { get; set; }

        public int Fan_Total { get; set; }
        
        public int Blog_Total { get; set; }


        public int Browse_Total { get; set; }  

       
    }
}
