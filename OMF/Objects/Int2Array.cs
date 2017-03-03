using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace OMF.Objects
{
    public class Int2Array : DateBase, IObject
    {
        public string __class__ { get; set; }
        public ScalarArray array { get; set; }

        [JsonIgnore]
        public List<int[]> Data { get; set; }

        public void Deserialize(Dictionary<string, object> json, System.IO.BinaryReader br)
        {
            Data = new List<int[]>();
            br.BaseStream.Seek(array.start, System.IO.SeekOrigin.Begin);

            byte[] data = br.ReadBytes((int)(array.length));

            byte[] newdata = Ionic.Zlib.ZlibStream.UncompressBuffer(data);
            for (int i = 0; i < newdata.Length; i = i + 16)
            {
                int[] toadd = new int[2];
                toadd[0] = BitConverter.ToInt32(newdata, i);
                toadd[1] = BitConverter.ToInt32(newdata, i + 8);
                Data.Add(toadd);
            }
        }
    }
}
