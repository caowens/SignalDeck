using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SignalDeck.Api.Data.Entities;

namespace SignalDeck.Api.Data.Configurations
{
    public class ApplicationConfiguration : IEntityTypeConfiguration<Application>
    {
        public void Configure(EntityTypeBuilder<Application> builder)
        {
            builder.HasKey(a => a.Id);

            builder.HasIndex(a => a.ApiKey).IsUnique();

            builder.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(200);
        }
    }
}