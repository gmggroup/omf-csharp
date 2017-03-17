using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OMF;
using System.IO;
namespace OMFTests
{
    [TestFixture]
    public class ReaderTests
    {
        [Test]
        public void RoundTrip()
        {
            // Retrieve data from Resources
            System.Resources.ResourceManager rm = Resources.ResourceManager;
            byte[] bytes = (byte[])rm.GetObject("Wolfpass");
            Stream originalStream = new MemoryStream(bytes);
            
            //Read in the original data
            OMFReader originalData = new OMFReader();
            Project proj = originalData.Read(originalStream);
            
            //Make a copy
            OMFWriter writeData = new OMFWriter();
            writeData.LineSetElements = originalData.LineSetElements;
            writeData.PointSetElements = originalData.PointSetElements;
            writeData.SurfaceElements = originalData.SurfaceElements;
            writeData.VolumeElements = originalData.VolumeElements;

            //Write the copy data
            var writeStream = new MemoryStream();
            writeData.Write(writeStream);

            //Read the copy data
            var readStream = new MemoryStream(writeStream.ToArray());
            OMFReader readData = new OMFReader();
            proj = readData.Read(readStream);


            // test with actual file output

            //try
            //{
            string fileout = Path.GetTempFileName();
            writeData.Write(fileout);
            //readData.Read(fileout);
            //    System.IO.File.Delete(fileout);
            //}
            //catch
            //{

            //}

            //Check that the write and read copy data are the same
            Assert.AreEqual(writeData.LineSetElements.Count, readData.LineSetElements.Count, "Mismatch in LineSetElements count vs. write");
            Assert.AreEqual(writeData.PointSetElements.Count, readData.PointSetElements.Count, "Mismatch in PointSetElements count vs. write");
            Assert.AreEqual(writeData.SurfaceElements.Count, readData.SurfaceElements.Count, "Mismatch in SurfaceElements count vs. write");
            Assert.AreEqual(writeData.VolumeElements.Count, readData.VolumeElements.Count, "Mismatch in VolumeElements count vs. write");

            //Check that the original and read copy data are the same
            Assert.AreEqual(originalData.LineSetElements.Count, readData.LineSetElements.Count, "Mismatch in LineSetElements count vs. original");
            Assert.AreEqual(originalData.PointSetElements.Count, readData.PointSetElements.Count, "Mismatch in PointSetElements count vs. original");
            Assert.AreEqual(originalData.SurfaceElements.Count, readData.SurfaceElements.Count, "Mismatch in SurfaceElements count vs. original");
            Assert.AreEqual(originalData.VolumeElements.Count, readData.VolumeElements.Count, "Mismatch in VolumeElements count vs. original");

        }
    }
}
