using System.Collections.Generic;
using System.IO;

namespace OMF
{
    public class VolumeGridGeometry : ProjectElementGeometry, IObject
    {
        public VolumeGridGeometry()
        {

        }

        public double[] axis_u { get; set; }
        public double[] axis_v { get; set; }
        public double[] axis_w { get; set; }
        public double[] tensor_v { get; set; }
        public double[] tensor_w { get; set; }
        public double[] tensor_u { get; set; }

        public void Deserialize(Dictionary<string, object> json, System.IO.BinaryReader br)
        {

        }

        public void Serialize(Dictionary<string, object> json, BinaryWriter bw)
        {

            ObjectFactory.GetObjectToData(json, this, uid.ToString());
        }
    }
}
