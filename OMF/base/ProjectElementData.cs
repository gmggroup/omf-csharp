using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMF
{
    /// <summary>
    /// Data array with values at specific locations on the mesh
    /// </summary>
    public class ProjectElementData:ContentModel
    {
        public enum LocationType
        {
            vertices,
            segments,
            faces,
            cells
        }

        public LocationType location { get; set; }
        
    }

    
}
