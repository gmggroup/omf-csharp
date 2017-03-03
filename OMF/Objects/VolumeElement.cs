using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace OMF.Objects
{
    public class VolumeElement : DateBase, IObject
    {
        public string name { get; set; }
        public string __class__ { get; set; }
        public byte[] color { get; set; }
        public string subtype { get; set; }
        public string geometry { get; set; }
        public string[] data { get; set; }
        public string description { get; set; }

        [JsonIgnore]
        public SurfaceGeometry Surface { get; set; }

        [JsonIgnore]
        public List<IObject> Objects { get; set; }

        public void Deserialize(Dictionary<string, object> json, BinaryReader br)
        {
            Surface = (SurfaceGeometry)ObjectFactory.GetObjectFromGuid(json, br, geometry);
            
            Objects = ObjectFactory.DeserializeObjects(json, br, data);
        }

        public void Serialize(Dictionary<string, object> json, BinaryWriter bw,string guid)
        {
            //need to fill up the json Dictionary then write the binary data
            geometry = ObjectFactory.SerializeObject(Surface, json, bw);
            
            data=ObjectFactory.SerializeObjects(Objects, json, bw);

            ObjectFactory.GetObjectToData(json, this, guid);
        }
    }
}
