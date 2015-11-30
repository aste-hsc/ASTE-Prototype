using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASTE.Processes.Mielenterveysseura.Models
{ 
    /// <summary>
    /// Demo process form model
    /// </summary>
    public class Form
    {
        public int id { get; set; }
        public virtual List<FormAnswer> answers { get; set; }
        public int form_metadata_id { get; set;}
        public virtual FormMetadata form_metadata { get; set; }
    }
}