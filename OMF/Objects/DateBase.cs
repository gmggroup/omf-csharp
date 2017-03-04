using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMF.Objects
{
    public class DateBase: ClassBase
    {
        public DateBase()
        {
            date_modified = DateTime.Now;
            date_created = DateTime.Now;
        }
        public DateTime date_modified { get; set; }
        public DateTime date_created { get; set; }
        
    }
}
