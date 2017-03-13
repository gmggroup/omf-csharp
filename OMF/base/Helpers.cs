using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace OMF.Base
{
    public class Helpers
    {
        public static byte[] GetAttributeByteArray(List<object> data, AttributeDefinitionArray definitions)
        {
            if (definitions.attributenames.Length == definitions.attributetypes.Length && definitions.attributetypes.Length == data.Count)
            {
                List<byte> returnvalue = new List<byte>();
                for (int i = 0; i < data.Count; i++)
                {
                    switch ((AttributeDefinitionArray.AttributeTypeConstants)definitions.attributetypes[i])
                    {
                        case AttributeDefinitionArray.AttributeTypeConstants.datetime:
                            returnvalue.AddRange(BitConverter.GetBytes(Convert.ToDateTime(data[i]).Ticks));
                            break;
                        case AttributeDefinitionArray.AttributeTypeConstants.boolean:
                            returnvalue.AddRange(BitConverter.GetBytes(Convert.ToBoolean(data[i])));
                            break;
                        case AttributeDefinitionArray.AttributeTypeConstants.@double:
                            returnvalue.AddRange(BitConverter.GetBytes(Convert.ToDouble(data[i])));
                            break;
                        case AttributeDefinitionArray.AttributeTypeConstants.integer:
                            returnvalue.AddRange(BitConverter.GetBytes(Convert.ToInt32(data[i])));
                            break;
                        case AttributeDefinitionArray.AttributeTypeConstants.@string:
                            byte[] toadd = Encoding.UTF8.GetBytes(data[i].ToString());
                            //first append the length then the byt array
                            returnvalue.AddRange(BitConverter.GetBytes(toadd.Length));
                            returnvalue.AddRange(toadd);

                            break;
                    }
                }

                return returnvalue.ToArray();
            }
            else
            {
                return null;
            }
        }
        public static List<object> GetAttributeList(byte[] data, AttributeDefinitionArray definitions)
        {
            if (definitions.attributenames.Length == definitions.attributetypes.Length)
            {
                List<object> returnvalue = new List<object>();

                BinaryReader br = new BinaryReader(new MemoryStream(data));

                for (int i = 0; i < data.Length; i++)
                {
                    switch ((AttributeDefinitionArray.AttributeTypeConstants)definitions.attributetypes[i])
                    {
                        case AttributeDefinitionArray.AttributeTypeConstants.datetime:
                            DateTime dateToAdd = DateTime.FromBinary(br.ReadInt64());
                            returnvalue.Add(dateToAdd);
                            break;
                        case AttributeDefinitionArray.AttributeTypeConstants.boolean:
                            returnvalue.Add(br.ReadBoolean());
                            break;
                        case AttributeDefinitionArray.AttributeTypeConstants.@double:
                            returnvalue.Add(br.ReadDouble());
                            break;
                        case AttributeDefinitionArray.AttributeTypeConstants.integer:
                            returnvalue.Add(br.ReadInt32());
                            break;
                        case AttributeDefinitionArray.AttributeTypeConstants.@string:
                            int length = br.ReadInt32();
                            byte[] toadd = br.ReadBytes(length);
                            string stringtoadd = Encoding.UTF8.GetString(toadd);
                            returnvalue.Add(stringtoadd);
                            break;
                    }
                }

                return returnvalue;
            }
            else
            {
                return null;
            }
        }
        public static List<double[]> ReadDouble3Array(BinaryReader br, ScalarArray array)
        {
            List<double[]> Data = new List<double[]>();
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

            return Data;
        }
        public static List<int[]> ReadInt3Array(BinaryReader br, ScalarArray array)
        {
            List<int[]> Data = new List<int[]>();
            br.BaseStream.Seek(array.start, System.IO.SeekOrigin.Begin);

            byte[] data = br.ReadBytes((int)(array.length));

            byte[] newdata = Ionic.Zlib.ZlibStream.UncompressBuffer(data);
            for (int i = 0; i < newdata.Length; i = i + 24)
            {
                int[] toadd = new int[3];
                toadd[0] = BitConverter.ToInt32(newdata, i);
                toadd[1] = BitConverter.ToInt32(newdata, i + 8);
                toadd[2] = BitConverter.ToInt32(newdata, i + 16);
                Data.Add(toadd);
            }

            return Data;
        }
        public static List<int[]> ReadInt2Array(BinaryReader br, ScalarArray array)
        {
            List<int[]> Data = new List<int[]>();
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

            return Data;
        }
        public static byte[] ReadByteArray(BinaryReader br, ScalarArray array)
        {
            List<int[]> Data = new List<int[]>();
            br.BaseStream.Seek(array.start, System.IO.SeekOrigin.Begin);

            byte[] data = br.ReadBytes((int)(array.length));

            byte[] newdata = Ionic.Zlib.ZlibStream.UncompressBuffer(data);

            return newdata;
        }
        public static ScalarArray WriteDouble3Array(BinaryWriter bw, List<double[]> data)
        {


            byte[] towrite = new byte[data.Count * 24];
            int pos = 0;
            for (int i = 0; i < data.Count; i++)
            {
                BitConverter.GetBytes(data[i][0]).CopyTo(towrite, pos);
                pos += 8;
                BitConverter.GetBytes(data[i][1]).CopyTo(towrite, pos);
                pos += 8;
                BitConverter.GetBytes(data[i][2]).CopyTo(towrite, pos);
                pos += 8;
            }

            byte[] compresseddata = Ionic.Zlib.ZlibStream.CompressBuffer(towrite);

            Base.ScalarArray array = new Base.ScalarArray();
            array.start = bw.BaseStream.Position;
            array.length = compresseddata.Length;
            array.dtype = "<d8";

            bw.Write(compresseddata);

            return array;
        }
        public static ScalarArray WriteByteArray(BinaryWriter bw, byte[] data)
        {
            byte[] compresseddata = Ionic.Zlib.ZlibStream.CompressBuffer(data);

            Base.ScalarArray array = new Base.ScalarArray();
            array.start = bw.BaseStream.Position;
            array.length = compresseddata.Length;
            array.dtype = "mixed";

            bw.Write(compresseddata);

            return array;
        }
        public static ScalarArray WriteInt3Array(BinaryWriter bw, List<int[]> data)
        {

            byte[] towrite = new byte[data.Count * 24];
            int pos = 0;
            for (int i = 0; i < data.Count; i++)
            {
                BitConverter.GetBytes(data[i][0]).CopyTo(towrite, pos);
                pos += 8;
                BitConverter.GetBytes(data[i][1]).CopyTo(towrite, pos);
                pos += 8;
                BitConverter.GetBytes(data[i][2]).CopyTo(towrite, pos);
                pos += 8;
            }

            byte[] compresseddata = Ionic.Zlib.ZlibStream.CompressBuffer(towrite);

            Base.ScalarArray array = new Base.ScalarArray();
            array.start = bw.BaseStream.Position;
            array.length = compresseddata.Length;
            array.dtype = "<i8";


            bw.Write(compresseddata);

            return array;
        }
        public static ScalarArray WriteInt2Array(BinaryWriter bw, List<int[]> data)
        {


            byte[] towrite = new byte[data.Count * 16];
            int pos = 0;
            for (int i = 0; i < data.Count; i++)
            {
                BitConverter.GetBytes(data[i][0]).CopyTo(towrite, pos);
                pos += 8;
                BitConverter.GetBytes(data[i][1]).CopyTo(towrite, pos);
                pos += 8;
            }

            byte[] compresseddata = Ionic.Zlib.ZlibStream.CompressBuffer(towrite);
            
            Base.ScalarArray array = new Base.ScalarArray();
            array.start = bw.BaseStream.Position;
            array.length = compresseddata.Length;
            array.dtype = "<i8";

            bw.Write(compresseddata);

            return array;
        }
    }
}
