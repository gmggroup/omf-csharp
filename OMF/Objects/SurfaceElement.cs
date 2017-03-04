using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace OMF.Objects
{
    public class SurfaceElement : DateAndDescriptionBase, IObject
    {
        public SurfaceElement()
        {

        }
        public SurfaceElement(List<double[]> vertices, List<int[]> faces) : this()
        {
            Surface = new SurfaceGeometry(vertices, faces);
        }
        public string name { get; set; }
        public byte[] color { get; set; }
        public string subtype { get; set; }
        public string geometry { get; set; }

        [JsonIgnore]
        public SurfaceGeometry Surface { get; set; }

        public string[] data { get; set; }


        public void Deserialize(Dictionary<string, object> json, BinaryReader br)
        {
            Surface = (SurfaceGeometry)ObjectFactory.GetObjectFromGuid(json, br, geometry);
        }

        public void Serialize(Dictionary<string, object> json, BinaryWriter bw, string guid)
        {
            geometry = ObjectFactory.SerializeObject(Surface, json, bw);

            ObjectFactory.GetObjectToData(json, this, guid);
        }
    }
}
