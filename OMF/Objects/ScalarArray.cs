using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace OMF.Objects
{
    public class ScalarArray: DateBase, IObject
    {
        public string __class__ { get; set; }
        public long start { get; set; }
        public long length { get; set; }
        public string dtype { get; set; }

        public void Deserialize(Dictionary<string, object> json, BinaryReader br)
        {

        }
    }
}
