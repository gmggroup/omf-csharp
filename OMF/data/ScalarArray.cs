using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace OMF.Base
{
    /// <summary>
    /// Class with unique ID and data array
    /// </summary>
    public class ScalarArray: UidModel, IObject
    {
        public ScalarArray()
        {

        }
        public ScalarArray(long _start,long _length,string _dtype)
        {
            start = _start;
            length = _length;
            dtype = _dtype;
        }
        public long start { get; set; }
        public long length { get; set; }
        public string dtype { get; set; }

        public void Deserialize(Dictionary<string, object> json, BinaryReader br)
        {
            
        }

        public void Serialize(Dictionary<string, object> json, BinaryWriter bw, string guid)
        {
            ObjectFactory.GetObjectToData(json, this, guid);
        }
    }
}
