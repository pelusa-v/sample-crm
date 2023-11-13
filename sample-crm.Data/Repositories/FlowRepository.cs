using Microsoft.EntityFrameworkCore;
using sample_crm.Core.Entities;
using sample_crm.Data.Interfaces;

namespace sample_crm.Data.Repositories;

public class FlowRepository : IFlowRepository
{
    private readonly AppDbContext _context;

    public FlowRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Flow> CreateFlow(Flow newFlow)
    {
        _context.Add(newFlow);
        await _context.SaveChangesAsync();
        return newFlow;
    }

    public async Task DeleteFlow(int id)
    {
        var toDelete = _context.Flows.Where(f => f.Id == id).First();
        _context.Flows.Remove(toDelete);
        await _context.SaveChangesAsync();
    }

    public async Task<Flow> GetFlow(int id)
    {
        return await _context.Flows.FindAsync(id);
    }

    public async Task<List<Flow>> ListFlows()
    {
        return await _context.Flows.ToListAsync();
    }

    public async Task<Flow> UpdateFlow(Flow newFlow)
    {
        _context.Update(newFlow);
        await _context.SaveChangesAsync();
        return newFlow;
    }
}
