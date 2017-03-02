using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace OMF.OMFClasses
{
    public class Vector3Array : DateBase, IClass
    {
        public string __class__ { get; set; }
        public ScalarArray array { get; set; }

        [JsonIgnore]
        public List<double[]> Data { get; set; }

        public void Deserialize(Dictionary<string, object> json, System.IO.BinaryReader br)
        {
            Data = new List<double[]>();
            br.BaseStream.Seek(array.start, System.IO.SeekOrigin.Begin);

            byte[] data = br.ReadBytes((int)(array.length));

            byte[] newdata = Ionic.Zlib.ZlibStream.UncompressBuffer(data);
            for (int i = 0; i < newdata.Length; i = i + 24)
            {
                double[] toadd = new double[3];
                toadd[0] = BitConverter.ToDouble(newdata, i);
                toadd[1] = BitConverter.ToDouble(newdata, i + 8);
                toadd[2] = BitConverter.ToDouble(newdata, i + 16);
                Data.Add(toadd);
            }
        }
    }
}
