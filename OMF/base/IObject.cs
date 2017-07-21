using System.Collections.Generic;

namespace OMF
{
    public interface IObject
    {
        string __class__ { get; set; }

        void Deserialize(Dictionary<string, object> json,System.IO.BinaryReader br);
        void Serialize(Dictionary<string, object> json, System.IO.BinaryWriter bw);
    }
}
