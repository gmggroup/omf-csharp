using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
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

        public Project Read(string file)
        {
            return Read(File.Open(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
        }

        public Project Read(Stream filestream)
        {
            Project project = new Project();

            using (BinaryReader br = new BinaryReader(filestream))
            {
                // Header 60 bytes
                var headerResult = ReadHeader(br.ReadBytes(60));
                Guid projectUid = headerResult.Item1;
                ulong jsonPosition = headerResult.Item2;
                long postionofBlob = br.BaseStream.Position;

                //JSON objects
                string jsonstring = ReadJson(br, jsonPosition);
                Dictionary<string, object> jsonDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonstring);


                //Project object
                project = (Project)ObjectFactory.GetObjectFromGuid(jsonDict, br, projectUid.ToString());
                project.Deserialize(jsonDict, br);
                
            }
            return project;

        }

        private static string ReadJson(BinaryReader br, ulong jsonPosition)
        {
            //Read the json string
            br.BaseStream.Seek((long)jsonPosition, SeekOrigin.Begin);
            byte[] jsonbytes = br.ReadBytes(Convert.ToInt32(br.BaseStream.Length - (long)jsonPosition));
            return Encoding.UTF8.GetString(jsonbytes);
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
            string uidString = BitConverter.ToString(bytearray, 36, 16).Replace("-", string.Empty);
            Guid uid = new Guid(uidString);

            //json Position ulong (8bytes)
            ulong jsonPosition = BitConverter.ToUInt64(bytearray ,52);

            //Return uid, jsonPosition
            return new Tuple<Guid, ulong>(uid, jsonPosition);
        }
    }
}
