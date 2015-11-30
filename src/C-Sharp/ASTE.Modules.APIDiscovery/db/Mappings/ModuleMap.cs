using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using ASTE.Modules.APIDiscovery.db.Entities;
using System.Linq;
using System.Web;

namespace ASTE.Modules.APIDiscovery.db.Mappings
{
    /// <summary>
    /// DB Mappings for module
    /// </summary>
    public class ModuleMap : EntityTypeConfiguration<Module>
    {
        public ModuleMap()
        {
            this.ToTable("MODULE");

            this.HasKey(x => x.id)
                .Property(x => x.id).HasColumnName("module_id")
                .HasColumnOrder(1)
                .IsRequired();



            this.Property(x => x.created)
                .HasColumnName("created_at")
                .HasColumnOrder(2)
                .IsRequired();

            this.Property(x => x.modified)
                .HasColumnName("modified_at")
                .HasColumnOrder(3);

            this.Property(x => x.name)
                .HasColumnName("name")
                .HasColumnOrder(4)
                .IsRequired();

            this.Property(x => x.author)
                .HasColumnName("author")
                .HasColumnOrder(5);

            this.Property(x => x.authorContact)
                .HasColumnName("authorContact")
                .HasColumnOrder(6);

            this.Property(x => x.description)
                .HasColumnName("description")
                .HasColumnOrder(7);

            this.Property(x => x.api_url)
                .HasColumnName("api_url")
                .HasColumnOrder(8)
                .IsRequired();

            this.Property(x => x.version)
                .HasColumnName("version")
                .HasColumnOrder(9)
                .IsRequired();

            this.Property(x => x.active)
                .HasColumnName("is_active")
                .HasColumnOrder(10);

            this.Property(x => x.isProcess)
                 .HasColumnName("is_process")
                 .HasColumnOrder(11);

            this.Property(x => x.isdeleted)
                .HasColumnName("is_deleted")
                .HasColumnOrder(12);

            this.Property(x => x.guid)
                .HasColumnName("Guid")
                .HasColumnOrder(13);
        }
    }
}