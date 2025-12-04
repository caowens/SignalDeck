using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using SignalDeck.Domain.Entities;
using ApplicationEntity = SignalDeck.Domain.Entities.Application;

namespace SignalDeck.Infrastructure.Data
{
    public class SignalDeckDbContext : DbContext
    {
        public DbSet<ApplicationEntity> Applications { get; set; }
        public DbSet<Event> Events { get; set; }
        public SignalDeckDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SignalDeckDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}