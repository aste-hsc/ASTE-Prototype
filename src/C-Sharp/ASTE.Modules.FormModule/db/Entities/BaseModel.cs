using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Xml.Serialization;

namespace ASTE.Modules.FormModule.db.Entities
{
    /// <summary>
    /// FormModule db Base model
    /// </summary>
    [Serializable, XmlRoot, DataContract]
    public class BaseModel
    {
        [DataMember]
        public int id { get; set; }
        [DataMember]
        public virtual string name { get; set; }
    }
}