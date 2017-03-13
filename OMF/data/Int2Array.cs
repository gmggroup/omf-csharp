using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace OMF.Base
{
    public class Int2Array : UidModel, IObject
    {
        public Int2Array()
        {

        }
        public Int2Array(List<int[]> data) : this()
        {
            Data = data;
        }
        public ScalarArray array { get; set; }

        [JsonIgnore]
        public List<int[]> Data { get; set; }

        public void Deserialize(Dictionary<string, object> json, System.IO.BinaryReader br)
        {
            Data = Helpers.ReadInt2Array(br, array);
        }

        public void Serialize(Dictionary<string, object> json, BinaryWriter bw, string guid)
        {
            array = Helpers.WriteInt2Array(bw, Data);

            ObjectFactory.GetObjectToData(json, this, guid);
        }
    }
}
