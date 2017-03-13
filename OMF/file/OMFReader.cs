using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OMF
{
    /// <summary>
    /// OMFReader deserializes an OMF file.
    /// 
    /// .. code::
    /// 
    ///     # Read all elements
    ///     reader = omf.OMFReader('infile.omf')
    ///     project = reader.get_project()
    /// 
    ///     # Read all PointSets:
    ///     reader = omf.OMFReader('infile.omf')
    ///     project = reader.get_project_overview()
    ///     uids_to_import = [element.uid for element in project.elements
    ///                       if isinstance(element, omf.PointSetElement)]
    ///     filtered_project = reader.get_project(uids_to_import)
    /// </summary>
    public class OMFReader
    {
        public OMFReader()
        {
            SurfaceElements = new List<SurfaceElement>();
            PointSetElements = new List<PointSetElement>();
            LineSetElements = new List<LineSetElement>();
            VolumeElements = new List<VolumeElement>();
        }
        public List<SurfaceElement> SurfaceElements { get; private set; }
        public List<PointSetElement> PointSetElements { get; private set; }
        public List<VolumeElement> VolumeElements { get; private set; }
        public List<LineSetElement> LineSetElements { get; private set; }

        public bool Read(string file)
        {
            return Read(File.Open(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
        }

        public bool Read(Stream filestream)
        {
            bool returnvalue = true;
            using (BinaryReader br = new BinaryReader(filestream))
            {
                // Header 60 bytes
                var headerResult = ReadHeader(br.ReadBytes(60));
                Guid uid = headerResult.Item1;
                ulong jsonPosition = headerResult.Item2;
                long postionofBlob = br.BaseStream.Position;

                //Read the json string
                br.BaseStream.Seek((long)jsonPosition, SeekOrigin.Begin);
                byte[] jsonbytes = br.ReadBytes(Convert.ToInt32(br.BaseStream.Length - (long)jsonPosition));
                string jsonstring = Encoding.UTF8.GetString(jsonbytes);


                //Decode 
                Dictionary<string, object> jsonDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonstring);

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
                                    SurfaceElement obj = (SurfaceElement)ObjectFactory.GetObjectFromData(jsonDict, br, data);
                                    if (obj != null)
                                    {
                                        SurfaceElements.Add(obj);
                                    }
                                }
                                break;
                            case "POINTSETELEMENT":
                                {
                                    PointSetElement obj = (PointSetElement)ObjectFactory.GetObjectFromData(jsonDict, br, data);
                                    if (obj != null)
                                    {
                                        PointSetElements.Add(obj);
                                    }
                                }
                                break;
                            case "VOLUMEELEMENT":
                                {
                                    VolumeElement obj = (VolumeElement)ObjectFactory.GetObjectFromData(jsonDict, br, data);
                                    if (obj != null)
                                    {
                                        VolumeElements.Add(obj);
                                    }
                                }
                                break;
                            case "LINESETELEMENT":
                                {
                                    LineSetElement obj = (LineSetElement)ObjectFactory.GetObjectFromData(jsonDict, br, data);
                                    if (obj != null)
                                    {
                                        LineSetElements.Add(obj);
                                    }
                                }
                                break;
                            default:

                                break;
                        }
                    }

                }

            }
            return returnvalue;

        }

        private static Tuple<Guid,ulong> ReadHeader(byte[] bytearray)
        {
            //Magic Number 4 bytes
            if (Version.MagicNumber[0] != bytearray[0]
                || Version.MagicNumber[1] != bytearray[1]
                || Version.MagicNumber[2] != bytearray[2]
                || Version.MagicNumber[3] != bytearray[3])
            {
                throw new InvalidOmfException("Invalid OMF File");
            }

            //Version string 32 bytes
            string version = Encoding.UTF8.GetString(bytearray, 4, 32).TrimEnd('\0');
            if (version.CompareTo(Version.VersionString) != 0)
            {
                throw new InvalidOmfException(string.Format("Version mismatch: file version {0}, reader version {1}",version,Version.VersionString));
            }

            //project uid 16 bytes
            string uidString = Encoding.UTF8.GetString(bytearray, 36, 16);
            Guid uid;
            Guid.TryParse(uidString, out uid);

            //json Position ulong (8bytes)
            ulong jsonPosition = BitConverter.ToUInt64(bytearray ,52);

            //Return uid, jsonPosition
            return new Tuple<Guid, ulong>(uid, jsonPosition);
        }
    }
}
