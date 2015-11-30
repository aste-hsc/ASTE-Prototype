using ASTE.Modules.APIDiscovery.db.Context;
using ASTE.Resources;
using ASTE.Resources.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ASTE.Modules.APIDiscovery.Helpers
{
    public class RestHelper
    {
        public async Task<ASTE.Resources.Models.PingModel> Ping(string url, string version)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url + "/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // New code:
                HttpResponseMessage response = await client.GetAsync(version + "/ping");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsAsync<object>();
                    var json_data = json.ToString();
                    var pingmodel = JsonConvert.DeserializeObject<Resources.Models.PingModel>(json_data);
                    return pingmodel;
                }
                else
                {
                    throw new HttpResponseException(response);
                }
            }
        }

        public async Task<ASTE.Resources.Models.ConfigModel> Config(string url, string version)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url + "/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // New code:
                HttpResponseMessage response = await client.GetAsync(version + "/config");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsAsync<object>();
                    var json_data = json.ToString();
                    var configmodel = JsonConvert.DeserializeObject<Resources.Models.ConfigModel>(json_data);
                    return configmodel;
                }
                else
                {
                    throw new HttpResponseException(response);
                }
            }
        }

        public async Task<string> Log(string message)
        {
            LoggerModel logger = new LoggerModel();
            logger.eventTimestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            logger.source = "API DISCOVERY";
            logger.message = message;

            APIDiscoveryContext ctx = new APIDiscoveryContext();
            var logmodule = ctx.modules.Where(x => x.guid == Constants.ASTE_MODULES_LOGGER && !x.isdeleted && x.active && x.version == "1.0").FirstOrDefault();
            if(logmodule == null)
            {
                throw new HttpRequestException("Mandatory module not installed: LoggerModule");
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(logmodule.api_url + "/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var callUrl = logmodule.version + "/log";
                if (logger != null)
                {
                    HttpResponseMessage response = await client.PutAsJsonAsync(callUrl, logger);
                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsAsync<object>();
                        return data.ToString();

                    }
                    else
                    {
                        throw new HttpResponseException(response);
                    }
                }
            }
            return null;
        }
    }
}