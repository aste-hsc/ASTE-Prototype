using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASTE.Processes.Mielenterveysseura.Models
{
    /// <summary>
    /// Demo process Form answer model
    /// </summary>
    public class FormAnswer
    {
        public int id { get; set; }

        public int form_question_metadata_id { get; set; }
        public virtual FormQuestionMetadata form_question_metadata { get; set; }

        public int form_id { get; set; }
        public virtual Form form { get; set; }

        public string value { get; set; }
    }
}