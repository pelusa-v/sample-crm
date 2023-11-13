using AutoMapper;
using sample_crm.Application.DTOs;
using sample_crm.Application.Services.Interfaces;
using sample_crm.Core.Entities;
using sample_crm.Data.Repositories;
namespace sample_crm.Application.Services
{
	public class FlowService : IFlowService
	{
        private readonly FlowRepository _flowRepo;
        private readonly FlowStateRepository _flowStateRepo;
        private readonly IMapper _mapper;

        public FlowService(FlowRepository flowRepo, FlowStateRepository flowStateRepo, IMapper mapper)
		{
			_flowRepo = flowRepo;
            _flowStateRepo = flowStateRepo;
            _mapper = mapper;
		}

        public async Task<FlowDTO> Create(CreateFlowDTO flow)
        {
            var defaultState = await _flowStateRepo.GetDefaultFlowState();
            var flowToCreate = _mapper.Map<Flow>(flow);

            if(defaultState != null)
            {
                flowToCreate.FlowSateId = defaultState.Id;
            }

            var newFlow = await _flowRepo.CreateFlow(flowToCreate);
            return _mapper.Map<FlowDTO>(newFlow);
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<FlowDTO> Get(int id)
        {
            var flow = await _flowRepo.GetFlow(id);
            return _mapper.Map<FlowDTO>(flow);
        }

        public async Task<IEnumerable<FlowDTO>> List()
        {
            var flows = await _flowRepo.ListFlows();
            return _mapper.Map<IEnumerable<FlowDTO>>(flows);
        }

        public async Task<FlowDTO> Update(int id, UpdateFlowDTO flow)
        {
            var flowFound = await _flowRepo.GetFlow(id);
            var flowToUpdate = _mapper.Map<Flow>(flow);
            flowToUpdate.Id = flowFound.Id;

            var newFlow = await _flowRepo.UpdateFlow(flowToUpdate);
            return _mapper.Map<FlowDTO>(newFlow);
        }
    }
}

