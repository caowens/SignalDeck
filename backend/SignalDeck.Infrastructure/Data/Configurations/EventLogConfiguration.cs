using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SignalDeck.Domain.Entities;

namespace SignalDeck.Infrastructure.Data.Configurations
{
    public class EventLogConfiguration : IEntityTypeConfiguration<EventLog>
    {
        public void Configure(EntityTypeBuilder<EventLog> builder)
        {
            builder.HasKey(el => el.Id);

            builder.Property(el => el.Message)
                .IsRequired()
                .HasMaxLength(500);
            
            builder.Property(el => el.Severity)
                .IsRequired();
            
            builder.Property(el => el.Timestamp)
                .IsRequired();
            
            builder.HasOne(el => el.Application)
                .WithMany(a => a.EventLogs)
                .HasForeignKey(el => el.ApplicationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}