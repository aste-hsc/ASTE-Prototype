using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using ASTE.Modules.APIDiscovery.db.Entities;
using System.Linq;
using System.Web;

namespace ASTE.Modules.APIDiscovery.db.Mappings
{
    /// <summary>
    /// DB Mappings for module dependencies
    /// </summary>
    public class ModuleDependencyMap : EntityTypeConfiguration<ModuleDependency>
    {
        public ModuleDependencyMap()
        {
            this.ToTable("MODULE_DEPENDENCY");

            this.HasKey(x => x.id)
                .Property(x => x.id).HasColumnName("module_dependency_id")
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

            this.Property(x => x.isdeleted)
                .HasColumnName("is_deleted")
                .HasColumnOrder(5);

            this.HasRequired(x => x.module)
                .WithMany(x => x.my_dependencies)
                .HasForeignKey(x => x.module_id);

            this.HasRequired(x => x.dependency)
                .WithMany(x => x.dependent_on_me)
                .HasForeignKey(x => x.dependency_id);
        }
    }
}