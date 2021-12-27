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
    public class NotesMapper : IEntityTypeConfiguration<Notes>
    {
        public void Configure(EntityTypeBuilder<Notes> builder)
        {
            builder.HasKey(x => x.Id).HasName("pk_notesid");
            builder.Property(x => x.Id).ValueGeneratedOnAdd()
                .HasColumnName("Id")
                .HasColumnType("UniqueIdentifier");

            builder.Property(x => x.SenderMessage)
                .HasColumnName("SenderMessage")
                .HasColumnType("VARCHAR(200)");

            builder.Property(x => x.ResponseMessage)
                .HasColumnName("ResponseMessage")
                .HasColumnType("VARCHAR(200)");

            builder.Property(x => x.SenderId)
                .HasColumnName("SenderId")
                .HasColumnType("UniqueIdentifier");

            builder.Property(x => x.RecieverId)
                .HasColumnName("RecieverId")
                .HasColumnType("UniqueIdentifier");
   
            builder.Property(x => x.SentDateTime)
                .HasColumnName("SentDateTime")
                .HasColumnType("DATEIME");

            builder.Property(x => x.RecievedDateTime)
                .HasColumnName("RecievedDateTime")
                .HasColumnType("DATEIME");
        }
    }
}
