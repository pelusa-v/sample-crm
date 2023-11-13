using System;
using sample_crm.Core.Entities;

namespace sample_crm.Application.DTOs
{
	public class FlowStateDTO
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Flow> Flows { get; set; }
    }
}

