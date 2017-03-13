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
    public class ProjectElementData<T>:ContentModel
    {
        public enum LocationType
        {
            vertices,
            segments,
            faces,
            cells
        }

        public LocationType location { get; set; }
        public virtual List<T> array { get; set; }

    }

    
}
