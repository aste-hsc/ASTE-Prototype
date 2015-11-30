using ASTE.Modules.APIDiscovery.db.Context;
using ASTE.Modules.APIDiscovery.db.Entities;
using ASTE.Modules.APIDiscovery.Helpers;
using ASTE.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ASTE.Modules.APIDiscovery.Controllers
{
    /// <summary>
    /// Controller for the Module views
    /// </summary>
    [RoutePrefix("Module")]
    public class ModuleController : Controller
    {
        private APIDiscoveryContext ctx = new APIDiscoveryContext();

        /// <summary>
        /// Index view of the modules
        /// </summary>
        /// <returns>Module index -view</returns>
        public ActionResult Index()
        {
            var modules = ctx.modules.Where(x => !x.isdeleted && !x.isProcess).ToList();
            return View(modules);
        }

        /// <summary>
        /// Displays the New Module -view
        /// </summary>
        /// <returns>New Module -view</returns>
        [Route("New")]
        public ActionResult New()
        {
            return View(new Models.newModelOrProcess());
        }

        /// <summary>
        /// Adds a new module to API Discovery
        /// </summary>
        /// <param name="url">Base URL of the Modules REST API</param>
        /// <param name="version">Version of the module</param>
        /// <returns>Module Index view if saved, New Module view if not saved</returns>
        [Route("Add")]
        [HttpGet]
        public async Task<ActionResult> Add(string url, string version)
        {
            var ctx = new APIDiscoveryContext();
            if (ModelState.IsValid)
            {
                RestHelper helper = new RestHelper();
                try
                {
                    var status = await helper.Ping(url, version);
                    if(status != null && status.status.ToLower() == Constants.ASTE_PINGSTATUS_SUCCESS)
                    {
                        try
                        {
                            var config = await helper.Config(url,version);
                            if(config != null)
                            {
                                if (config.type.ToLower() == Constants.ASTE_MODULETYPE_MODULE)
                                {
                                    Module new_module = new Module();
                                    new_module.name = config.name;
                                    new_module.created = DateTime.Now;
                                    new_module.guid = config.guid;
                                    new_module.description = config.description;
                                    new_module.author = config.author;
                                    new_module.authorContact = config.authorContact;
                                    new_module.isProcess = false;
                                    new_module.methods = new List<ModuleMethod>();
                                    new_module.my_dependencies = new List<ModuleDependency>();
                                    new_module.api_url = url;
                                    new_module.version = version;
                                    if(config.methods != null)
                                    {
                                        foreach(var m in config.methods)
                                        {
                                            new_module.methods.Add(new ModuleMethod()
                                            {
                                                created = new DateTime(),
                                                name = m.method,
                                                methodInfo = m.@params 
                                            });
                                        }
                                    }
                                    var dependency_not_found = false;
                                    foreach(var d in config.dependencies)
                                    {
                                        var db_module = ctx.modules.Where(x => x.guid == d).FirstOrDefault();
                                        if(db_module == null)
                                        {
                                            ModelState.AddModelError("Config", string.Format("Module dependency {0} was not found, could not install API to discovery",d));
                                            return View("New", new Models.newModelOrProcess() { url = url, version = version });
                                        }
                                        else
                                        {
                                            new_module.my_dependencies.Add(new ModuleDependency()
                                            {
                                                created = DateTime.Now,
                                                dependency_id = db_module.id,
                                                name = db_module.name

                                            });
                                        }
                                    }
                                    if(!dependency_not_found)
                                    {
                                        ctx.modules.Add(new_module);
                                        ctx.SaveChanges();
                                    }

                                }
                                else
                                {
                                    ModelState.AddModelError("Config", "Contacted api is a process, not a module");
                                    return View("New", new Models.newModelOrProcess() { url = url, version = version });
                                }
                               
                            }
                            else
                            {
                                ModelState.AddModelError("Config", "config did not return any data");
                                return View("New", new Models.newModelOrProcess() { url = url, version = version });
                            }
                        }
                        catch(Exception ex)
                        {
                            ModelState.AddModelError("Config", "Url of the API could not be resolved, or the service is not listening - config method");
                            return View("New", new Models.newModelOrProcess() { url = url, version = version });
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Ping", status.message);
                        return View("New", new Models.newModelOrProcess() { url = url, version = version });
                    }
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("Ping", "Url of the API could not be resolved, or the service is not listening - ping method");
                    return View("New", new Models.newModelOrProcess() { url = url, version = version });
                }
               
                return RedirectToAction("Index", "Module");
            }

            return View("New", new Models.newModelOrProcess() { url = url, version = version });
        }

        /// <summary>
        /// Deletes the given module
        /// </summary>
        /// <param name="module_id">id of the module to delete</param>
        /// <returns>Index view of the modules</returns>
        [Route("Delete/{module_id}")]
        public ActionResult Delete(int module_id)
        {
            var module = ctx.modules.Where(x => x.id == module_id).FirstOrDefault();
            if (module != null)
            {
                if(module.dependent_on_me != null && module.dependent_on_me.Count > 0)
                {
                    TempData.Add("error", "Cannot delete module, module has one or more dependencies");
                    return RedirectToAction("Index", "Module");
                }
                module.isdeleted = true;
                if(module.my_dependencies != null && module.my_dependencies.Count > 0)
                {
                   foreach(var d in module.my_dependencies)
                    {
                        d.isdeleted = true;
                    }
                }
                ctx.SaveChanges();
                return RedirectToAction("Index", "Module");
            }

            throw new ArgumentException("Module not found!");
        }

        /// <summary>
        /// Displays the Edit Module -view
        /// </summary>
        /// <param name="module_id">id of the module to edit</param>
        /// <returns>Edit Module -view</returns>
        [Route("Edit/{module_id}")]
        public ActionResult Edit(int module_id)
        {
            var module = ctx.modules.Where(x => x.id == module_id).FirstOrDefault();
            if (module != null)
            {
                return View(module);
            }

            throw new ArgumentException("Module not found!");
        }

        /// <summary>
        /// Updates the given module
        /// </summary>
        /// <param name="module">module data</param>
        /// <returns>Module Index -view if saved, Edit Module view if not saved</returns>
        [Route("Update")]
        [HttpPost]
        public ActionResult Update(Module module)
        {
            if (ModelState.IsValid)
            {
                var _module = ctx.modules.Where(x => x.id == module.id).FirstOrDefault();

                if (_module.dependent_on_me != null && _module.dependent_on_me.Count > 0 && !module.active)
                {
                    TempData.Add("error", "Cannot deactivate module, module has one or more dependencies");
                     return View("Edit",module);
                }

                _module.active = module.active;
                _module.api_url = module.api_url;
                _module.description = module.description;
                _module.author = module.author;
                _module.authorContact = module.authorContact;
                _module.name = module.name;

                _module.modified = DateTime.Now;

                ctx.SaveChanges();
                return RedirectToAction("Index", "Module");
            }

            return View("Edit", module);
        }
    }
}