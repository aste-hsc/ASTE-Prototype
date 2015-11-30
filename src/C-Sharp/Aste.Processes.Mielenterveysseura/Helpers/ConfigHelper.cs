using System;
using ASTE.Resources;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Aste.Processes.Mielenterveysseura.Helpers
{
    /// <summary>
    /// General Config helper
    /// </summary>
    public class ConfigHelper
    {
        /// <summary>
        /// Return API Discovery url from web.config
        /// </summary>
        /// <returns></returns>
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