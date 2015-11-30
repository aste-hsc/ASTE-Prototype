using ASTE.Public.Rest.Models;
using ASTE.Resources;
using ASTE.Resources.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ASTE.Public.Rest.Helpers
{
    /// <summary>
    /// Helper for ASTE REST calls
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RestHelper<T>
    {
        /// <summary>
        /// Calls a given process
        /// </summary>
        /// <param name="process">Name of the process</param>
        /// <param name="method">Name of the method</param>
        /// <param name="parameters">Parameters for the process method</param>
        /// <param name="api_url">URL where the process is listening</param>
        /// <returns></returns>
        public async Task<string> CallProcess(string process, string version, string method, dynamic parameters, string api_url, string api_key)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(api_url + "/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("api_key", api_key);

                var callUrl = version + "/" + method;
                if (parameters != null)
                {
                    string json_data = Newtonsoft.Json.JsonConvert.SerializeObject(parameters);
                    var json = "{ \"json\" : " + json_data + " }";
                    HttpResponseMessage response = await client.PostAsync(callUrl, new StringContent(json, Encoding.UTF8, "application/json"));
                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsAsync<string>();
                        return data;

                    }
                    else
                    {
                        throw new HttpResponseException(response);
                    }
                }
                else
                {
                    HttpResponseMessage response = await client.GetAsync(callUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsAsync<string>();
                        return data;

                    }
                    else
                    {
                        throw new HttpResponseException(response);
                    }
                }

            }
        }

        /// <summary>
        /// Returns module information from API Discovery
        /// </summary>
        /// <param name="process">Name of the process</param>
        /// <param name="method">NAme of the method</param>
        /// <param name="api_key">API Key</param>
        /// <param name="version">Version of the process</param>
        /// <returns></returns>
        public async Task<ModuleInfo> GetConfig(string process, string method, string api_key,string version)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(new ConfigHelper().GetApiDiscoveryUrl() + "/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("api_key", api_key);

                string data = "?process=" + process + "&method=" + method + "&version=" + version + "&source=Public API";

                HttpResponseMessage response = await client.GetAsync(Constants.ASTE_APIDISCOVERY_PROCESS_PREFIX + data);
                if (response.IsSuccessStatusCode)
                {
                    var module = await response.Content.ReadAsAsync<ModuleInfo>();
                    return module;
                }
                else
                {
                    throw new HttpResponseException(response);
                }
            }
        }
    }
}