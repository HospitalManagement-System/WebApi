using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.EntityMapper
{
    public class PatientVisitDetailsMapper : IEntityTypeConfiguration<PatientVisitDetails>
    {
        public void Configure(EntityTypeBuilder<PatientVisitDetails> builder)
        {
            builder.HasKey(x => x.Id).HasName("pk_patientvisitdetailsid");
            builder.Property(x => x.Id).ValueGeneratedOnAdd()
                .HasColumnName("Id")
                .HasColumnType("UniqueIdentifier");

            builder.Property(x => x.Height)
                .HasColumnName("Height")
                .HasColumnType("DECIMAL");

            builder.Property(x => x.Weight)
                .HasColumnName("Weight")
                .HasColumnType("DECIMAL");

            builder.Property(x => x.BloodPressure)
              .HasColumnName("BloodPressure")
              .HasColumnType("INT");

            builder.Property(x => x.BodyTemprature)
              .HasColumnName("BodyTemprature")
              .HasColumnType("INT");

            builder.Property(x => x.RespirationRate)
              .HasColumnName("RespirationRate")
              .HasColumnType("INT");

            builder.Property(x => x.DoctorDescription)
            .HasColumnName("DoctorDescription")
            .HasColumnType("VARCHAR(150)");

            builder.Property(x => x.ProcedureDesciption)
            .HasColumnName("ProcedureDesciption")
            .HasColumnType("VARCHAR(150)");

            builder.Property(x => x.Medication)
            .HasColumnName("Medication")
            .HasColumnType("VARCHAR(150)");

            builder.Property(x => x.Dosage)
            .HasColumnName("Dosage")
            .HasColumnType("VARCHAR(150)");

            builder.Property(x => x.AppointmentId)
           .HasColumnName("AppointmentId")
           .HasColumnType("UniqueIdentifier");

            builder.Property(x => x.DiagnosisId)
           .HasColumnName("DiagnosisId")
           .HasColumnType("UniqueIdentifier");

            builder.Property(x => x.ProcedureId)
           .HasColumnName("ProcedureId")
           .HasColumnType("UniqueIdentifier");

        }
    }
}
