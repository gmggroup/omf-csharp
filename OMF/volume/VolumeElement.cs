using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace OMF
{
    public class VolumeElement : ContentModel, IObject
    {
        public int[] color { get; set; }
        public string subtype { get; set; }
        public string geometry { get; set; }

        [JsonIgnore]
        public string[] data { get; set; }

        [JsonIgnore]
        public VolumeGridGeometry VolumeGrid { get; set; }

        [JsonIgnore]
        public List<IObject> Objects { get; set; }

        public void Deserialize(Dictionary<string, object> json, BinaryReader br)
        {
            VolumeGrid = (VolumeGridGeometry)ObjectFactory.GetObjectFromGuid(json, br, geometry);
            
            Objects = ObjectFactory.DeserializeObjects(json, br, data);
        }

        public void Serialize(Dictionary<string, object> json, BinaryWriter bw)
        {
            //need to fill up the json Dictionary then write the binary data
            geometry = ObjectFactory.SerializeObject(VolumeGrid, json, bw);
            data=ObjectFactory.SerializeObjects(Objects, json, bw);

            ObjectFactory.GetObjectToData(json, this, uid.ToString());
        }
    }
}
