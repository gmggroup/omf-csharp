using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace OMF.Objects
{
    public class Helpers
    {
        public static List<double[]>ReadDouble3Array(BinaryReader br, ScalarArray array)
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
        public static ScalarArray WriteDouble3Array(BinaryWriter bw, List<double[]> data)
        {
            Objects.ScalarArray array = new Objects.ScalarArray();
            array.start = bw.BaseStream.Position;
            array.length = data.Count * 24;
            array.dtype = "<d8";

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
            bw.Write(compresseddata);

            return array;
        }
        public static ScalarArray WriteInt3Array(BinaryWriter bw, List<int[]> data)
        {
            Objects.ScalarArray array = new Objects.ScalarArray();
            array.start = bw.BaseStream.Position;
            array.length = data.Count * 24;
            array.dtype = "<i8";

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
            bw.Write(compresseddata);

            return array;
        }
        public static ScalarArray WriteInt2Array(BinaryWriter bw, List<int[]> data)
        {
            Objects.ScalarArray array = new Objects.ScalarArray();
            array.start = bw.BaseStream.Position;
            array.length = data.Count * 16;
            array.dtype = "<i8";

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
            bw.Write(compresseddata);

            return array;
        }
    }
}
