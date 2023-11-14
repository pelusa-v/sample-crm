using sample_crm.Application.DTOs;

namespace sample_crm.Application.Services.Interfaces
{
	public interface IFlowService
	{
		Task<FlowDTO> Create(CreateFlowDTO flow);
		Task<FlowDTO> Update(int id, UpdateFlowDTO flow);
		Task<FlowDTO> Get(int id);
		Task<IEnumerable<FlowDTO>> List();
		Task<bool> ValidateFlow(int flowId);
		Task Delete(int id);
	}
}

