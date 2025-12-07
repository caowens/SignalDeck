using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SignalDeck.Domain.Entities;

namespace SignalDeck.Infrastructure.Data.Configurations
{
    public class MetricConfiguration : IEntityTypeConfiguration<Metric>
    {
        public void Configure(EntityTypeBuilder<Metric> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(m => m.Value)
                .IsRequired();
            
            builder.Property(m => m.Timestamp)
                .IsRequired();

            builder.HasOne(m => m.Application)
                .WithMany(a => a.Metrics)
                .HasForeignKey(m => m.ApplicationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}