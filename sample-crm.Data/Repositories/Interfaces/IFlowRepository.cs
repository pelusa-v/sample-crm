using sample_crm.Core.Entities;
namespace sample_crm.Data.Interfaces;

public interface IFlowRepository
{
    Task<Flow> GetFlow(int id);
    Task<List<Flow>> ListFlows();
    Task<Flow> CreateFlow(Flow newFlow);
    Task<Flow> UpdateFlow(Flow newFlow);
    Task DeleteFlow(int id);
}
