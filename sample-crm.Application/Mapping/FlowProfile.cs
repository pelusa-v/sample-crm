using AutoMapper;
using sample_crm.Core.Entities;
using sample_crm.Application.DTOs;
namespace sample_crm.Application.Mapping
{
	public class FlowProfile : Profile
	{
		public FlowProfile()
		{
			CreateMap<CreateFlowDTO, Flow>();
			CreateMap<UpdateFlowDTO, Flow>();
			CreateMap<Flow, FlowDTO>();

            CreateMap<CreateFlowStateDTO, FlowState>();
            CreateMap<UpdateFlowStateDTO, FlowState>();
            CreateMap<FlowState, FlowStateDTO>();
        }
	}
}

