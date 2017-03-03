using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OMF
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            string baseDir = System.AppDomain.CurrentDomain.BaseDirectory;
            string file = System.IO.Path.Combine(baseDir, "..", "..", "test.omf");
            if (System.IO.File.Exists(file) == false)
            {
                Console.WriteLine(string.Format("File '{0}' does not exist.", file));
                Console.ReadLine();
                return;
            }


            OMFReader torun = new OMFReader();
            torun.Execute(file);

            OMFWriter towrite = new OMFWriter();
            towrite.LineSetElements = torun.LineSetElements;
            towrite.PointSetElements = torun.PointSetElements;
            towrite.SurfaceElements = torun.SurfaceElements;
            towrite.VolumeElements = torun.VolumeElements;
            towrite.Execute(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(file), "FileOut.omf"));
        }
    }
}
