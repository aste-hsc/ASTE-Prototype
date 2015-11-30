using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using ASTE.Modules.APIDiscovery.db.Entities;
using System.Linq;
using System.Web;

namespace ASTE.Modules.APIDiscovery.db.Mappings
{
    /// <summary>
    /// DB Mappings for Client
    /// </summary>
    public class ClientMap : EntityTypeConfiguration<Client>
    {
        public ClientMap()
        {
            this.ToTable("CLIENT");

            this.HasKey(x => x.id)
                .Property(x => x.id).HasColumnName("client_id")
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

            this.Property(x => x.api_key)
                .HasColumnName("api_key")
                .HasColumnOrder(5)
                .IsRequired();

            this.Property(x => x.client_ip)
                .HasColumnName("client_ip")
                .HasColumnOrder(6)
                .IsRequired();

            this.Property(x => x.client_name)
                .HasColumnName("client_name")
                .HasColumnOrder(7)
                .IsRequired();

            this.Property(x => x.isdeleted)
                .HasColumnName("is_deleted")
                .HasColumnOrder(8);
        }
    }
}