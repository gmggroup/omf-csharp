using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace OMF.Objects
{
    public class StringArray : DateBase, IObject
    {
        public string[] array { get; set; }

        public void Deserialize(Dictionary<string, object> json, BinaryReader br)
        {

        }

        public void Serialize(Dictionary<string, object> json, BinaryWriter bw, string guid)
        {
            ObjectFactory.GetObjectToData(json, this, guid);
        }
    }
}
