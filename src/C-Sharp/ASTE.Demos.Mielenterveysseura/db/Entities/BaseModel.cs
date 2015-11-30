using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASTE.Demos.Mielenterveysseura.db.Entities
{
    public class BaseModel
    {
        public int id { get; set; }
        public virtual string name { get; set; }
    }
}