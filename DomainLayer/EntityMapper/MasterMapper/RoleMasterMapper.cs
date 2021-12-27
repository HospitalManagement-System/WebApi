using DomainLayer.Models.Master;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.EntityMapper
{
    public class RoleMasterMapper :  IEntityTypeConfiguration<RoleMaster>
    {
        public void Configure(EntityTypeBuilder<RoleMaster> builder)
        {
            builder.HasKey(x => x.Id).HasName("pk_roleid");
            builder.Property(x => x.Id).ValueGeneratedOnAdd()
                    .HasColumnName("Id")
                    .HasColumnType("UniqueIdentifier");

            builder.Property(x => x.UserRole)
                  .HasColumnName("Role")
                  .HasColumnType("VARCHAR(50)");
        }
    }
}
