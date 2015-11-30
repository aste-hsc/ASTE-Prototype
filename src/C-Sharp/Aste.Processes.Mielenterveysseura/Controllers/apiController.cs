using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using ASTE.Processes.Mielenterveysseura.Helpers;
using ASTE.Processes.Mielenterveysseura.Models;
using Newtonsoft.Json.Linq;
using ASTE.Resources.Models;
using ASTE.Resources;

namespace ASTE.Processes.Mielenterveysseura.Controllers
{
    /// <summary>
    /// API Controller for the Demo process
    /// </summary>
    [RoutePrefix("Mielenterveysseura/1.0")]
    public class apiController : ApiController
    {
        private string api_key { get; set; }

        /// <summary>
        /// Mandatory ping method for the API Discovery
        /// </summary>
        /// <returns>ping status</returns>
        [HttpGet]
        [Route("ping")]
        public string Ping()
        {

            PingModel model = new PingModel();
            model.status = Constants.ASTE_PINGSTATUS_SUCCESS;
            model.message = "";
            var json = JsonConvert.SerializeObject(model);

            return json;
        }

        /// <summary>
        /// Mandatory config method for the API Discovery
        /// </summary>
        /// <returns>config data</returns>
        [HttpGet]
        [Route("config")]
        public string Config()
        {

            ConfigModel model = new ConfigModel();
            model.name = "Mielenterveysseura";
            model.guid = Constants.ASTE_PROCESS_MIELENTERVEYSSEURA;
            model.description = "example process";
            model.author = "Codecontrol Oy";
            model.authorContact = "http://www.codecontrol.fi";
            model.dependencies = new string[] { Constants.ASTE_MODULES_API_DISCOVERY_GUID, Constants.ASTE_MODULES_FORM_MODULE_GUID, Constants.ASTE_MODULES_LOGGER };
            model.version = "1.0";
            model.type = Constants.ASTE_MODULETYPE_PROCESS;
            model.methods = new List<MethodModel>();
            model.methods.Add(new MethodModel()
            {
                method = "lokikirjaKysymykset",
                @params = ""
            });
            model.methods.Add(new MethodModel()
            {
                method = "tallennaLokikirja",
                @params = "FormAnswer objects in json format: "
            });
            model.methods.Add(new MethodModel()
            {
                method = "haeLokikirja",
                @params = "int form_id"
            });

            var json = JsonConvert.SerializeObject(model);
            return json;
        }

        /// <summary>
        /// Returns predefined questions from the Demo form
        /// </summary>
        /// <returns>Questions from the demo form through FormModule</returns>
        [HttpGet]
        [Route("lokikirjaKysymykset")]
        public async Task<string> LokikirjaKysymykset()
        {
            check_api_key();

            //Create Resthelper
            RestHelper<object> rh = new RestHelper<object>();

            var logModule = await rh.GetConfig("LoggerModule", "log", api_key, "1.0");
            if(logModule != null)
            {
                 //Get process url from API Data
                var url = logModule.api_url;

                LoggerModel lm = new LoggerModel()
                {
                     message = string.Format("lokikirjakysymykset called from {0}","Lokikirja"),
                     source = "Tonin megaboksi",
                     eventTimestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm")
                };

                //Call module
                var response = await rh.Log(logModule.version, "log", lm, logModule.api_url);
            }

            //Get Module info from API Discovery
            var data = await rh.GetConfig("FormModule", "getQuestions", api_key, "1.0");

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
                var response = await rh.CallModule(data.name, "1.0", "GetQuestions","id=1", url);
                //Return Data to APP
                return response;

            }
        }

        /// <summary>
        /// Saves Answers from the Demo application through FormModule
        /// </summary>
        /// <param name="json">Answers in json format</param>
        /// <returns>id of the saved form, through FormModule</returns>
        [HttpGet]
        [HttpPost]
        [Route("tallennaLokikirja")]
        public async Task<string> TallennaLokikirja(JObject json)
        {
            var json_array = json.GetValue("json");
            JArray array = JArray.Parse(json_array.ToString());

            int index = 0;
            List<FormAnswer> answers = new List<FormAnswer>();
            foreach (JObject content in array.Children<JObject>())
            {

                var allChildren = array.Children<JObject>().Where(x => x.First.FirstOrDefault().ToString().Contains("[" + index.ToString() + "]"));
                FormAnswer answer = new FormAnswer();
                if (allChildren != null && allChildren.Count() > 0)
                {
                    foreach (var c in allChildren)
                    {
                        var property = c.First.FirstOrDefault().ToString();
                        var propertyPrefix = property.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
                        var propertyValue = c.Last.FirstOrDefault().ToString();
                        switch (propertyPrefix.Last().ToString())
                        {
                            case "form_question_metadata_id":
                                answer.form_question_metadata_id = int.Parse(propertyValue);
                                break;
                            case "form_id":
                                answer.form_id = int.Parse(propertyValue);
                                break;
                            case "value":
                                answer.value = propertyValue;
                                break;
                        }
                          
                    }
                    answers.Add(answer);
                }
                index++;

            }

            check_api_key();

            //Create Resthelper
            RestHelper<object> rh = new RestHelper<object>();

            //Get Module info from API Discovery
            var data = await rh.GetConfig("FormModule", "saveForm", api_key, "1.0");

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
                var response = await rh.SaveForm(data.name, "1.0", "saveForm", answers, url);
                return response;
            }

        }


        /// <summary>
        /// Fetches a given form through FormModule
        /// </summary>
        /// <param name="json">form id in json</param>
        /// <returns>Form data in json format through FormModule</returns>
        [HttpGet]
        [HttpPost]
        [Route("haeLokikirja")]
        public async Task<string> HaeLokikirja(JObject json)
        {
            var form_value= json.GetValue("json");
            check_api_key();

            //Create Resthelper
            RestHelper<object> rh = new RestHelper<object>();

            //Get Module info from API Discovery
            var data = await rh.GetConfig("FormModule", "getForm", api_key, "1.0");

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
                var id = int.Parse(form_value.Last.FirstOrDefault().ToString());
                var response = await rh.GetForm(data.name, "1.0", "getForm", id, url);
                return response;
            }

        }

        /// <summary>
        /// Validates api key
        /// </summary>
        private void check_api_key()
        {
            var _api_key = Request.Headers.Where(x => x.Key == "api_key").FirstOrDefault();

            if (_api_key.Value == null)
            {
                HttpResponseMessage response = this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "API Key missing.");
                throw new HttpResponseException(response);
            }
            else
            {
                this.api_key = _api_key.Value.FirstOrDefault().ToString();
            }
        }
    }
}