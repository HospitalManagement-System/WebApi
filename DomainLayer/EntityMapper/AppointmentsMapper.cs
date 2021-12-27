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
    public class AppointmentsMapper : IEntityTypeConfiguration<Appointments>
    {
        public void Configure(EntityTypeBuilder<Appointments> builder)
        {
            builder.HasKey(x => x.Id).HasName("pk_appointmentid");
            builder.Property(x => x.Id).ValueGeneratedOnAdd()
                .HasColumnName("Id")
                .HasColumnType("UniqueIdentifier");

            builder.Property(x => x.AppointmentType)
                .HasColumnName("AppointmentType")
                .HasColumnType("CHAR");

            builder.Property(x => x.Diagnosis)
                .HasColumnName("Diagnosis")
                .HasColumnType("VARCHAR(100)");


            builder.Property(x => x.AppointmentStatus)
                .HasColumnName("Status")
                .HasColumnType("CHAR");

            builder.Property(x => x.AppointmentDateTime)
                .HasColumnName("AppointmentDateTime")
                .HasColumnType("DATEIME");

            builder.Property(x => x.ModifiedDate)
                .HasColumnName("ModifiedDate")
                .HasColumnType("DATEIME");

            builder.Property(x => x.ModifiedReason)
              .HasColumnName("ModifiedReason")
              .HasColumnType("VARCHAR(150)");

            builder.Property(x => x.DeletedBy)
                .HasColumnName("DeletedBy")
                .HasColumnType("UniqueIdentifier");

            builder.Property(x => x.DeletedDate)
              .HasColumnName("DeletedDate")
              .HasColumnType("DATEIME");

            builder.Property(x => x.DeletedReason)
              .HasColumnName("DeletedReason")
              .HasColumnType("VARCHAR(150)");

            builder.Property(x => x.PatientId)
              .HasColumnName("PatientId")
              .HasColumnType("UniqueIdentifier");

            builder.Property(x => x.PhysicianId)
              .HasColumnName("PhysicianId")
              .HasColumnType("UniqueIdentifier");

            builder.Property(x => x.NurseId)
              .HasColumnName("NurseId")
              .HasColumnType("UniqueIdentifier");
        }
    }
}
