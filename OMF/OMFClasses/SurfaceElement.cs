using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMF.OMFClasses
{
    public class SurfaceElement : DateBase, IClass
    {
        public string name { get; set; }
        public string __class__ { get; set; }
        public byte[] color { get; set; }
        public string subtype { get; set; }
        public string geometry { get; set; }
        public SurfaceGeometry surfacegeometry { get; set; }

        public string[] data { get; set; }

        public string description { get; set; }

        public void Deserialize(Dictionary<string, object> json, BinaryReader br)
        {
            if(json.ContainsKey(geometry))
            {
                surfacegeometry= Newtonsoft.Json.JsonConvert.DeserializeObject<OMFClasses.SurfaceGeometry>(json[geometry].ToString());
                surfacegeometry.Deserialize(json, br);
            }
        }
    }
}
