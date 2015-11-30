using ASTE.Demos.Mielenterveysseura.db.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace ASTE.Demos.Mielenterveysseura.db.Mappings
{
    public class FormQuestionMetadataMap : EntityTypeConfiguration<FormQuestionMetadata>
    {
        public FormQuestionMetadataMap()
        {
            this.ToTable("FORM_QUESTION_METADATA");

            this.HasKey(x => x.id)
                .Property(x => x.id).HasColumnName("form_question_metadata_id")
                .HasColumnOrder(1)
                .IsRequired();

            this.Property(x => x.name)
                .HasColumnName("name")
                .HasColumnOrder(2);

            this.Property(x => x.question)
                .HasColumnName("Question")
                .HasColumnOrder(3);

            this.Property(x => x.ordernumber)
                .IsRequired()
                .HasColumnName("ordernumber")
                .HasColumnOrder(4);

            this.HasRequired(x => x.form_metadata)
                .WithMany(x => x.questions)
                .HasForeignKey(x => x.form_metadata_id)
                .WillCascadeOnDelete(false);


        }
    }
}