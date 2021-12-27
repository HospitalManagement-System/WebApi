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
    public class UserDetailsMapper : IEntityTypeConfiguration<UserDetails>
    {
        public void Configure(EntityTypeBuilder<UserDetails> builder)
        {
            builder.HasKey(x => x.Id).HasName("pk_userid");
            builder.Property(x => x.Id).ValueGeneratedOnAdd()
                .HasColumnName("Id")
                .HasColumnType("UniqueIdentifier");

            builder.Property(x => x.UserName)
              .HasColumnName("UserName")
              .HasColumnType("VARCHAR(100)");

            builder.Property(x => x.Password)
              .HasColumnName("Password")
              .HasColumnType("VARCHAR(100)");

            builder.Property(x => x.Status)
              .HasColumnName("Status")
              .HasColumnType("CHAR");

            builder.Property(x => x.IsFirstLogIn)
            .HasColumnName("IsFirstLogIn")
            .HasColumnType("CHAR");


            builder.Property(x => x.NoOfAttempts)
            .HasColumnName("NoOfAttempts")
            .HasColumnType("INT");

            //builder.HasKey(x => x.RoleRefId).HasName("fk_roleid");
            builder.Property(x => x.RoleId)
             .HasColumnName("RoleId")
             .HasColumnType("UniqueIdentifier");
        }
    }
}
