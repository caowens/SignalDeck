using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SignalDeck.Domain.Entities;

namespace SignalDeck.Infrastructure.Data.Configurations
{
    public class ErrorLogConfiguration : IEntityTypeConfiguration<ErrorLog>
    {
        public void Configure(EntityTypeBuilder<ErrorLog> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Message).IsRequired().HasMaxLength(500);
            builder.Property(e => e.StackTrace).HasMaxLength(2000);
            builder.Property(e => e.Timestamp).IsRequired();

            builder.HasOne(e => e.Application)
                .WithMany(a => a.ErrorLogs)
                .HasForeignKey(e => e.ApplicationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}