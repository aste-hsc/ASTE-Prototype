using ASTE.Modules.FormModule.db.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace ASTE.Modules.FormModule.db.Mappings
{
    public class FormMap : EntityTypeConfiguration<Form>
    {
        public FormMap()
        {
            this.ToTable("FORM");

            this.HasKey(x => x.id)
                .Property(x => x.id).HasColumnName("form_id")
                .HasColumnOrder(1)
                .IsRequired();

            this.HasRequired(x => x.form_metadata)
                .WithMany(x => x.forms)
                .HasForeignKey(x => x.form_metadata_id);
        }
    }
}