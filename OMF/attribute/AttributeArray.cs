using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace OMF.Base
{
    public class AttributeArray : UidModel, IObject
    {
        public AttributeArray()
        {

        }
        public AttributeArray(List<object> data, AttributeDefinitionArray definitions)
        {
            Data = Helpers.GetAttributeByteArray(data, definitions);
        }
        public ScalarArray array { get; set; }
        public string attributedefinition { get; set; }
        [JsonIgnore]
        public byte[] Data { get; set; }

        public void Deserialize(Dictionary<string, object> json, System.IO.BinaryReader br)
        {
            Data=Helpers.ReadByteArray(br, array);
        }

        public void Serialize(Dictionary<string, object> json, BinaryWriter bw, string guid)
        {
            array = Helpers.WriteByteArray(bw, Data);

            ObjectFactory.GetObjectToData(json, this, guid);
        }
    }
}
