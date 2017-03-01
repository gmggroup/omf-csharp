using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace OMF
{
    public class OMF
    {
        public bool Execute()
        {
			string baseDir = System.AppDomain.CurrentDomain.BaseDirectory;
            string file = System.IO.Path.Combine(baseDir, "../../test.omf");
            if(System.IO.File.Exists(file)==false)
            {
                Console.WriteLine("File does not exist");
                return false;
            }
            BinaryReader br = new BinaryReader(File.Open(file, FileMode.Open));

            byte[] magic = br.ReadBytes(4);//4 byte magic number: b'\x81\x82\x83\x84'
            byte[] version = br.ReadBytes(32);//32 byte version string: 'OMF-v0.9.0' (other bytes empty)
            byte[] uid = br.ReadBytes(16);//16 byte project uid (in little-endian bytes)

            ulong jsonPosition = br.ReadUInt64();

            long postionofBlob = br.BaseStream.Position;

            br.BaseStream.Seek((long)jsonPosition, SeekOrigin.Begin);
            byte[] jsonbytes = br.ReadBytes(Convert.ToInt32(br.BaseStream.Length - (long)jsonPosition));
            string jsonstring = Encoding.UTF8.GetString(jsonbytes);


            Dictionary<string, object> jsonDict = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonstring);

            //try
            //{
                foreach (string id in jsonDict.Keys)
                {
                    string value = jsonDict[id].ToString();
                    Dictionary<string, object> thisDict = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(value);

                    if (thisDict.ContainsKey("__class__"))
                    {
                        switch (thisDict["__class__"].ToString().ToUpper())
                        {
                            case "SURFACEELEMENT":
                                OMFClasses.SurfaceElement se = Newtonsoft.Json.JsonConvert.DeserializeObject<OMFClasses.SurfaceElement>(value);
                                se.Deserialize(jsonDict, br);

                                break;
							case "POINTSETELEMENT":
								OMFClasses.PointSetElement pe = Newtonsoft.Json.JsonConvert.DeserializeObject<OMFClasses.PointSetElement>(value);
								pe.Deserialize(jsonDict, br);
								break;

                        }
                    }

                }
            //}
            //catch
            //{

            //}
            //finally
            //{
            //    br.Close();
            //}

            return true;

        }

    }
}
