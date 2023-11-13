using AutoMapper;
using sample_crm.Application.DTOs;
using sample_crm.Application.Services.Interfaces;
using sample_crm.Data.Repositories;

namespace sample_crm.Application.Services
{
	public class FlowTrayService : IFlowTrayService
	{
        private readonly FlowRepository _flowRepo;
        private readonly FlowStateRepository _flowStateRepo;
        private readonly IMapper _mapper;

		public FlowTrayService(FlowRepository flowRepo, FlowStateRepository flowStateRepo, IMapper mapper)
		{
            _flowRepo = flowRepo;
            _flowStateRepo = flowStateRepo;
            _mapper = mapper;
		}

        public async Task<FlowDTO> PromoteFlowState(int flowId, int stateId)
        {
            //var flowState = await _flowStateRepo.GetFlowState(stateId);
            var flow = await _flowRepo.GetFlow(flowId);
            flow.FlowSateId = stateId;
            var newFlow = await _flowRepo.UpdateFlow(flow);
            return _mapper.Map<FlowDTO>(newFlow);
        }

        public Task<FlowDTO> PromoteFlowState(int flowId, string stateName)
        {
            throw new NotImplementedException();
        }
    }
}

