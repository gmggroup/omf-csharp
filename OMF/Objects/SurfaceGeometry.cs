using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace OMF.Objects
{
    public class SurfaceGeometry : DateBase, IObject
    {
        public SurfaceGeometry()
        {

        }
        public SurfaceGeometry(List<double[]> vertices, List<int[]> faces) : this()
        {
            origin = new double[] { 0, 0, 0 };
            Verticies = new Objects.Vector3Array(vertices);
            Triangles = new Int3Array(faces);
        }
        public double[] origin { get; set; }
        public string vertices { get; set; }
        public string triangles { get; set; }


        [JsonIgnore]
        public Vector3Array Verticies { get; set; }

        [JsonIgnore]
        public Int3Array Triangles { get; set; }

        public void Deserialize(Dictionary<string, object> json, System.IO.BinaryReader br)
        {
            Verticies = (Vector3Array)ObjectFactory.GetObjectFromGuid(json, br, vertices);
            Triangles = (Int3Array)ObjectFactory.GetObjectFromGuid(json, br, triangles);
        }

        public void Serialize(Dictionary<string, object> json, BinaryWriter bw, string guid)
        {
            vertices = ObjectFactory.SerializeObject(Verticies, json, bw);
            triangles = ObjectFactory.SerializeObject(Triangles, json, bw);

            ObjectFactory.GetObjectToData(json, this, guid);
        }
    }
}
