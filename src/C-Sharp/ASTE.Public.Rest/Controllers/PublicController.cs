using ASTE.Public.Rest.Helpers;
using ASTE.Public.Rest.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace ASTE.Public.Rest.Controllers
{
    /// <summary>
    /// Public REST API
    /// </summary>
    [RoutePrefix("api/1.0")]
    public class PublicController : ApiController
    {
        /// <summary>
        /// redirects a Get call to given process
        /// </summary>
        /// <param name="json">Incoming Json data</param>
        /// <returns>JSON String</returns>
        [HttpGet]
        [Route("process")]
        public async Task<string> Process(string json)
        {
            //Serialize given json to model
            dynamic model = JsonConvert.DeserializeObject(json.ToString());
            //Get Api key from request headers
            var api_key = Request.Headers.Where(x => x.Key == "api_key").FirstOrDefault();
            
            if (api_key.Value != null)
            {
                string process = "";
                string method = "";
                string version = "";

                //Check mandatory values
                if (model["process"] == null)
                {
                    HttpResponseMessage response = this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Mandatory parameter missing: process");
                    throw new HttpResponseException(response);
                }
                else
                {
                    //Get process to be called from model
                    process = model["process"];
                }
                if (model["method"] == null)
                {
                    HttpResponseMessage response = this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Mandatory parameter missing: method");
                    throw new HttpResponseException(response);
                }
                else
                {
                    //Get process method from model
                    method = model["method"];
                }
                if (model["version"] == null)
                {
                    HttpResponseMessage response = this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Mandatory parameter missing: version");
                    throw new HttpResponseException(response);
                }
                else
                {
                    //Get Version
                    version = model["version"];
                }


                
                //Create Resthelper
                RestHelper<object> rh = new RestHelper<object>();

                //Get Process info from API Discovery
                var data = await rh.GetConfig(process, method, api_key.Value.FirstOrDefault().ToString(), version);

                if(data == null)
                {
                    //If process was not found, or process is not activated, return bad request
                    HttpResponseMessage response = this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Process not found.");
                    throw new HttpResponseException(response);
                }
                
                else
                {
                    //Get process url from API Data
                    var url = data.api_url;
                    //Call Process
                    var response = await rh.CallGetProcess(data.name,version, method, model["parameters"], url, api_key.Value.FirstOrDefault().ToString());
                    //Return Data to APP
                    return response;

                }
            }
            else
            {
                //API Key missing, returning bad request
                HttpResponseMessage response = this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "API key missing.");
                throw new HttpResponseException(response);
            }

        }

        /// <summary>
        /// Redirects a Post call to given process
        /// </summary>
        /// <param name="json">Incoming Json data</param>
        /// <returns>JSON String</returns>
        [HttpPost]
        [Route("process")]
        public async Task<string> Process(JObject json)
        {
            //Serialize given json to model

            JsonPostModel model = json.ToObject<JsonPostModel>();
            //Get Api key from request headers
            var api_key = Request.Headers.Where(x => x.Key == "api_key").FirstOrDefault();

            if (api_key.Value != null)
            {
                string process = "";
                string method = "";
                string version = "";

                //Check mandatory values
                if (model.process == null)
                {
                    HttpResponseMessage response = this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Mandatory parameter missing: process");
                    throw new HttpResponseException(response);
                }
                else
                {
                    //Get process to be called from model
                    process = model.process;
                }
                if (model.method == null)
                {
                    HttpResponseMessage response = this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Mandatory parameter missing: method");
                    throw new HttpResponseException(response);
                }
                else
                {
                    //Get process method from model
                    method = model.method;
                }
                if (model.version == null)
                {
                    HttpResponseMessage response = this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Mandatory parameter missing: version");
                    throw new HttpResponseException(response);
                }
                else
                {
                    //Get Version
                    version = model.version;
                }



                //Create Resthelper
                RestHelper<object> rh = new RestHelper<object>();

                //Get Process info from API Discovery
                var data = await rh.GetConfig(process, method, api_key.Value.FirstOrDefault().ToString(), "1.0");

                if (data == null)
                {
                    //If process was not found, or process is not activated, return bad request
                    HttpResponseMessage response = this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Process not found.");
                    throw new HttpResponseException(response);
                }

                else
                {
                    //Get process url from API Data
                    var url = data.api_url;
                    //Call Process
                    var response = await rh.CallPostProcess(data.name, version, method, model.parameters, url, api_key.Value.FirstOrDefault().ToString());
                    //Return Data to APP
                    return response;
                }
            }
            else
            {
                //API Key missing, returning bad request
                HttpResponseMessage response = this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "API key missing.");
                throw new HttpResponseException(response);
            }

        }


    }

}
