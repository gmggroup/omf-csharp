using System;
using System.Collections.Generic;
using System.IO;

namespace OMF
{
    /// <summary>
    /// OMF Project for serializing to .omf file
    /// </summary>
    public class Project : ContentModel, IObject
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
        public double[] origin { get; set; }

        public void Deserialize(Dictionary<string, object> json, BinaryReader br)
        {
            //Geometry = (LineSetGeometry)ObjectFactory.GetObjectFromGuid(json, br, geometry);
            //Objects = ObjectFactory.DeserializeObjects(json, br, data);
        }

        public void Serialize(Dictionary<string, object> json, BinaryWriter bw, string guid)
        {
            //geometry = ObjectFactory.SerializeObject(Geometry, json, bw);
            //data = ObjectFactory.SerializeObjects(Objects, json, bw);

            //ObjectFactory.GetObjectToData(json, this, guid);
        }
    }
}
