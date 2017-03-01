using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMF.OMFClasses
{
	public class PointSetElement : DateBase, IClass
	{
		public string name { get; set; }
		public string __class__ { get; set; }
		public byte[] color { get; set; }
		public string subtype { get; set; }
		public string geometry { get; set; }
		public PointSetGeometry pointsetgeometry { get; set; }

		public string[] data { get; set; }

		public string description { get; set; }

		public void Deserialize(Dictionary<string, object> json, BinaryReader br)
		{
			if (json.ContainsKey(geometry))
			{
				pointsetgeometry = Newtonsoft.Json.JsonConvert.DeserializeObject<OMFClasses.PointSetGeometry>(json[geometry].ToString());
				pointsetgeometry.Deserialize(json, br);
			}
		}
	}
}
