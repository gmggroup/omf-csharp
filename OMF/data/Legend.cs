using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace OMF
{
    public class Legend : ContentModel, IObject
    {
        public string values { get; set; }

        [JsonIgnore]
        public ColorArray Colors { get; set; }

        public void Deserialize(Dictionary<string, object> json, BinaryReader br)
        {
            Colors = (ColorArray)ObjectFactory.GetObjectFromGuid(json, br, values);
        }

        public void Serialize(Dictionary<string, object> json, BinaryWriter bw)
        {
            values = ObjectFactory.SerializeObject(Colors, json, bw);

            ObjectFactory.GetObjectToData(json, this, uid.ToString());
        }
    }
}
