using System;
using Ionic.Zlib;
using System.Collections.Generic;
using System.Linq;

namespace OMF
{
    public static class Extentions
    {

        public static byte[] Compress(this byte[] value)
        {
            // compress
            return ZlibStream.CompressBuffer(value);
        }

        public static byte[] Uncompress(this byte[] value)
        {
            // Uncompress
            return ZlibStream.UncompressBuffer(value);
        }

        public static byte[] CompressSegment(this byte[] value, int fromIndex, int toIndex)
        {
            if (toIndex < fromIndex) return value;

            // make a copy of the array with the segment we need
            int elementCount = toIndex - fromIndex;
            byte[] destination = new byte[elementCount];
            Array.Copy(value, fromIndex, destination, 0, elementCount);

            // Uncompress
            return ZlibStream.CompressBuffer(destination);
        }

        public static byte[] UncompressSegment(this byte[] value, int fromIndex, int toIndex)
        {
            if (toIndex < fromIndex) return value;

            // make a copy of the array with the segment we need
            int elementCount = toIndex - fromIndex;
            byte[] destination = new byte[elementCount];
            Array.Copy(value, fromIndex, destination, 0, elementCount);

            // Uncompress
            return ZlibStream.UncompressBuffer(destination);
        }
        
        public static IEnumerable<string> Chunks(this string str, int chunkSize)
        {
            return Enumerable.Range(0, str.Length / chunkSize)
                .Select(i => str.Substring(i * chunkSize, chunkSize));
        }
    }
}
