using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASTE.Processes.Mielenterveysseura.Models
{
    /// <summary>
    /// Demo process form metadata model
    /// </summary>
    public class FormMetadata : BaseModel
    {
        public virtual List<FormQuestionMetadata> questions { get; set; }
        public virtual List<Form> forms { get; set; }
    }
}