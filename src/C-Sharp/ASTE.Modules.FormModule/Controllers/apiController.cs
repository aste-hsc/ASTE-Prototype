using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ASTE.Modules.FormModule.db;
using ASTE.Modules.FormModule.db.Context;
using Newtonsoft.Json;
using System.IO;
using System.Runtime.Serialization.Json;
using ASTE.Modules.FormModule.db.Entities;
using ASTE.Resources.Models;
using ASTE.Resources;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Newtonsoft.Json.Linq;

namespace ASTE.Modules.FormModule.Controllers
{
    /// <summary>
    /// API Controller for FormModule version 1.0
    /// </summary>
    [RoutePrefix("FormModule/1.0")]
    public class apiController : ApiController
    {
        /// <summary>
        /// Mandatory ping method for API Discovery
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
        /// Mandatory config method for API Discovery
        /// </summary>
        /// <returns>config data</returns>
        [HttpGet]
        [Route("config")]
        public string Config()
        {

            ConfigModel model = new ConfigModel();
            model.name = "FormModule";
            model.guid = Constants.ASTE_MODULES_FORM_MODULE_GUID;
            model.description = "Module for Q/A Form handling";
            model.author = "Codecontrol Oy";
            model.authorContact = "http://www.codecontrol.fi";
            model.dependencies = new string[] { Constants.ASTE_MODULES_API_DISCOVERY_GUID, Constants.ASTE_MODULES_LOGGER };
            model.version = "1.0";
            model.type = Constants.ASTE_MODULETYPE_MODULE;
            model.methods = new List<MethodModel>();
            model.methods.Add(new MethodModel()
            {
                method = "getQuestions",
                @params = "int id"
            });
            model.methods.Add(new MethodModel()
            {
                method = "saveForm",
                @params = "List<FormAnswer> answers"
            });
            model.methods.Add(new MethodModel()
            {
                method = "getForm",
                @params = "int id"
            });

            var json = JsonConvert.SerializeObject(model);
            return json;
        }

        /// <summary>
        /// Returns Questions from the db
        /// </summary>
        /// <param name="id">form_metadata_id id</param>
        /// <returns>Questions in json format</returns>
        [Route("getQuestions")]
        public string GetQuestions(int id)
        {
            FormModuleContext ctx = new FormModuleContext();
            List<db.Entities.FormQuestionMetadata> questions = ctx.form_questions.Where(x => x.form_metadata_id == id).ToList();
            if(questions == null)
            {
                throw new ArgumentException("Form Questions not found!");
            }
            var json = JsonConvert.SerializeObject(questions);
            return json;
            
        }

        /// <summary>
        /// Saves the Answers to db
        /// </summary>
        /// <param name="answers">List of FormAnswers to be saved</param>
        /// <returns>id of the saved form data</returns>
        [HttpPost]
        [Route("saveForm")]
        public string SaveForm(List<FormAnswer> answers)
        {

            FormModuleContext ctx = new FormModuleContext();
            if(answers != null && answers.Count > 0)
            {
                Form form = new Form();
                form.id = answers[0].form_id;
                var q_id = answers[0].form_question_metadata_id;
                var q = ctx.form_questions.Where(x => x.id == q_id ).FirstOrDefault();
                form.form_metadata_id = q.form_metadata_id;
                form.answers = answers;
                ctx.forms.Add(form);
                ctx.SaveChanges();

                return "{ \"form_id\" : " + form.id.ToString() + " }";
            }
            return null;


            
        }

        /// <summary>
        /// Return a saved form from db
        /// </summary>
        /// <param name="id">form id to fetch</param>
        /// <returns>Form data in json</returns>
        [HttpGet]
        [Route("getForm")]
        public string GetForm(int id)
        {
 
            FormModuleContext ctx = new FormModuleContext();
            var result = ctx.forms.Where(x => x.id == id).FirstOrDefault();
            result.answers = ctx.form_answers.Where(x => x.form_id == result.id).ToList();
            result.form_metadata = ctx.form_metadatas.Where(x => x.id == result.form_metadata_id).FirstOrDefault();
            result.form_metadata.questions = ctx.form_questions.Where(x => x.form_metadata_id == result.form_metadata_id).ToList();
            if(result != null)
            {
                return JsonConvert.SerializeObject(result);
            }
            else
            {
                throw new ArgumentException("Form not found!");
            }
            
        }
    }
}
