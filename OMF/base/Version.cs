using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMF
{
    static public class Version
{
        static public byte[] MagicNumber = new byte[] { 0x84, 0x83, 0x82, 0x81 };
        static public string VersionString = "OMF-v0.9.0";
    }
}
