using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace OMF
{
    public class LineSetGeometry : ProjectElementGeometry, IObject
    {
        public LineSetGeometry()
        {

        }
        public LineSetGeometry(List<double[]> verts, List<int[]> segments) : this()
        {
            origin = new double[] { 0, 0, 0 };
            Segments = new Int2Array(segments);
            Vertices = new Vector3Array(verts);

        }

        public string vertices { get; set; }
        public string segments { get; set; }

        [JsonIgnore]
        public Int2Array Segments { get; set; }

        [JsonIgnore]
        public Vector3Array Vertices { get; set; }

        public void Deserialize(Dictionary<string, object> json, BinaryReader br)
        {
            Vertices = (Vector3Array)ObjectFactory.GetObjectFromGuid(json, br, vertices);
            Segments = (Int2Array)ObjectFactory.GetObjectFromGuid(json, br, segments);
        }

        public void Serialize(Dictionary<string, object> json, BinaryWriter bw)
        {
            vertices = ObjectFactory.SerializeObject(Vertices, json, bw);
            segments = ObjectFactory.SerializeObject(Segments, json, bw);

            ObjectFactory.GetObjectToData(json, this, uid.ToString());
        }
    }
}
