using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ASTE.Modules.FormModule.db.Entities
{
    /// <summary>
    /// Form Metadata entity
    /// </summary>
    public class FormMetadata : BaseModel
    {
        [IgnoreDataMember]
        public virtual List<FormQuestionMetadata> questions { get; set; }
        [IgnoreDataMember]
        public virtual List<Form> forms { get; set; }
    }
}