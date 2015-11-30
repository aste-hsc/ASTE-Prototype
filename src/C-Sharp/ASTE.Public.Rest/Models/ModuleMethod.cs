using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASTE.Public.Rest.Models
{
    /// <summary>
    /// Defines module dependencies
    /// </summary>
    public class ModuleMethod : BaseModel
    {
        /// <summary>
        /// Foreign key to module
        /// </summary>
        public int module_id { get; set; }
        /// <summary>
        /// Navigation property to module
        /// </summary>
        public virtual Module module { get; set; }

    }
}