using ASTE.Modules.APIDiscovery.db.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace ASTE.Modules.APIDiscovery.db.Context
{
    /// <summary>
    /// DB Context for the API Discovery
    /// </summary>
    public class APIDiscoveryContext : DbContext
    {
        public APIDiscoveryContext(string connectionstring) : base(connectionstring){ }
        public APIDiscoveryContext() {

            this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            this.Database.Initialize(true);
        }

        public virtual DbSet<Client> clients { get; set; }
        public virtual DbSet<Module> modules { get; set; }
        public virtual DbSet<ModuleMethod> methods { get; set; }
        public virtual DbSet<ModuleDependency> dependencies { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            
            modelBuilder.Configurations.Add(new Mappings.ClientMap());
            modelBuilder.Configurations.Add(new Mappings.ModuleMap());
            modelBuilder.Configurations.Add(new Mappings.ModuleDependencyMap());
            modelBuilder.Configurations.Add(new Mappings.ModuleMethodMap());
        }
    }

    /// <summary>
    /// API Discovery Seed
    /// </summary>
    public class APIDiscoveryInitializer : CreateDatabaseIfNotExists<APIDiscoveryContext>
    {

        protected override void Seed(APIDiscoveryContext context) 
        {
            //Adds the API Discovery Module after DB is created
            var module = new Module();
            module.name = "API Discovery Module";
            module.created = DateTime.Now;
            module.modified = DateTime.Now;
            module.api_url = "/ConfigApi";
            module.version = "1.0";
            module.description = "ASTE Configurations, Modules and clients";
            module.author = "Codecontrol Oy";
            module.guid = Resources.Constants.ASTE_MODULES_API_DISCOVERY_GUID;
            module.authorContact = "http://www.codecontrol.fi";
            module.isProcess = false;
            module.active = true;
            module.methods = new List<ModuleMethod>();
            module.methods.Add(new ModuleMethod()
            {
                created = DateTime.Now,
                modified = DateTime.Now,
                name = "GetActiveModules",
            });
            context.modules.Add(module);
            context.SaveChanges();
        }
    }

}