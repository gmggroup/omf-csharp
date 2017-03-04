using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
namespace OMF
{
    public class OMFWriter
    {
        public OMFWriter()
        {
            LastError = "";
            SurfaceElements = new List<OMF.Objects.SurfaceElement>();
            PointSetElements = new List<OMF.Objects.PointSetElement>();
            LineSetElements = new List<OMF.Objects.LineSetElement>();
            VolumeElements = new List<OMF.Objects.VolumeElement>();
        }
        public string LastError { get; private set; }
        public List<OMF.Objects.SurfaceElement> SurfaceElements { get; set; }
        public List<OMF.Objects.PointSetElement> PointSetElements { get; set; }
        public List<OMF.Objects.VolumeElement> VolumeElements { get; set; }
        public List<OMF.Objects.LineSetElement> LineSetElements { get; set; }

        public bool Write(string file)
        {
            BinaryWriter bw = new BinaryWriter(File.Open(file, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None));

            bool returnvalue = Write(bw);

            bw.Close();

            return returnvalue;
        }
        public bool Write(BinaryWriter bw)
        {
            bool returnvalue = true;

            List<byte> header = new List<byte>();
            header.Add(0x84);
            header.Add(0x83);
            header.Add(0x82);
            header.Add(0x81);
            header.AddRange(ASCIIEncoding.ASCII.GetBytes("OMF-v0.9.0"));
            header.AddRange(new byte[38 - header.Count]);
            header.AddRange(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 });

            //this is where the jsonPosition has to go
            header.AddRange(BitConverter.GetBytes(Convert.ToUInt64(0)));
            bw.Write(header.ToArray());

            Dictionary<string, object> json = new Dictionary<string, object>();
            //now write the binary data
            for(int i=0;i<SurfaceElements.Count;i++)
            {
                SurfaceElements[i].Serialize(json, bw, Guid.NewGuid().ToString());
            }
            for (int i = 0; i < PointSetElements.Count; i++)
            {
                PointSetElements[i].Serialize(json, bw, Guid.NewGuid().ToString());
            }
            for (int i = 0; i < VolumeElements.Count; i++)
            {
                VolumeElements[i].Serialize(json, bw, Guid.NewGuid().ToString());
            }
            for (int i = 0; i < LineSetElements.Count; i++)
            {
                LineSetElements[i].Serialize(json, bw, Guid.NewGuid().ToString());
            }

            UInt64 jsonPosition = (UInt64)bw.BaseStream.Position;

            //jsonstrings to the file
            string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(json);
            bw.Write(Encoding.UTF8.GetBytes(jsonString));

            bw.Seek(52, SeekOrigin.Begin);
            bw.Write(jsonPosition);

            //}
            //catch(Exception ex)
            //{
            // LastError=ex.Message;
            //returnvalue=false;
            //}
            //finally
            //{
            //}

            return returnvalue;

        }

    }
}
