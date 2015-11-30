using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASTE.Modules.APIDiscovery.Models
{
    public class ModuleInfo
    {
        public string name { get; set; }
        public string api_url { get; set; }

        public string version { get; set; }
    }
}