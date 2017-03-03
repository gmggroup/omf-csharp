using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMF.Objects
{
    public interface IObject
    {
        string __class__ { get; set; }
        void Deserialize(Dictionary<string, object> json,System.IO.BinaryReader br);
    }
}
