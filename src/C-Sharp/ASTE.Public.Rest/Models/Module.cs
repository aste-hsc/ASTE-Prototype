using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASTE.Public.Rest.Models
{
    /// <summary>
    /// Module entity
    /// Defines Module data
    /// </summary>
    public class Module : BaseModel
    {

        /// <summary>
        /// Is Module active
        /// </summary>
        [Display(Name="Active")]
        public bool active { get; set; }

        /// <summary>
        /// Module api url
        /// </summary>
        [Display(Name = "Host Url")]
        [Required]
        public string api_url { get; set; }

        /// <summary>
        /// Description of the module
        /// </summary>
        [Display(Name="Description")]
        public string description { get; set; }

        /// <summary>
        /// Documentation URL of the module
        /// </summary>
        [Display(Name = "Documentation Url")]
        public string documentation_url { get; set; }

        /// <summary>
        /// Collection of module dependencies
        /// </summary>
        [Display(Name = "Dependencies")]
        public virtual List<ModuleDependency> my_dependencies { get; set; }

        /// <summary>
        /// Collection on modules dependent on this
        /// </summary>
        public virtual List<ModuleDependency> dependent_on_me { get; set; }
        
        /// <summary>
        /// Public methods to interface
        /// </summary>
        public virtual List<ModuleMethod> methods { get; set; }


    }
}