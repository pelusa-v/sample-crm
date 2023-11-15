using AutoMapper;
using sample_crm.Application.DTOs;
using sample_crm.Application.Exceptions;
using sample_crm.Application.Services.Interfaces;
using sample_crm.Core.Entities;
using sample_crm.Data.Interfaces;
namespace sample_crm.Application.Services
{
	public class FlowService : IFlowService
	{
        private readonly IFlowRepository _flowRepo;
        private readonly IFlowStateRepository _flowStateRepo;
        private readonly IMapper _mapper;

        public FlowService(IFlowRepository flowRepo, IFlowStateRepository flowStateRepo, IMapper mapper)
		{
			_flowRepo = flowRepo;
            _flowStateRepo = flowStateRepo;
            _mapper = mapper;
		}

        public async Task<FlowDTO> Create(CreateFlowDTO flow)
        {
            var defaultState = await _flowStateRepo.GetDefaultFlowState();
            var flowToCreate = _mapper.Map<Flow>(flow);

            Console.WriteLine("----------------");
            Console.WriteLine(defaultState.Default);
            Console.WriteLine(defaultState.Name);
            Console.WriteLine(defaultState.Id);
            if(defaultState != null)
            {
                flowToCreate.FlowStateId = defaultState.Id;
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
            Console.WriteLine(flows[0].FlowState.Name);
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

        public async Task<bool> ValidateFlow(int flowId)
        {
            var flow = await _flowRepo.GetFlow(flowId);
            if(flow == null)
            {
                return false;
            }

            return true;
        }
    }
}

