using System.Collections.Generic;
using System.IO;

namespace OMF
{
    public class AttributeDefinitionArray : UidModel, IObject
    {
        public string[] attributenames { get; set; }
        public int[] attributetypes { get; set; }

        public enum AttributeTypeConstants
        {
            @string=0,
            integer=1,
            @double=2,
            datetime=3,
            boolean=4
        }
            
        
        public void Deserialize(Dictionary<string, object> json, BinaryReader br)
        {
            
        }

        public void Serialize(Dictionary<string, object> json, BinaryWriter bw, string guid)
        {
            ObjectFactory.GetObjectToData(json, this, guid);
        }
    }
}
