using ASTE.Modules.FormModule.db.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace ASTE.Modules.FormModule.db.Mappings
{
    public class FormAnswerMap : EntityTypeConfiguration<FormAnswer>
    {
        public FormAnswerMap()
        {
            this.ToTable("FORM_ANSWER");

            this.HasKey(x => x.id)
                .Property(x => x.id).HasColumnName("form_answer_id")
                .HasColumnOrder(1)
                .IsRequired();

            this.Property(x => x.value)
                .HasColumnName("value")
                .HasColumnOrder(2);

            this.HasRequired(x => x.form_question_metadata)
                .WithMany(x => x.answers)
                .HasForeignKey(x => x.form_question_metadata_id);

            this.HasRequired(x => x.form)
                .WithMany(x => x.answers)
                .HasForeignKey(x => x.form_id);

        }
    }
}