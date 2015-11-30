using ASTE.Modules.APIDiscovery.db.Context;
using ASTE.Modules.APIDiscovery.db.Entities;
using ASTE.Modules.APIDiscovery.Helpers;
using ASTE.Modules.APIDiscovery.Models;
using ASTE.Resources.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ASTE.Modules.APIDiscovery.Controllers
{
    [RoutePrefix("ConfigApi/1.0")]
    public class ConfigApiController : ApiController
    {
        private APIDiscoveryContext ctx = new APIDiscoveryContext();

        /// <summary>
        /// Returns Module information from an active module
        /// </summary>
        /// <param name="module">Name of the module</param>
        /// <param name="method">Name of the method</param>
        /// <param name="version">Version of the module</param>
        /// <param name="source">Where the call is coming from</param>
        /// <returns></returns>
        [Route("getActiveModules")]
        public async Task<ModuleInfo> GetActiveModules (string module, string method, string version, string source)
        {
            RestHelper rs = new RestHelper();
            Helper helper = new Helper();
            var api_key = helper.GetApiKey(Request);
   
            await rs.Log("Incoming getActiveModule call from: " + source + " at " + helper.GetClientIp(Request));

            if(string.IsNullOrEmpty(module))
            {
                await rs.Log("GetActiveModule failed: Missing module name from " + source + " at " + helper.GetClientIp(Request) );
                HttpResponseMessage no_name_response = this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, string.Format("module name cannot be empty"));
                throw new HttpResponseException(no_name_response);
            }
            if (string.IsNullOrEmpty(method))
            {
                await rs.Log("GetActiveModule failed: Missing module method from " + source + " at " + helper.GetClientIp(Request));
                HttpResponseMessage no_method_response = this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, string.Format("module method cannot be empty"));
                throw new HttpResponseException(no_method_response);
            }
            if (string.IsNullOrEmpty(version))
            {
                await rs.Log("GetActiveModule failed: Missing module version from " + source + " at " + helper.GetClientIp(Request));
                HttpResponseMessage no_version_response = this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, string.Format("module version cannot be empty"));
                throw new HttpResponseException(no_version_response);
            }
            if (string.IsNullOrEmpty(source))
            {
                await rs.Log("GetActiveModule failed: Missing module source at " + helper.GetClientIp(Request));
                HttpResponseMessage no_source_response = this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, string.Format("module source cannot be empty"));
                throw new HttpResponseException(no_source_response);
            }

            if (!string.IsNullOrEmpty(api_key))
            {
             
                var exists = ctx.clients.Where(x => x.api_key == api_key && !x.isdeleted).FirstOrDefault();
                if (exists != null)
                {

                    var activeModules = ctx.modules.Where(x => !x.isdeleted && x.active && !x.isProcess && x.name == module).ToList();
                    if(activeModules == null)
                    {
                        var module_not_found_message = string.Format("Module {0} not found from: " + source + " at " + helper.GetClientIp(Request), module);
                        await rs.Log(module_not_found_message);
                        HttpResponseMessage response = this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, string.Format("Module {0} not found",module));
                        throw new HttpResponseException(response);
                    }
                    var VersionFound = false;
                    var MethodFound = false;
                    foreach(var m in activeModules)
                    {
                        if(m.version == version)
                        {
                            VersionFound = true;
                            foreach (var moduleMethod in m.methods)
                            {
                                if(moduleMethod.name == method)
                                {
                                    MethodFound = true;
                                    ModuleInfo info = new ModuleInfo()
                                    {
                                        api_url = m.api_url,
                                        name = m.name,
                                        version = m.version
                                    };
                                    return info;
                                }
                            }
                        }
                    }

                    if(!VersionFound)
                    {
                        var version_not_found_message = string.Format("Version {0} is not available for module {1} from: " + source + " at " + helper.GetClientIp(Request),version, module);
                        await rs.Log(version_not_found_message);
                        HttpResponseMessage version_response = this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, string.Format("Version {0} is not available for module {1}.",version,module));
                        throw new HttpResponseException(version_response);
                    }
                    if(!MethodFound)
                    {
                        var method_not_found_message = string.Format("method {0} is not available for module {1} version {2} from: " + source + " at " + helper.GetClientIp(Request), method, module,version);
                        await rs.Log(method_not_found_message);
                        HttpResponseMessage method_response = this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, string.Format("method {0} is not available for module {1} version {2}.", method, module, version));
                        throw new HttpResponseException(method_response);
                    }

                    var message = string.Format("Version {0} is not available for module {1} from: " + source + " at " + helper.GetClientIp(Request), version, module);
                    await rs.Log(message);
                    HttpResponseMessage error_response = this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Error while fetching module information");
                    throw new HttpResponseException(error_response);

                }
                else
                {
                    var message = string.Format("Invalid API key from: " + source + " at " + helper.GetClientIp(Request));
                    await rs.Log(message);
                    HttpResponseMessage response = this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid API key.");
                    throw new HttpResponseException(response);
                }
            }
            else
            {
                var message = string.Format("API key missing from: " + source + " at " + helper.GetClientIp(Request));
                await rs.Log(message);
                HttpResponseMessage response = this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "API key missing.");
                throw new HttpResponseException(response);
            }

        }

        /// <summary>
        /// Returns process information from an active process
        /// </summary>
        /// <param name="process">Name of the process</param>
        /// <param name="method">Name of the method</param>
        /// <param name="version">Version of the process</param>
        /// <param name="source">Where the call is coming from</param>
        /// <returns></returns>
        [Route("getActiveProcesses")]
        public async Task<ModuleInfo> getActiveProcess(string process, string method, string version, string source)
        {
            RestHelper rs = new RestHelper();
            Helper helper = new Helper();
            var api_key = helper.GetApiKey(Request);

            await rs.Log("Incoming getActiveProcess call from: " + source);

            if (string.IsNullOrEmpty(process))
            {
                await rs.Log("getActiveProcess failed: Missing process name from " + source + " at " + helper.GetClientIp(Request));
                HttpResponseMessage no_name_response = this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, string.Format("Process name cannot be empty"));
                throw new HttpResponseException(no_name_response);
            }
            if (string.IsNullOrEmpty(method))
            {
                await rs.Log("getActiveProcess failed: Missing process method from " + source + " at " + helper.GetClientIp(Request));
                HttpResponseMessage no_method_response = this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, string.Format("Process method cannot be empty"));
                throw new HttpResponseException(no_method_response);
            }
            if (string.IsNullOrEmpty(version))
            {
                await rs.Log("getActiveProcess failed: Missing process version from " + source + " at " + helper.GetClientIp(Request));
                HttpResponseMessage no_version_response = this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, string.Format("Process version cannot be empty"));
                throw new HttpResponseException(no_version_response);
            }
            if (string.IsNullOrEmpty(source))
            {
                await rs.Log("getActiveProcess failed: Missing process source at " + helper.GetClientIp(Request));
                HttpResponseMessage no_source_response = this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, string.Format("Process source cannot be empty"));
                throw new HttpResponseException(no_source_response);
            }

            if (!string.IsNullOrEmpty(api_key))
            {

                var exists = ctx.clients.Where(x => x.api_key == api_key && !x.isdeleted).FirstOrDefault();
                if (exists != null)
                {

                    var activeModules = ctx.modules.Where(x => !x.isdeleted && x.active && x.isProcess && x.name == process).ToList();
                    if (activeModules == null)
                    {
                        var process_not_found_message = string.Format("Process {0} not found from: " + source + " at " + helper.GetClientIp(Request), process);
                        await rs.Log(process_not_found_message);
                        HttpResponseMessage response = this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, string.Format("Process {0} not found.", process));
                        throw new HttpResponseException(response);
                    }
                    var VersionFound = false;
                    var MethodFound = false;
                    foreach (var m in activeModules)
                    {
                        if (m.version == version)
                        {
                            VersionFound = true;
                            foreach (var moduleMethod in m.methods)
                            {
                                if (moduleMethod.name == method)
                                {
                                    MethodFound = true;
                                    ModuleInfo info = new ModuleInfo()
                                    {
                                        api_url = m.api_url,
                                        name = m.name,
                                        version = m.version
                                    };
                                    return info;
                                }
                            }
                        }
                    }


                    if (!VersionFound)
                    {
                        var version_not_found_message = string.Format("Version {0} is not available for process {1} from: " + source + " at " + helper.GetClientIp(Request), version, process);
                        await rs.Log(version_not_found_message);
                        HttpResponseMessage version_response = this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, string.Format("Version {0} is not available for process {1}.", version, process));
                        throw new HttpResponseException(version_response);
                    }
                    if (!MethodFound)
                    {
                        var method_not_found_message = string.Format("method {0} is not available for process {1} version {2} from: " + source +  " at" + helper.GetClientIp(Request), method, process, version);
                        await rs.Log(method_not_found_message);
                        HttpResponseMessage method_response = this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, string.Format("method {0} is not available for process {1} version {2}.", method, process, version));
                        throw new HttpResponseException(method_response);
                    }

                    var message = string.Format("Version {0} is not available for process {1} from: " + source + " at " + helper.GetClientIp(Request), version, process);
                    await rs.Log(message);
                    HttpResponseMessage error_response = this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Error while fetching process information");
                    throw new HttpResponseException(error_response);

                }
                else
                {
                    var message = string.Format("Invalid API key from: " + source + " at " + helper.GetClientIp(Request));
                    await rs.Log(message);
                    HttpResponseMessage response = this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid API key.");
                    throw new HttpResponseException(response);
                }
            }
            else
            {
                var message = string.Format("API key missing from: " + source + " at " + helper.GetClientIp(Request));
                await rs.Log(message);
                HttpResponseMessage response = this.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "API key missing.");
                throw new HttpResponseException(response);
            }

        }
    }
}