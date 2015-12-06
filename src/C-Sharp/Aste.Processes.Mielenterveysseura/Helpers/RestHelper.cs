using Aste.Processes.Mielenterveysseura.Helpers;
using ASTE.Processes.Mielenterveysseura.Models;
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

namespace ASTE.Processes.Mielenterveysseura.Helpers
{
    /// <summary>
    /// Rest Helper
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RestHelper<T>
    {

        /// <summary>
        /// Logs a message through LoggerModule
        /// </summary>
        /// <param name="version">Version of the logger</param>
        /// <param name="method">Logger method to call</param>
        /// <param name="logger">Log data</param>
        /// <param name="api_url">LoggerModule url</param>
        /// <returns>Log id</returns>
        public async Task<string> Log(string version, string method, LoggerModel logger, string api_url)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(api_url + "/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var callUrl = version + "/" + method ;
                if (logger != null)
                {

                    //var json = JsonConvert.SerializeObject(logger);

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

        /// <summary>
        /// Calls a given module
        /// </summary>
        /// <param name="module">Name of the module</param>
        /// <param name="method">Name of the method</param>
        /// <param name="parameters">Parameters for the process method</param>
        /// <param name="api_url">URL where the process is listening</param>
        /// <returns>json data</returns>
        public async Task<string> GetQuestions(string module, string version, string method, string id, string api_url)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(api_url + "/");
                var callUrl = version + "/" + method + "/" + id;
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

        /// <summary>
        /// Calls the FormModule and saves the given form
        /// </summary>
        /// <param name="module">FormModule name</param>
        /// <param name="version">FormModule version</param>
        /// <param name="method">FormModule method</param>
        /// <param name="parameters">FormAnswers in json</param>
        /// <param name="api_url">FormModule api url</param>
        /// <returns>id of the saved form</returns>
        public async Task<string> SaveForm(string module, string version, string method, List<FormAnswer> parameters, string api_url)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(api_url + "/");

                var callUrl = version + "/" + method + "?";
                if (parameters != null)
                {

                    HttpResponseMessage response = await client.PostAsJsonAsync(callUrl,parameters);
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
            return null;
        }

        /// <summary>
        /// Returns a saved form from the FormModule
        /// </summary>
        /// <param name="module">FormModule name</param>
        /// <param name="version">FormModule version</param>
        /// <param name="method">FormModule method</param>
        /// <param name="id">id of the form</param>
        /// <param name="api_url">FormModule api url</param>
        /// <returns>Form data in json format</returns>
        public async Task<string> GetForm(string module, string version, string method, int id, string api_url)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(api_url + "/");

                var callUrl =  version + "/" + method + "/" + id.ToString();
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
            return null;
        }

        /// <summary>
        /// Returns API information from the API Discovery
        /// </summary>
        /// <param name="module">module name</param>
        /// <param name="method">method name</param>
        /// <param name="api_key">API Key</param>
        /// <param name="version">module version</param>
        /// <returns>Module information</returns>
        public async Task<ModuleInfo> GetConfig(string module, string method, string api_key, string version)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(new ConfigHelper().GetApiDiscoveryUrl() + "/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("api_key", api_key);

                string data = "?module=" + module + "&method=" + method + "&version=" + version + "&source=Mielenterveysseura%20Process";

                HttpResponseMessage response = await client.GetAsync(Constants.ASTE_APIDISCOVERY_MODULE_PREFIX + data);
                if (response.IsSuccessStatusCode)
                {
                    var activemodule = await response.Content.ReadAsAsync<ModuleInfo>();
                    return activemodule;
                }
                else
                {
                    throw new HttpResponseException(response);
                }
            }
            return null;
        }
    }
}