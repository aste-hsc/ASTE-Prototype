using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASTE.Processes.Mielenterveysseura.Models
{ 
    /// <summary>
    /// Demo process base model
    /// </summary>
    public class BaseModel
    {
        public int id { get; set; }
        public virtual string name { get; set; }
    }
}