using System.Collections.Generic;
using System.Drawing;


namespace OMF
{
    /// <summary>
    /// Base ProjectElement class for OMF file
    ///     ProjectElement subclasses must define their mesh.
    ///     ProjectElements include PointSet, LineSet, Surface, and Volume
    /// </summary>
    public class ProjectElement: ContentModel
    {
        public List<ProjectElementData> data { get; set; }
        public Color color  { get;set; }
        public ProjectElementGeometry geometry { get; set; }
    }
}
