using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASTE.Resources.Models
{
    public class LoggerModel
    {
        public string source { get; set; }
        public string message { get; set; }
        public string eventTimestamp { get; set; }
    }
}
