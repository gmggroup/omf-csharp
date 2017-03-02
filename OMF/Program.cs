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


            OMF torun = new OMF();
            torun.Execute(file);

        }
    }
}
