using System;
using ASTE.Resources;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ASTE.Public.Rest.Helpers
{
    /// <summary>
    /// General Config helper
    /// </summary>
    public class ConfigHelper
    {
        /// <summary>
        /// Returns API Discovery url from web.config
        /// </summary>
        /// <returns>API Discovery url</returns>
        public string GetApiDiscoveryUrl()
        {
            var value = ConfigurationManager.AppSettings[Constants.API_DISCOVERY_URL_KEY];
            if(value == null)
            {
                throw new ArgumentException("Api discovery URL not set in web.config.");
            }
            else
            {
                return value;
            }
        }
    }
}