using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASTE.Modules.APIDiscovery.db.Entities
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

        /// <summary>
        /// Ext. Information about the method
        /// </summary>
        public string methodInfo { get; set; }

    }
}