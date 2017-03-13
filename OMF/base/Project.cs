using System;
using System.Collections.Generic;


namespace OMF
{
    /// <summary>
    /// OMF Project for serializing to .omf file
    /// </summary>
    public class Project : ContentModel
    {
        /// <summary>
        /// Author
        /// </summary>
        public string author { get; set; }
        /// <summary>
        /// Revision
        /// </summary>
        public string revision { get; set; }
        /// <summary>
        /// Date associated with the project data
        /// </summary>
        public DateTime date { get; set; }
        /// <summary>
        /// Spatial units of project
        /// </summary>
        public string units { get; set; }
        /// <summary>
        /// Project Elements
        /// </summary>
        public List<ProjectElement> elements { get; set; }
        /// <summary>
        /// Origin point for all elements in the project
        /// </summary>
        public Tuple<double,double,double> origin { get; set; }
    }
}
