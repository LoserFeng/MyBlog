using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.MarkDown.Model
{
    public class Segment
    {


        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }
        public String Text { get; set; }
    }
}
