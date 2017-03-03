using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OMF.Objects
{
    public class Legend : DateAndDescriptionBase, IObject
    {
        public string name { get; set; }
        public string __class__ { get; set; }
        public string values { get; set; }

        [JsonIgnore]
        public ColorArray colorarray { get; set; }

        public void Deserialize(Dictionary<string, object> json, BinaryReader br)
        {
            colorarray = (ColorArray)ObjectFactory.GetObjectFromGuid(json, br, values);
        }
    }
}
