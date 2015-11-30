using ASTE.Modules.FormModule.db.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace ASTE.Modules.FormModule.db.Mappings
{
    public class FormMetadataMap : EntityTypeConfiguration<FormMetadata>
    {
        public FormMetadataMap()
        {
            this.ToTable("FORM_METADATA");

            this.HasKey(x => x.id)
                .Property(x => x.id).HasColumnName("form_metadata_id")
                .HasColumnOrder(1)
                .IsRequired();

            this.Property(x => x.name)
                .HasColumnName("name")
                .HasColumnOrder(2);

        }
    }
}