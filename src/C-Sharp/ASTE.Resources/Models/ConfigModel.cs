using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASTE.Resources.Models
{
    public class ConfigModel
    {
        public string name { get; set; }
        public string type { get; set; }
        public string[] dependencies { get; set; }
        public List<MethodModel> methods { get; set; }
        public string description { get; set; }
        public string version { get; set; }
        public string author { get; set; }
        public string authorContact { get; set; }
        public string guid { get; set; }
    }
}
