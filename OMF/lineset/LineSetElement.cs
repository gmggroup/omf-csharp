﻿using Newtonsoft.Json;
using OMF.Base;
using System.Collections.Generic;
using System.IO;

namespace OMF
{
    public class LineSetElement : ContentModel, IObject
    {
        public LineSetElement()
        {

        }
        public LineSetElement(List<double[]> verts, List<int[]> segments) : this()
        {
            Geometry = new LineSetGeometry(verts, segments);
        }
        public byte[] color { get; set; }
        public string subtype { get; set; }
        public string geometry { get; set; }
        public string[] data { get; set; }

        [JsonIgnore]
        public LineSetGeometry Geometry { get; set; }

        [JsonIgnore]
        public List<IObject> Objects { get; set; }

        public void Deserialize(Dictionary<string, object> json, BinaryReader br)
        {
            Geometry = (LineSetGeometry)ObjectFactory.GetObjectFromGuid(json, br, geometry);
            Objects = ObjectFactory.DeserializeObjects(json, br, data);
        }

        public void Serialize(Dictionary<string, object> json, BinaryWriter bw, string guid)
        {
            geometry = ObjectFactory.SerializeObject(Geometry, json, bw);
            data = ObjectFactory.SerializeObjects(Objects, json, bw);

            ObjectFactory.GetObjectToData(json, this, guid);
        }
    }
}