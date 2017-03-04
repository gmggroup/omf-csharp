using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace OMF.Objects
{
    public class LineSetElement : DateAndDescriptionBase, IObject
    {
        public LineSetElement()
        {

        }
        public LineSetElement(List<double[]> verts, List<int[]> segments) : this()
        {
            Geometry = new LineSetGeometry(verts, segments);
        }
        public string name { get; set; }
        public byte[] color { get; set; }
        public string subtype { get; set; }
        public string geometry { get; set; }
        public string[] data { get; set; }

        [JsonIgnore]
        public LineSetGeometry Geometry { get; set; }

        [JsonIgnore]
        public List<IObject> Objects { get; set; }

        public void Deserialize(Dictionary<string, object> json, BinaryReader br)
        {
            Geometry = (LineSetGeometry)ObjectFactory.GetObjectFromGuid(json, br, geometry);
            Objects = ObjectFactory.DeserializeObjects(json, br, data);
        }

        public void Serialize(Dictionary<string, object> json, BinaryWriter bw, string guid)
        {
            geometry = ObjectFactory.SerializeObject(Geometry, json, bw);
            data = ObjectFactory.SerializeObjects(Objects, json, bw);

            ObjectFactory.GetObjectToData(json, this, guid);
        }
    }
}
