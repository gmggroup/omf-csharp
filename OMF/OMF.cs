using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
namespace OMF
{
    public class OMF
    {
        public OMF()
        {
            LastError = "";
            SurfaceElements = new List<OMFClasses.SurfaceElement>();
            PointSetElements = new List<OMFClasses.PointSetElement>();
            LineSetElements = new List<OMFClasses.LineSetElement>();
            VolumeElements = new List<OMFClasses.VolumeElement>();
        }
        public string LastError { get; private set; }
        public List<OMFClasses.SurfaceElement> SurfaceElements { get; private set; }
        public List<OMFClasses.PointSetElement> PointSetElements { get; private set; }
        public List<OMFClasses.VolumeElement> VolumeElements { get; private set; }
        public List<OMFClasses.LineSetElement> LineSetElements { get; private set; }

        public bool Execute(string file)
        {
            bool returnvalue = true;

            BinaryReader br = new BinaryReader(File.Open(file, FileMode.Open,FileAccess.Read,FileShare.ReadWrite));

            byte[] magic = br.ReadBytes(4);//4 byte magic number: b'\x81\x82\x83\x84'
            byte[] version = br.ReadBytes(32);//32 byte version string: 'OMF-v0.9.0' (other bytes empty)
            byte[] uid = br.ReadBytes(16);//16 byte project uid (in little-endian bytes)

            ulong jsonPosition = br.ReadUInt64();

            long postionofBlob = br.BaseStream.Position;

            br.BaseStream.Seek((long)jsonPosition, SeekOrigin.Begin);
            byte[] jsonbytes = br.ReadBytes(Convert.ToInt32(br.BaseStream.Length - (long)jsonPosition));
            string jsonstring = Encoding.UTF8.GetString(jsonbytes);


            Dictionary<string, object> jsonDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonstring);

            //try
            //{
            foreach (string id in jsonDict.Keys)
            {
                string data = jsonDict[id].ToString();
                Dictionary<string, object> thisDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(data);

                if (thisDict.ContainsKey("__class__"))
                {
                    switch (thisDict["__class__"].ToString().ToUpper())
                    {
                        case "SURFACEELEMENT":
                            {
                                OMFClasses.SurfaceElement obj = (OMFClasses.SurfaceElement)OMFClasses.ObjectFactory.GetObjectFromData(jsonDict, br, data);
                                if (obj != null)
                                {
                                    SurfaceElements.Add(obj);
                                }
                            }
                            break;
                        case "POINTSETELEMENT":
                            {
                                OMFClasses.PointSetElement obj = (OMFClasses.PointSetElement)OMFClasses.ObjectFactory.GetObjectFromData(jsonDict, br, data);
                                if (obj != null)
                                {
                                    PointSetElements.Add(obj);
                                }
                            }
                            break;
                        case "VOLUMELEMENT":
                            {
                                OMFClasses.VolumeElement obj = (OMFClasses.VolumeElement)OMFClasses.ObjectFactory.GetObjectFromData(jsonDict, br, data);
                                if (obj != null)
                                {
                                    VolumeElements.Add(obj);
                                }
                            }
                            break;
                        case "LINESETELEMENT":
                            {
                                OMFClasses.LineSetElement obj = (OMFClasses.LineSetElement)OMFClasses.ObjectFactory.GetObjectFromData(jsonDict, br, data);
                                if (obj != null)
                                {
                                    LineSetElements.Add(obj);
                                }
                            }
                            break;

                    }
                }

            }
            //}
            //catch(Exception ex)
            //{
            // LastError=ex.Message;
            //returnvalue=false;
            //}
            //finally
            //{
            br.Close();
            //}

            return returnvalue;

        }

    }
}
