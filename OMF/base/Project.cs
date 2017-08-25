using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace OMF
{
    /// <summary>
    /// OMF Project for serializing to .omf file
    /// </summary>
    public class Project : ContentModel, IObject
    {
        public Project()
        {
            try
            {
                author = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            }
            catch
            {
                //not being on the domain could cause problems
                author = "";
            }
            SurfaceElements = new List<OMF.SurfaceElement>();
            PointSetElements = new List<OMF.PointSetElement>();
            VolumeElements = new List<OMF.VolumeElement>();
            LineSetElements = new List<OMF.LineSetElement>();
            elements = new List<string>();
            units = "m";
            origin = new double[] { 0, 0, 0 };
            date = DateTime.Now;
            revision = "1";
        }
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
        [JsonConverter(typeof(OMFDateTimeConverter))]
        public DateTime date { get; set; }

        /// <summary>
        /// Spatial units of project
        /// </summary>
        public string units { get; set; }

        /// <summary>
        /// Origin point for all elements in the project
        /// </summary>
        public double[] origin { get; set; }

        /// <summary>
        /// Project Elements
        /// </summary>
        public List<String> elements { get; set; }

        [JsonIgnore]
        public List<SurfaceElement> SurfaceElements { get; set; }
        [JsonIgnore]
        public List<PointSetElement> PointSetElements { get; set; }
        [JsonIgnore]
        public List<VolumeElement> VolumeElements { get; set; }
        [JsonIgnore]
        public List<LineSetElement> LineSetElements { get; set; }



        public void Deserialize(Dictionary<string, object> json, BinaryReader br)
        {
            SurfaceElements = new List<SurfaceElement>();
            PointSetElements = new List<PointSetElement>();
            VolumeElements = new List<VolumeElement>();
            LineSetElements = new List<LineSetElement>();

            foreach (string id in json.Keys)
            {
                string data = json[id].ToString();
                Dictionary<string, object> thisDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(data);

                if (thisDict.ContainsKey("__class__"))
                {
                    switch (thisDict["__class__"].ToString().ToUpper())
                    {
                        case "SURFACEELEMENT":
                            {
                                SurfaceElement obj = (SurfaceElement)ObjectFactory.GetObjectFromData(json, br, data);
                                if (obj != null)
                                {
                                    SurfaceElements.Add(obj);
                                }
                            }
                            break;
                        case "POINTSETELEMENT":
                            {
                                PointSetElement obj = (PointSetElement)ObjectFactory.GetObjectFromData(json, br, data);
                                if (obj != null)
                                {
                                    PointSetElements.Add(obj);
                                }
                            }
                            break;
                        case "VOLUMEELEMENT":
                            {
                                VolumeElement obj = (VolumeElement)ObjectFactory.GetObjectFromData(json, br, data);
                                if (obj != null)
                                {
                                    VolumeElements.Add(obj);
                                }
                            }
                            break;
                        case "LINESETELEMENT":
                            {
                                LineSetElement obj = (LineSetElement)ObjectFactory.GetObjectFromData(json, br, data);
                                if (obj != null)
                                {
                                    LineSetElements.Add(obj);
                                }
                            }
                            break;
                        default:

                            break;
                    }
                }

            }
        }

        public void Serialize(Dictionary<string, object> json, BinaryWriter bw)
        {
            elements = new List<string>();

            if (PointSetElements != null)
                foreach (var PointSet in PointSetElements)
                {
                    PointSet.Serialize(json, bw);
                    elements.Add(PointSet.uid.ToString());
                }


            if (LineSetElements != null)
                foreach (var LineSet in LineSetElements)
                {
                    LineSet.Serialize(json, bw);
                    elements.Add(LineSet.uid.ToString());
                }

            if (SurfaceElements != null)
                foreach (var Surface in SurfaceElements)
                {
                    Surface.Serialize(json, bw);
                    elements.Add(Surface.uid.ToString());
                }

            if (VolumeElements != null)
                foreach (var VolumeElement in VolumeElements)
                {
                    VolumeElement.Serialize(json, bw);
                    elements.Add(VolumeElement.uid.ToString());
                }
        }
    }


    public class ProjectDictionary : Dictionary<string, JRaw>
    {
        public ProjectDictionary(Dictionary<string, object> project) : base(FromStringDict(project)) { }
        private static Dictionary<string, JRaw> FromStringDict(Dictionary<string, object> project)
        {
            Dictionary<string, JRaw> base_dict = new Dictionary<string, JRaw>();

            foreach (KeyValuePair<string, object> kvp in project)
            {
                base_dict.Add(kvp.Key, new JRaw(kvp.Value));
            }
            return base_dict;
        }
    }
}
