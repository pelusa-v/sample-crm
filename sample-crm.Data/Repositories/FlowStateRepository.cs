using Microsoft.EntityFrameworkCore;
using sample_crm.Core.Entities;
using sample_crm.Data.Interfaces;

namespace sample_crm.Data.Repositories;

public class FlowStateRepository : IFlowStateRepository
{
    private readonly AppDbContext _context;

    public FlowStateRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<FlowState> CreateFlowState(FlowState newState)
    {
        _context.Add(newState);
        await _context.SaveChangesAsync();
        return newState;
    }

    public async Task DeleteFlowState(int id)
    {
        var toDelete = _context.FlowStates.Where(f => f.Id == id).First();
        _context.FlowStates.Remove(toDelete);
        await _context.SaveChangesAsync();
    }

    public async Task<FlowState> GetDefaultFlowState()
    {
        return await _context.FlowStates.Where(f => f.Default == true).FirstOrDefaultAsync();
    }

    public async Task<FlowState> GetFlowState(int id)
    {
        return await _context.FlowStates.Include(f => f.Flows).FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task<List<FlowState>> ListFlowStates()
    {
        return await _context.FlowStates.Include(f => f.Flows).ToListAsync();
    }

    public async Task<FlowState> UpdateFlowState(FlowState newState)
    {
        _context.Update(newState);
        await _context.SaveChangesAsync();
        return newState;
    }
}
