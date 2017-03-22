using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace OMF
{
    public class PointSetGeometry : ProjectElementGeometry, IObject
    {
        public PointSetGeometry()
        {

        }
        public PointSetGeometry(List<double[]> data) : this()
        {
            origin = new double[] { 0, 0, 0 };
            Verticies = new Vector3Array(data);
        }
        
        public string vertices { get; set; }

        [JsonIgnore]
        public Vector3Array Verticies { get; set; }

        public void Deserialize(Dictionary<string, object> json, System.IO.BinaryReader br)
        {
            Verticies = (Vector3Array)ObjectFactory.GetObjectFromGuid(json, br, vertices);
        }

        public void Serialize(Dictionary<string, object> json, BinaryWriter bw, string guid)
        {
            vertices = ObjectFactory.SerializeObject(Verticies, json, bw);

            ObjectFactory.GetObjectToData(json, this, guid);
        }
    }
}
