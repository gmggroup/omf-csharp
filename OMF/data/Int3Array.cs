using Newtonsoft.Json;
using OMF.Base;
using System.Collections.Generic;
using System.IO;

namespace OMF
{
    public class Int3Array : UidModel, IObject
    {
        public Int3Array()
        {

        }
        public Int3Array(List<int[]> data)
        {
            Data = data;
        }
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
