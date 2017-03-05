using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace OMF.Objects
{
    public class ScalarData : DateBase, IObject
    {
        public string array { get; set; }
        public string location { get; set; }
        public string name { get; set; }

        public void Deserialize(Dictionary<string, object> json, BinaryReader br)
        {

        }

        public void Serialize(Dictionary<string, object> json, BinaryWriter bw, string guid)
        {
            ObjectFactory.GetObjectToData(json, this, guid);
        }
    }
}
