using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace OMF.OMFClasses
{
    public class StringArray : DateBase, IObject
    {
        public string __class__ { get; set; }
        public string[] array { get; set; }

        public void Deserialize(Dictionary<string, object> json, BinaryReader br)
        {

        }
    }
}
