using Newtonsoft.Json;
using OMF.Base;
using System.Collections.Generic;
using System.IO;

namespace OMF
{
    public class Vector3Array : UidModel, IObject
    {
        public Vector3Array()
        {

        }
        public Vector3Array(List<double[]> data)
        {
            Data = data;
        }
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
