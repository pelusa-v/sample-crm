﻿using AutoMapper;
using sample_crm.Application.DTOs;
using sample_crm.Application.Exceptions;
using sample_crm.Application.Services.Interfaces;
using sample_crm.Data.Interfaces;

namespace sample_crm.Application.Services
{
	public class FlowTrayService : IFlowTrayService
	{
        private readonly IFlowRepository _flowRepo;
        private readonly IFlowStateRepository _flowStateRepo;
        private readonly IMapper _mapper;

		public FlowTrayService(IFlowRepository flowRepo, IFlowStateRepository flowStateRepo, IMapper mapper)
		{
            _flowRepo = flowRepo;
            _flowStateRepo = flowStateRepo;
            _mapper = mapper;
		}

        public async Task<FlowDTO> PromoteFlowState(int flowId, int stateId)
        {
            var flow = await _flowRepo.GetFlow(flowId);
            var flowState = await _flowStateRepo.GetFlowState(stateId);

            flow.FlowStateId = flowState.Id;
            var newFlow = await _flowRepo.UpdateFlow(flow);
            return _mapper.Map<FlowDTO>(newFlow);
        }

        public Task<FlowDTO> PromoteFlowState(int flowId, string stateName)
        {
            throw new NotImplementedException();
        }

        public async Task<FlowDTO> UnfreezeFlowTray(int flowId)
        {
            var flow = await _flowRepo.GetFlow(flowId);
            flow.Freeze = true;
            var newFlow = await _flowRepo.UpdateFlow(flow);
            return _mapper.Map<FlowDTO>(newFlow);
        }

        public async Task<FlowDTO> FreezeFlowTray(int flowId)
        {
            var flow = await _flowRepo.GetFlow(flowId);
            flow.Freeze = false;
            var newFlow = await _flowRepo.UpdateFlow(flow);
            return _mapper.Map<FlowDTO>(newFlow);
        }

    }
}

