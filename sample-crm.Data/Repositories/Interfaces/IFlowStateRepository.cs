using sample_crm.Core.Entities;

namespace sample_crm.Data.Interfaces;

public interface IFlowStateRepository
{
    Task<FlowState> GetFlowState(int id);
    Task<FlowState> GetDefaultFlowState();
    Task<List<FlowState>> ListFlowStates();
    Task<FlowState> CreateFlowState(FlowState newState);
    Task<FlowState> UpdateFlowState (FlowState newState);
    Task DeleteFlowState(int id);
}
