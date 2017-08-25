using System.Collections.Generic;
using System.IO;

namespace OMF
{
    public class StringArray : UidModel, IObject
    {
        public string[] array { get; set; }

        public void Deserialize(Dictionary<string, object> json, BinaryReader br)
        {

        }

        public void Serialize(Dictionary<string, object> json, BinaryWriter bw)
        {
            ObjectFactory.GetObjectToData(json, this, uid.ToString());
        }
    }
}
