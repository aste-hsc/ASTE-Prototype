using ASTE.Demos.Mielenterveysseura.db.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace ASTE.Demos.Mielenterveysseura.db.Context
{
    public class AsteContext : DbContext
    {
        public AsteContext(string connectionstring) : base(connectionstring){ }
        public AsteContext() {

            this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
            this.Database.Initialize(true);
        }

        
        public virtual DbSet<FormMetadata> form_metadatas { get; set; }
        public virtual DbSet<FormQuestionMetadata> form_questions { get; set; }
        public virtual DbSet<Form> forms { get; set; }
        public virtual DbSet<FormAnswer> form_answers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            modelBuilder.Configurations.Add(new Mappings.FormMetadataMap());
            modelBuilder.Configurations.Add(new Mappings.FormQuestionMetadataMap());
            modelBuilder.Configurations.Add(new Mappings.FormMap());
            modelBuilder.Configurations.Add(new Mappings.FormAnswerMap());
        }
    }

    public class AsteInitializer : CreateDatabaseIfNotExists<AsteContext>
    {

        protected override void Seed(AsteContext context) 
        {

            FormMetadata LapsetPuheeksi = new FormMetadata();
            LapsetPuheeksi.name = "Lapset puheeksi";
            context.form_metadatas.Add(LapsetPuheeksi);


            FormQuestionMetadata q1 = new FormQuestionMetadata()
            {
                name = "Q1",
                ordernumber = 1,
                question = "Miten kuvailisitte perhettänne ja perhesuhteitanne?"
            };

            FormQuestionMetadata q2 = new FormQuestionMetadata()
            {
                name = "Q2",
                ordernumber = 2,
                question = "Miten kuvailisitte lasta?"
            };

            FormQuestionMetadata q3 = new FormQuestionMetadata()
            {
                name = "Q3",
                ordernumber = 3,
                question = "Miten kuvailisit varhaiskasvatusympäristöä?"
            };

            FormQuestionMetadata q4 = new FormQuestionMetadata()
            {
                name = "Q4",
                ordernumber = 4,
                question = "Minkälainen lapsi on mielialaltaan?"
            };

            FormQuestionMetadata q5 = new FormQuestionMetadata()
            {
                name = "Q5",
                ordernumber = 5,
                question = "Minkälainen lapsi on toiminnaltaan ja käyttäytymiseltään?"
            };

            FormQuestionMetadata q6 = new FormQuestionMetadata()
            {
                name = "Q6",
                ordernumber = 6,
                question = "Miten sujuu kanssakäyminen ja leikki muiden lasten kanssa?"
            };


            FormQuestionMetadata q7 = new FormQuestionMetadata()
            {
                name = "Q7",
                ordernumber = 7,
                question = "Miten arkiset toimet sujuvat?"
            };

            FormQuestionMetadata q8 = new FormQuestionMetadata()
            {
                name = "Q8",
                ordernumber = 8,
                question = "Missä tilanteissa kotona ja varhaiskasvatuksen piirissä tulee ristiriitoja?"
            };

            FormQuestionMetadata q9 = new FormQuestionMetadata()
            {
                name = "Q9",
                ordernumber = 9,
                question = "Onko lapsesta ollut huolta? Onko haettu ja saatu apua?"
            };

            FormQuestionMetadata q10 = new FormQuestionMetadata()
            {
                name = "Q10",
                ordernumber = 10,
                question = "Onko perheessä huolia tai paineita, jotka voivat heijastuvat lapseen?"
            };

            FormQuestionMetadata q11 = new FormQuestionMetadata()
            {
                name = "Q11",
                ordernumber = 11,
                question = "Ovatko perheen huolet tai paineet näkyneet tavalla tai toisella lapsessa?"
            };

            FormQuestionMetadata q12 = new FormQuestionMetadata()
            {
                name = "Q12",
                ordernumber = 12,
                question = "12. Onko lapsella sosiaalista tukiverkostoa, entä huoltajilla?"
            };

            FormQuestionMetadata q13 = new FormQuestionMetadata()
            {
                name = "Q13",
                ordernumber = 13,
                question = "Onko lapsella varhaiskasvatuksen piirissä aikuinen, joka on hänelle läheinen ja jonka puoleen hän voi halutessaan kääntyä?"
            };

            FormQuestionMetadata q14 = new FormQuestionMetadata()
            {
                name = "Q14",
                ordernumber = 14,
                question = "Onko varhaiskasvatuksen piirissä joitakin sellaisia asioita, jotka heijastuvat lapseen esim. levottomuutena tai alakulona?"
            };

            FormQuestionMetadata q15 = new FormQuestionMetadata()
            {
                name = "Q15",
                ordernumber = 15,
                question = "Miten helppoa tai vaikeaa teidän on keskustella kotiin, lapseen ja varhaiskasvatukseen liittyvistä haasteista ja ongelmista?"
            };

            FormQuestionMetadata q16 = new FormQuestionMetadata()
            {
                name = "Q16",
                ordernumber = 16,
                question = "Kodin ja päivähoidon kasvatuspäämäärät:"
            };

            FormQuestionMetadata q17 = new FormQuestionMetadata()
            {
                name = "Q17",
                ordernumber = 17,
                question = "Miten koette kodin ja päivähoidon yhteistyön ja vuorovaikutuksen?"
            };

            FormQuestionMetadata q18 = new FormQuestionMetadata()
            {
                name = "Q18",
                ordernumber = 18,
                question = "Toteutetaanko Lp -keskustelun toinen osa?"
            };

            FormQuestionMetadata q19 = new FormQuestionMetadata()
            {
                name = "Q19",
                ordernumber = 19,
                question = "Toimintasuunnitelman laatiminen"
            };

            FormQuestionMetadata q20 = new FormQuestionMetadata()
            {
                name = "Q20",
                ordernumber = 20,
                question = "Miltä tämä keskustelu tuntui?"
            };

            LapsetPuheeksi.questions = new List<FormQuestionMetadata>();

            LapsetPuheeksi.questions.Add(q1);
            LapsetPuheeksi.questions.Add(q2);
            LapsetPuheeksi.questions.Add(q3);
            LapsetPuheeksi.questions.Add(q4);
            LapsetPuheeksi.questions.Add(q5);
            LapsetPuheeksi.questions.Add(q6);
            LapsetPuheeksi.questions.Add(q7);
            LapsetPuheeksi.questions.Add(q8);
            LapsetPuheeksi.questions.Add(q9);
            LapsetPuheeksi.questions.Add(q10);
            LapsetPuheeksi.questions.Add(q11);
            LapsetPuheeksi.questions.Add(q12);
            LapsetPuheeksi.questions.Add(q13);
            LapsetPuheeksi.questions.Add(q14);
            LapsetPuheeksi.questions.Add(q15);
            LapsetPuheeksi.questions.Add(q16);
            LapsetPuheeksi.questions.Add(q17);
            LapsetPuheeksi.questions.Add(q18);
            LapsetPuheeksi.questions.Add(q19);
            LapsetPuheeksi.questions.Add(q20);
            
            context.SaveChanges();


        }
    }

}