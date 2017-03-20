using Newtonsoft.Json;
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
        /// Origin point for all elements in the project
        /// </summary>
        public double[] origin { get; set; }

        /// <summary>
        /// Project Elements
        /// </summary>
        public string elements { get; set; }

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

        public void Serialize(Dictionary<string, object> json, BinaryWriter bw, string guid)
        {
            if (PointSetElements != null)
                PointSetElements.ForEach(elem => elem.Serialize(json, bw, Guid.NewGuid().ToString()));

            if (LineSetElements != null)
                LineSetElements.ForEach(elem => elem.Serialize(json, bw, Guid.NewGuid().ToString()));

            if (SurfaceElements != null)
                SurfaceElements.ForEach(elem => elem.Serialize(json, bw, Guid.NewGuid().ToString()));

            if (VolumeElements != null)
                VolumeElements.ForEach(elem => elem.Serialize(json, bw, Guid.NewGuid().ToString()));
            
            ObjectFactory.GetObjectToData(json, this, guid);
        }
    }
}
