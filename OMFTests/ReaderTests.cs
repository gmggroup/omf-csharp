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
            OMFReader torun = new OMFReader();

            System.Resources.ResourceManager rm = Resources.ResourceManager;
            byte[] bytes = (byte[])rm.GetObject("Wolfpass");
            Stream s = new MemoryStream(bytes);
            BinaryReader br = new BinaryReader(s);

            torun.Read(br);

            string fileout = System.IO.Path.GetTempFileName();

            OMFWriter towrite = new OMFWriter();
            towrite.LineSetElements = torun.LineSetElements;
            towrite.PointSetElements = torun.PointSetElements;
            towrite.SurfaceElements = torun.SurfaceElements;
            towrite.VolumeElements = torun.VolumeElements;
            towrite.Write(fileout);


            OMFReader roundTrip = new OMF.OMFReader();
            roundTrip.Read(fileout);

            try
            {
                System.IO.File.Delete(fileout);
            }
            catch
            {

            }

            Assert.AreEqual(towrite.LineSetElements.Count, roundTrip.LineSetElements.Count, "Mismatch in LineSetElements count");
            Assert.AreEqual(towrite.PointSetElements.Count, roundTrip.PointSetElements.Count, "Mismatch in PointSetElements count");
            Assert.AreEqual(towrite.SurfaceElements.Count, roundTrip.SurfaceElements.Count, "Mismatch in SurfaceElements count");
            Assert.AreEqual(towrite.VolumeElements.Count, roundTrip.VolumeElements.Count, "Mismatch in VolumeElements count");

        }
    }
}
