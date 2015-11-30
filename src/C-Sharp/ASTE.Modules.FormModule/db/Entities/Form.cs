using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ASTE.Modules.FormModule.db.Entities
{
    /// <summary>
    /// Form entity
    /// </summary>
    public class Form
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public virtual List<FormAnswer> answers { get; set; }
        [DataMember]
        public int form_metadata_id { get; set;}
        [DataMember]
        public virtual FormMetadata form_metadata { get; set; }
    }
}