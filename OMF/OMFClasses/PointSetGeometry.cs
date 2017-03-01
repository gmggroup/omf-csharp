using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMF.OMFClasses
{
	public class PointSetGeometry : DateBase, IClass
	{
		public double[] origin { get; set; }
		public string vertices { get; set; }
		public string __class__ { get; set; }
		public Vector3Array Verticies { get; set; }

		public void Deserialize(Dictionary<string, object> json, System.IO.BinaryReader br)
		{
			if (json.ContainsKey(vertices))
			{
				Verticies = Newtonsoft.Json.JsonConvert.DeserializeObject<OMFClasses.Vector3Array>(json[vertices].ToString());
				Verticies.Deserialize(json, br);
			}
		}
	}
}
