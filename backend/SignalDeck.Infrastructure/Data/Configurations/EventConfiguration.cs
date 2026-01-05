using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SignalDeck.Domain.Entities;

namespace SignalDeck.Infrastructure.Data.Configurations
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("uuid"); // Explicitly set for Postgres

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
            
            // Ensure the Foreign Key is treated as a UUID
            builder.Property(e => e.ApplicationId)
                .IsRequired()
                .HasColumnType("uuid");

            builder.HasOne(e => e.Application)
                .WithMany(a => a.Events)
                .HasForeignKey(e => e.ApplicationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}