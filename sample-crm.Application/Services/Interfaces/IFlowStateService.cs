using System;
using sample_crm.Application.DTOs;

namespace sample_crm.Application.Services.Interfaces
{
	public interface IFlowStateService
	{
        Task<FlowStateDTO> Create(CreateFlowStateDTO flow);
        Task<FlowStateDTO> Update(int id, UpdateFlowStateDTO flow);
        Task<FlowStateDTO> Get(int id);
        Task<IEnumerable<FlowStateDTO>> List();
        Task Delete(int id);
    }
}

