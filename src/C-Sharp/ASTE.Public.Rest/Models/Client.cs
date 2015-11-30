using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASTE.Public.Rest.Models
{
    /// <summary>
    /// Application Clients using ASTE 
    /// </summary>
    public class Client : BaseModel
    {
        /// <summary>
        /// API Access key
        /// </summary>
        [Display(Name = "Api key")]
        public string api_key { get; set; }

        /// <summary>
        /// Name of the client application
        /// </summary>
        [Display(Name = "Client name")]
        public string client_name { get; set; }

        /// <summary>
        /// IP of the client application
        /// </summary>
        [Display(Name = "Client IP Address")]
        public string client_ip { get; set; }


    }
}