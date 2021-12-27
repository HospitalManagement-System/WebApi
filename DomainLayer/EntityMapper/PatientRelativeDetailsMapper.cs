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
    public class PatientRelativeDetailsMapper : IEntityTypeConfiguration<PatientRelativeDetails>
    {
        public void Configure(EntityTypeBuilder<PatientRelativeDetails> builder)
        {
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

            builder.Property(x => x.Email)
              .HasColumnName("Email")
              .HasColumnType("VARCHAR(100)");

            builder.Property(x => x.Address)
              .HasColumnName("Address")
              .HasColumnType("VARCHAR(100)");

            builder.Property(x => x.Relation)
              .HasColumnName("Relation")
              .HasColumnType("VARCHAR(50)");

            builder.Property(x => x.Contact)
              .HasColumnName("Contact")
              .HasColumnType("BIGINT");

        }
    }
}
