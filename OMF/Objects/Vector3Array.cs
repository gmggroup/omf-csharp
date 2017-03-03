using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace OMF.Objects
{
    public class Vector3Array : DateBase, IObject
    {
        public string __class__ { get; set; }
        public ScalarArray array { get; set; }

        [JsonIgnore]
        public List<double[]> Data { get; set; }

        public void Deserialize(Dictionary<string, object> json, System.IO.BinaryReader br)
        {
            Data = Helpers.ReadDouble3Array(br, array);
        }

        public void Serialize(Dictionary<string, object> json, BinaryWriter bw, string guid)
        {
            array=Helpers.WriteDouble3Array(bw, Data);

            ObjectFactory.GetObjectToData(json, this, guid);
        }
    }
}
