using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;

namespace ASTE.Modules.FormModule.db.Entities
{
    /// <summary>
    /// Form Question metadata entity
    /// </summary>
    [Serializable, XmlRoot, DataContract]
    public class FormQuestionMetadata : BaseModel
    {
        [DataMember]
        public string question { get; set; }

        [DataMember]
        public int ordernumber { get; set; }

        [DataMember]
        public int form_metadata_id { get; set; }
        [IgnoreDataMember]
        public virtual FormMetadata form_metadata { get; set; }
        [IgnoreDataMember]
        public virtual List<FormAnswer> answers { get; set; }
    }
}