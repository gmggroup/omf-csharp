using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OMF
{
    /// <summary>
    /// OMFWriter serializes a OMF project to a file
    /// 
    /// .. code::
    /// 
    ///     proj = omf.project()
    ///     ...
    ///     omf.OMFWriter(proj, 'outfile.omf')
    /// 
    /// The output file starts with a 60 byte header:
    /// 
    /// * 4 byte magic number: :code:`b'\\x81\\x82\\x83\\x84'`
    /// * 32 byte version string: :code:`'OMF-v0.9.0'` (other bytes empty)
    /// * 16 byte project uid(in little-endian bytes)
    /// * 8 byte unsigned long long (little-endian): JSON start location in file
    /// 
    /// Following the header is a binary data blob.
    /// 
    /// Following the binary is a UTF-8 encoded JSON dictionary containing
    /// all elements of the project keyed by UID string. Objects can reference
    /// each other by UID, and arrays and images contain pointers to their data
    /// in the binary blob.
    /// </summary>
    public class OMFWriter
    {
        
        public bool Write(Project project, string file)
        {
            return Write(project, File.Open(file, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None));
        }

        public bool Write(Project project, Stream filestream)
        {
            bool returnvalue = true;

            using (BinaryWriter bw = new BinaryWriter(filestream))
            {
                bw.Write(CreateHeader());

                Dictionary<string, object> json = new Dictionary<string, object>();
                //now write the binary data
                var projectUid = ObjectFactory.SerializeObject(project, json, bw);
                
                ObjectFactory.GetObjectToData(json, project, projectUid);
                project.Serialize(json, bw, projectUid);

                UInt64 jsonPosition = (UInt64)bw.BaseStream.Position;

                //json string to the file
                string jsonString = JsonConvert.SerializeObject(json);
                bw.Write(Encoding.UTF8.GetBytes(jsonString));


                //TODO - make function of this update
                bw.Seek(52, SeekOrigin.Begin);
                bw.Write(jsonPosition);
            }

            return returnvalue;

        }

        private static byte[] CreateHeader()
        {
            List<byte> header = new List<byte>();

            //Magic Number 4 bytes
            header.AddRange(Version.MagicNumber); 
            
            //Version string 32 bytes
            byte[] version = Encoding.UTF8.GetBytes(Version.VersionString);
            //pad or truncate to 32 bytes
            Array.Resize(ref version, 32);
            header.AddRange(version);

            //project uid 16 bytes
            byte[] uid = new byte[16];
            header.AddRange(uid);

            //json Position ulong (8bytes)
            byte[] jsonPosition = new byte[8];
            header.AddRange(jsonPosition);

            return header.ToArray();
        }
    }
}
