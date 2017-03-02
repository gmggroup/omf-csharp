using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace OMF.OMFClasses
{
    public class ScalarData : DateBase, IObject
    {
        public string __class__ { get; set; }
        public string array { get; set; }
        public string location { get; set; }
        public string name { get; set; }

        public void Deserialize(Dictionary<string, object> json, BinaryReader br)
        {

        }
    }
}
