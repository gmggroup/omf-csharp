using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMF.Objects
{
    public class Int3Array : DateBase, IObject
    {
        public string __class__ { get; set; }
        public ScalarArray array { get; set; }

        [JsonIgnore]
        public List<int[]> Data { get; set; }

        public void Deserialize(Dictionary<string, object> json, BinaryReader br)
        {
            Data = Helpers.ReadInt3Array(br, array);
        }

        public void Serialize(Dictionary<string, object> json, BinaryWriter bw, string guid)
        {
            array=Helpers.WriteInt3Array(bw, Data);

            ObjectFactory.GetObjectToData(json, this, guid);
        }
    }
}
