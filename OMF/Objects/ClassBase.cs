using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMF.Objects
{
    public class ClassBase
    {
        public ClassBase()
        {
            __class__ = this.GetType().Name;
        }
        public string __class__ { get; set; }
    }
}
