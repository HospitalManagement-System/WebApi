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
    public class DrugMapper : IEntityTypeConfiguration<Drug>
    {
        public void Configure(EntityTypeBuilder<Drug> builder)
        {
            builder.HasKey(x => x.Id).HasName("pk_drugid");
            builder.Property(x => x.Id).ValueGeneratedOnAdd()
                .HasColumnName("Id")
                .HasColumnType("UniqueIdentifier");

            builder.Property(x => x.DrugName)
                .HasColumnName("DrugName")
                .HasColumnType("VARCHAR(150)");

            builder.Property(x => x.DrugForm)
               .HasColumnName("DrugForm")
               .HasColumnType("VARCHAR(100)");

            builder.Property(x => x.DrugStrength)
               .HasColumnName("DrugStrength")
               .HasColumnType("VARCHAR(100)");

            builder.Property(x => x.DrugGenericName)
                .HasColumnName("DrugGenericName")
                .HasColumnType("VARCHAR(150)");

            builder.Property(x => x.DrugManufacturerName)
               .HasColumnName("DrugManufacturerName")
               .HasColumnType("VARCHAR(150)");

        }
    }
}
