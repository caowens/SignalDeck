using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using SignalDeck.Api.Data.Entities;
using ApplicationEntity = SignalDeck.Api.Data.Entities.Application;

namespace SignalDeck.Api.Data
{
    public class SignalDeckDbContext : DbContext
    {
        public DbSet<ApplicationEntity> Applications { get; set; }
        public DbSet<SignalEntity> Signals { get; set; }
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