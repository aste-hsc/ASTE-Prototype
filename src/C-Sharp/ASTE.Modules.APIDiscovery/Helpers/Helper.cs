using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.ServiceModel.Channels;
using System.Web;

namespace ASTE.Modules.APIDiscovery.Helpers
{
    public class Helper
    {

        public string GetClientIp(HttpRequestMessage request = null)
        {

            if (request.Properties.ContainsKey("MS_HttpContext"))
            {
                return ((HttpContextWrapper)request.Properties["MS_HttpContext"]).Request.UserHostAddress;
            }
            else if (request.Properties.ContainsKey(RemoteEndpointMessageProperty.Name))
            {
                RemoteEndpointMessageProperty prop = (RemoteEndpointMessageProperty)request.Properties[RemoteEndpointMessageProperty.Name];
                return prop.Address;
            }
            else if (HttpContext.Current != null)
            {
                return HttpContext.Current.Request.UserHostAddress;
            }
            else
            {
                return null;
            }
        }
    

        public string GetApiKey(HttpRequestMessage request)
        {
            var api_key = request.Headers.Where(x => x.Key == "api_key").FirstOrDefault();
            if (api_key.Value == null)
                return null;
            else
            {
                return api_key.Value.FirstOrDefault().ToString();
            }
        }


    }
}