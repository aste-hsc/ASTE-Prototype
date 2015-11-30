using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASTE.Modules.APIDiscovery.Models
{
    /// <summary>
    /// viewmodel for new model or process
    /// </summary>
    public class newModelOrProcess
    {
        [Display(Name = "API Base url")]
        public string url;
        [Display(Name = "Version")]
        public string version;
    }
}