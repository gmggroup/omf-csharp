using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace OMF
{
    /// <summary>
    /// Contains mesh, data, textures, and options of a point set
    /// </summary>
	public class PointSetElement : ContentModel, IObject
	{
        public PointSetElement()
        {
        }
        public PointSetElement(List<double[]> data):this()
        {
            PointSet = new PointSetGeometry(data);
        }
        public byte[] color { get; set; }
		public string subtype { get; set; }
		public string geometry { get; set; }
		
        public string[] data { get; set; }

        [JsonIgnore]
        public List<IObject> Objects { get; set; }

        [JsonIgnore]
        public PointSetGeometry PointSet { get; set; }

        public void Deserialize(Dictionary<string, object> json, BinaryReader br)
		{
            PointSet = (PointSetGeometry)ObjectFactory.GetObjectFromGuid(json, br, geometry);
            Objects = ObjectFactory.DeserializeObjects(json, br, data);
        }

        public void Serialize(Dictionary<string, object> json, BinaryWriter bw, string guid)
        {
            geometry = ObjectFactory.SerializeObject(PointSet, json, bw);

            data = ObjectFactory.SerializeObjects(Objects, json, bw);

            ObjectFactory.GetObjectToData(json, this, guid);
        }
    }
}
