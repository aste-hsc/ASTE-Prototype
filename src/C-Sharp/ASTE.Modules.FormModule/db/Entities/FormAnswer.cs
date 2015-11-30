using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ASTE.Modules.FormModule.db.Entities
{
    /// <summary>
    /// Form answer entity
    /// </summary>
    public class FormAnswer
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public int form_question_metadata_id { get; set; }
        [DataMember]
        public virtual FormQuestionMetadata form_question_metadata { get; set; }
        [DataMember]
        public int form_id { get; set; }
        [IgnoreDataMember]
        public virtual Form form { get; set; }
        [DataMember]
        public string value { get; set; }
    }
}