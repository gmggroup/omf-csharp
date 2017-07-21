using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace OMF
{
    /// <summary>
    /// UidModel is an object with uid
    /// </summary>
    public class UidModel: ClassBase
    {
        public UidModel()
        {
            uid = Guid.NewGuid();
            date_modified = DateTime.Now;
            date_created = DateTime.Now;
        }
        public UidModel(Guid guid, DateTime modified, DateTime created)
        {
            uid = guid;
            date_modified = modified;
            date_created = created;
        }

        /// <summary>
        /// Unique identifier
        /// </summary>
        [JsonIgnore]
        public Guid uid { get; set; }
        /// <summary>
        /// Date project was modified
        /// </summary>
        [JsonConverter(typeof(OMFDateTimeConverter))]
        public DateTime date_modified { get; private set; }
        /// <summary>
        /// Date project was created
        /// </summary>
        [JsonConverter(typeof(OMFDateTimeConverter))]
        public DateTime date_created { get; private set; }
        
        /// <summary>
        /// Update date_modified if any contained UidModel has been modified
        /// </summary>
        public void  Modify()
        {
            date_modified = DateTime.Now;
        }

    }
    class OMFDateTimeConverter : IsoDateTimeConverter
    {
        public OMFDateTimeConverter()
        {
            base.DateTimeFormat = "yyyy-MM-ddTHH:mm:ssZ";
        }
    }
}
