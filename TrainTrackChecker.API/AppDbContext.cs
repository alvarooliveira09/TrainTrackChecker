using System.Collections.Generic;
using System.Reflection.Emit;
using System;
using TrainTrackChecker.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Reflection.Metadata;


namespace TrainTrackChecker.API {

    public class AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor httpContextAccessor) 
        : IdentityDbContext(options) {

        public DbSet<Hardware> Hardwares { get; set; }
        public DbSet<Trecho> Trechos { get; set; }
        public DbSet<Trecho_Registro> Trechos_Registros { get; set; }
        public DbSet<OrdemManutencao> OrdensManutencao { get; set; }
        public DbSet<OrdemManutencao_Local> OrdensManutencao_Locais { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>().ToList()) {
                switch (entry.State) {
                    case EntityState.Added:
                        entry.Entity.CreatedOn = DateTime.UtcNow;
                        entry.Entity.CreatedBy = httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? "unknown user";
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedOn = DateTime.UtcNow;
                        entry.Entity.LastModifiedBy = httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? "unknown user";
                        break;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Hardware>().Property(e => e.Codigo).ValueGeneratedOnAdd();
            modelBuilder.Entity<OrdemManutencao>().Property(e => e.Codigo).ValueGeneratedOnAdd();
            modelBuilder.Entity<Trecho>().Property(e => e.Codigo).ValueGeneratedOnAdd();

        }
    }
}
