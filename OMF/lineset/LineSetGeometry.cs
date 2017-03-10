using Newtonsoft.Json;
using OMF.Base;
using System.Collections.Generic;
using System.IO;

namespace OMF
{
    public class LineSetGeometry : UidModel, IObject
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
        public double[] origin { get; set; }
        public string vertices { get; set; }
        public string segments { get; set; }

        [JsonIgnore]
        public Int2Array Segments { get; set; }

        [JsonIgnore]
        public Vector3Array Vertices { get; set; }

        public void Deserialize(Dictionary<string, object> json, System.IO.BinaryReader br)
        {
            Vertices = (Vector3Array)ObjectFactory.GetObjectFromGuid(json, br, vertices);
            Segments = (Int2Array)ObjectFactory.GetObjectFromGuid(json, br, segments);
        }

        public void Serialize(Dictionary<string, object> json, BinaryWriter bw, string guid)
        {
            vertices = ObjectFactory.SerializeObject(Vertices, json, bw);
            segments = ObjectFactory.SerializeObject(Segments, json, bw);

            ObjectFactory.GetObjectToData(json, this, guid);
        }
    }
}
