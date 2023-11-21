using System.Data.Common;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using sample_crm.Core;
using sample_crm.Core.Entities;

namespace sample_crm.Data;

public class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions options) : base(options) 
    {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);  // don't forget!

        modelBuilder.Entity<FlowState>().HasData(
            new FlowState
            {
                Id = 1,
                Name = "Validación previa",
                Default = true,
            },
            new FlowState
            {
                Id = 2,
                Name = "Preparación de informes",
            },
            new FlowState
            {
                Id = 3,
                Name = "Recepción de legalización",
            },
            new FlowState
            {
                Id = 4,
                Name = "Firma de partes",
            }
        );
    }

    public DbSet<Flow> Flows { get; set; }
    public DbSet<FlowState> FlowStates { get; set; }
}
