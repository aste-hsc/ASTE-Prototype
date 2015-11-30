using ASTE.Modules.APIDiscovery.db.Context;
using ASTE.Modules.APIDiscovery.db.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASTE.Modules.APIDiscovery.Controllers
{
    /// <summary>
    /// Controller for handling API DIscovery Clients
    /// </summary>
    [RoutePrefix("Client")]
    public class ClientController : Controller
    {
        private APIDiscoveryContext ctx = new APIDiscoveryContext();

        /// <summary>
        /// Client Index
        /// </summary>
        /// <returns>Index view of clients</returns>
        public ActionResult Index()
        {
            var clients = ctx.clients.ToList();
            return View(clients);
        }

        /// <summary>
        /// Displays the New Client -View
        /// </summary>
        /// <returns>New Client View</returns>
        [Route("New")]
        public ActionResult New()
        {
            return View(new Client());
        }

        /// <summary>
        /// Adds a new client to the API Discovery
        /// </summary>
        /// <param name="client">Client Data</param>
        /// <returns>Client index -view if saved, New Client view if not saved</returns>
        [Route("Add")]
        [HttpPost]
        public ActionResult Add(Client client)
        {
            if (ModelState.IsValid)
            {
                client.created = DateTime.Now;
                client.modified = DateTime.Now;
                client.api_key = Guid.NewGuid().ToString().ToLower();
                ctx.clients.Add(client);
                ctx.SaveChanges();

                return RedirectToAction("Index", "Client");
            }
            return View("New", client);
        }

        /// <summary>
        /// Deletes an Client from the API Discovery
        /// </summary>
        /// <param name="client_id">id of the Client to delete</param>
        /// <returns>Index view of Clients</returns>
        [Route("Delete/{client_id}")]
        public ActionResult Delete(int client_id)
        {
            var client = ctx.clients.Where(x => x.id == client_id).FirstOrDefault();
            if (client != null)
            {
                client.isdeleted = true;
                ctx.SaveChanges();
                return RedirectToAction("Index", "Client");
            }

            throw new ArgumentException("Client not found!");
        }

        /// <summary>
        /// Displays the Edit Client -View
        /// </summary>
        /// <param name="client_id">Id of the Client</param>
        /// <returns>Edit view of the Client</returns>
        [Route("Edit/{client_id}")]
        public ActionResult Edit(int client_id)
        {
            var client = ctx.clients.Where(x => x.id == client_id).FirstOrDefault();
            if (client != null)
            {
                return View(client);
            }

            throw new ArgumentException("Client not found!");
        }

        /// <summary>
        /// Updates the selected client
        /// </summary>
        /// <param name="client">Client data</param>
        /// <returns>Client Index view if saved, Edit Client view if not saved</returns>
        [Route("Update")]
        [HttpPost]
        public ActionResult Update(Client client)
        {
            if (ModelState.IsValid)
            {
                var _client = ctx.clients.Where(x => x.id == client.id).FirstOrDefault();
                _client.name = client.name;
                _client.modified = DateTime.Now;
                _client.api_key = client.api_key;
                _client.client_name = client.client_name;
                _client.client_ip = client.client_ip;

                ctx.SaveChanges();
                return RedirectToAction("Index", "Client");
            }

            return View("Edit", client);
        }
    }
}