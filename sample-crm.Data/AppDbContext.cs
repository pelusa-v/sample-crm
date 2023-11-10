using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using sample_crm.Core;
using sample_crm.Core.Entities;

namespace sample_crm.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options) 
    {}

    public DbSet<Flow> Flows { get; set; }
    public DbSet<FlowState> FlowStates { get; set; }
}
