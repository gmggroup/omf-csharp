using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMF
{
    /// <summary>
    /// ContentModel is a UidModel with title and description
    /// </summary>
    public class ContentModel : UidModel
    {
        public ContentModel()
        {
            name = "";
            description = "";
        }
        /// <summary>
        /// Title
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        public string description { get; set; }
    }
}