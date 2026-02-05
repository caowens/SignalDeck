using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SignalDeck.Api.Data.Entities;

namespace SignalDeck.Api.Data.Configurations
{
    public class SignalConfiguration : IEntityTypeConfiguration<SignalEntity>
    {
        public void Configure(EntityTypeBuilder<SignalEntity> builder)
        {
            builder.HasKey(s => s.InternalId);

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100);
            
            builder.Property(e => e.ApplicationId)
                .IsRequired();

            builder.HasOne(s => s.Application)
                .WithMany(a => a.Signals)
                .HasForeignKey(s => s.ApplicationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(s => s.Properties)
                .HasColumnType("jsonb")
                .IsRequired();

            builder.Property(s => s.EventTimestamp)
                .IsRequired();

            builder.HasIndex(e => new { e.ApplicationId, e.EventTimestamp });
        }
    }
}