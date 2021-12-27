using DomainLayer.Models.Master;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.EntityMapper.MasterMapper
{
    public class AllergyMapper : IEntityTypeConfiguration<Allergy>
    {
        public void Configure(EntityTypeBuilder<Allergy> builder)
        {
            builder.HasKey(x => x.Id).HasName("pk_allergyid");
            builder.Property(x => x.Id).ValueGeneratedOnAdd()
                .HasColumnName("Id")
                .HasColumnType("UniqueIdentifier");

            builder.Property(x => x.AllergyCode)
                .HasColumnName("AllergyCode")
                .HasColumnType("VARCHAR(50)");

            builder.Property(x => x.AllergyType)
                .HasColumnName("AllergyType")
                .HasColumnType("VARCHAR(100)");

            builder.Property(x => x.AllergyName)
                .HasColumnName("AllergyName")
                .HasColumnType("VARCHAR(150)");

            builder.Property(x => x.ClinicalInformation)
                .HasColumnName("ClinicalInformation")
                .HasColumnType("VARCHAR(150)");
        }
    }
}
