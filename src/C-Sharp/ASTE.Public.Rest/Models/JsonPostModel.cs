using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASTE.Public.Rest.Models
{
    /// <summary>
    /// JSON Post model
    /// </summary>
    public class JsonPostModel
    {
        public string process { get; set; }
        public string method { get; set; }
        public string version { get; set; }
        public List<dynamic> parameters { get; set; }
    }
}