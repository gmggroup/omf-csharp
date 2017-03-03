using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace OMF.Objects
{
	public class PointSetGeometry : DateBase, IObject
	{
        public string __class__ { get; set; }
        public double[] origin { get; set; }
		public string vertices { get; set; }

        [JsonIgnore]
        public Vector3Array Verticies { get; set; }

		public void Deserialize(Dictionary<string, object> json, System.IO.BinaryReader br)
		{
            Verticies = (Vector3Array)ObjectFactory.GetObjectFromGuid(json, br, vertices);
		}
	}
}
