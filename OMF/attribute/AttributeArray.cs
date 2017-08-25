using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace OMF
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

        public void Serialize(Dictionary<string, object> json, BinaryWriter bw)
        {
            array = Helpers.WriteByteArray(bw, Data);

            ObjectFactory.GetObjectToData(json, this, uid.ToString());
        }
    }
}
