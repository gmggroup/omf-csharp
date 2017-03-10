using Newtonsoft.Json;
using OMF.Base;
using System.Collections.Generic;
using System.IO;

namespace OMF
{
    public class SurfaceElement : ContentModel, IObject
    {
        public SurfaceElement()
        {

        }
        public SurfaceElement(List<double[]> vertices, List<int[]> faces) : this()
        {
            Surface = new SurfaceGeometry(vertices, faces);
        }
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
