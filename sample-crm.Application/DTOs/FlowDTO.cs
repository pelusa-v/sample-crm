using System;
using sample_crm.Core.Entities;

namespace sample_crm.Application.DTOs
{
	public class FlowDTO
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public FlowStateDTO FlowState { get; set; }
    }
}

