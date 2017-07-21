using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace OMF
{
    public class Int2Array : UidModel, IObject
    {
        [JsonIgnore]
        public List<int[]> Data { get; set; }
        public ScalarArray array { get; set; }

        public Int2Array()
        {

        }

        public Int2Array(List<int[]> data) : this()
        {
            Data = data;
        }
        
        public void Deserialize(Dictionary<string, object> json, System.IO.BinaryReader br)
        {
            Data = Helpers.ReadInt2Array(br, array);
        }

        public void Serialize(Dictionary<string, object> json, BinaryWriter bw)
        {
            array = Helpers.WriteInt2Array(bw, Data);

            ObjectFactory.GetObjectToData(json, this, uid.ToString());
        }
    }
}
