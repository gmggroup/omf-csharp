using System;
using System.Collections.Generic;


namespace OMF
{
    /// <summary>
    /// OMF Project for serializing to .omf file
    /// </summary>
    public class Project : ContentModel
    {
        public string author { get; set; }
        public string revision { get; set; }
        public DateTime date { get; set; }
        public string units { get; set; }
        public List<ProjectElement> elements { get; set; }
        public Tuple<double,double,double> origin { get; set; }
    }
}
