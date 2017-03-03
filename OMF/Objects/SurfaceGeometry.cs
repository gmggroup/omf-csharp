using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace OMF.Objects
{
    public class SurfaceGeometry: DateBase, IObject
    {
        public string __class__ { get; set; }
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
    }
}
