using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace OMF
{
    public class SurfaceElement : ContentModel, IObject
    {
        public SurfaceElement()
        {
            subtype = "surface";
        }
        public SurfaceElement(List<double[]> vertices, List<int[]> faces) : this()
        {
            Surface = new SurfaceGeometry(vertices, faces);
        }
        public int[] color { get; set; }
        public string subtype { get; set; }
        public string geometry { get; set; }

        [JsonIgnore]
        public SurfaceGeometry Surface { get; set; }

        [JsonIgnore]
        public string[] data { get; set; }


        public void Deserialize(Dictionary<string, object> json, BinaryReader br)
        {
            Surface = (SurfaceGeometry)ObjectFactory.GetObjectFromGuid(json, br, geometry);
        }

        public void Serialize(Dictionary<string, object> json, BinaryWriter bw)
        {
            geometry = ObjectFactory.SerializeObject(Surface, json, bw);

            ObjectFactory.GetObjectToData(json, this, uid.ToString());
        }
    }
}
