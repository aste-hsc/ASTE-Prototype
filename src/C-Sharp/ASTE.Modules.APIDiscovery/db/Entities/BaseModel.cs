using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASTE.Modules.APIDiscovery.db.Entities
{
    /// <summary>
    /// API Discovery base model
    /// </summary>
    public class BaseModel
    {
        [Required]
        public int id { get; set; }

        [Required]
        public string name { get; set; }

        public DateTime? modified { get; set; }

        [Required]
        public DateTime created { get; set; }

        public bool isdeleted { get; set; }

    }
}