using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace OMF.OMFClasses
{
	public class LineSetGeometry : DateBase, IObject
	{
        public string __class__ { get; set; }
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
	}
}
