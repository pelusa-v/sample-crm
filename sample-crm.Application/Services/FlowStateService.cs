using System;
using AutoMapper;
using sample_crm.Application.DTOs;
using sample_crm.Application.Services.Interfaces;
using sample_crm.Core.Entities;
using sample_crm.Data.Interfaces;

namespace sample_crm.Application.Services
{
	public class FlowStateService : IFlowStateService
	{
        private readonly IFlowStateRepository _flowStateRepo;
        private readonly IMapper _mapper;

        public FlowStateService(IFlowStateRepository flowStateRepo, IMapper mapper)
        {
            _flowStateRepo = flowStateRepo;
            _mapper = mapper;
        }

        public async Task<FlowStateDTO> Create(CreateFlowStateDTO flowState)
        {
            var flowStateToCreate = _mapper.Map<FlowState>(flowState);
            var newFlow = await _flowStateRepo.CreateFlowState(flowStateToCreate);
            return _mapper.Map<FlowStateDTO>(newFlow);
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<FlowStateDTO> Get(int id)
        {
            var flowState= await _flowStateRepo.GetFlowState(id);
            return _mapper.Map<FlowStateDTO>(flowState);
        }

        public async Task<IEnumerable<FlowStateDTO>> List()
        {
            var flowStates = await _flowStateRepo.ListFlowStates();
            return _mapper.Map<IEnumerable<FlowStateDTO>>(flowStates);
        }

        public async Task<FlowStateDTO> Update(int id, UpdateFlowStateDTO flowState)
        {
            var flowStateFound = await _flowStateRepo.GetFlowState(id);
            var flowStateToUpdate = _mapper.Map<FlowState>(flowState);
            flowStateToUpdate.Id = flowStateFound.Id;

            var newState = await _flowStateRepo.UpdateFlowState(flowStateToUpdate);
            return _mapper.Map<FlowStateDTO>(newState);
        }

        public async Task<bool> ValidateFlowState(int id)
        {
            var flowState = await _flowStateRepo.GetFlowState(id);
            if(flowState == null)
            {
                return false;
            }

            return true;
        }
    }
}

