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

            //Write the copy data
            OMFWriter writeData = new OMFWriter();
            var writeStream = new MemoryStream();
            writeData.Write(proj, writeStream);

            //Read the copy data
            var readStream = new MemoryStream(writeStream.ToArray());
            OMFReader readData = new OMFReader();
            var projNew = readData.Read(readStream);


            // test with actual file output

            //try
            //{
            //string fileout = Path.GetTempFileName();
            //writeData.Write(fileout);
            //readData.Read(fileout);
            //    System.IO.File.Delete(fileout);
            //}
            //catch
            //{

            //}

            //Check that the write and read copy data are the same
            Assert.AreEqual(proj.LineSetElements.Count, projNew.LineSetElements.Count, "Mismatch in LineSetElements count vs. write");
            Assert.AreEqual(proj.PointSetElements.Count, projNew.PointSetElements.Count, "Mismatch in PointSetElements count vs. write");
            Assert.AreEqual(proj.SurfaceElements.Count, projNew.SurfaceElements.Count, "Mismatch in SurfaceElements count vs. write");
            Assert.AreEqual(proj.VolumeElements.Count, projNew.VolumeElements.Count, "Mismatch in VolumeElements count vs. write");

        }
    }
}
