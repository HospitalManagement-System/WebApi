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
    public class PatientDetailsMapper : IEntityTypeConfiguration<PatientDetails>
    {
        public void Configure(EntityTypeBuilder<PatientDetails> builder)
        {
            builder.HasKey(x => x.Id).HasName("pk_patientdetailsid");
            builder.Property(x => x.Id).ValueGeneratedOnAdd()
                .HasColumnName("Id")
                .HasColumnType("UniqueIdentifier");

            builder.Property(x => x.Title)
                .HasColumnName("Title")
                .HasColumnType("VARCHAR(15)");

            builder.Property(x => x.FirstName)
                .HasColumnName("FirstName")
                .HasColumnType("VARCHAR(50)");

            builder.Property(x => x.LastName)
                .HasColumnName("LastName")
                .HasColumnType("VARCHAR(50)");

            builder.Property(x => x.Contact)
                .HasColumnName("Contact")
                .HasColumnType("BIGINT");

            builder.Property(x => x.PatientDemographicId)
                   .HasColumnName("PatientDemographicId")
                   .HasColumnType("UniqueIdentifier");

            builder.Property(x => x.UserId)
               .HasColumnName("UserId")
               .HasColumnType("UniqueIdentifier");

            builder.Property(x => x.IsActive)
              .HasColumnName("IsActive")
              .HasColumnType("CHAR");
        }
    }
}
