using System;
using sample_crm.Application.DTOs;

namespace sample_crm.Application.Services.Interfaces
{
	public interface IFlowTrayService
	{
		Task<FlowDTO> PromoteFlowState(int flowId, int stateId);
		Task<FlowDTO> PromoteFlowState(int flowId, string stateName);
        Task<FlowDTO> FreezeFlowTray(int flowId);
        Task<FlowDTO> UnfreezeFlowTray(int flowId);
	}
}

