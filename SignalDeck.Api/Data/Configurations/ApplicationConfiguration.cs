using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ApplicationEntity = SignalDeck.Api.Data.Entities.Application;

namespace SignalDeck.Api.Data.Configurations
{
    public class ApplicationConfiguration : IEntityTypeConfiguration<ApplicationEntity>
    {
        public void Configure(EntityTypeBuilder<ApplicationEntity> builder)
        {
            builder.HasKey(a => a.InternalId);

            builder.HasIndex(a => a.ApiKey).IsUnique();

            builder.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(200);
        }
    }
}