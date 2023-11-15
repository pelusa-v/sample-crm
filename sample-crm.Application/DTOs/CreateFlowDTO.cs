using System;
using sample_crm.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace sample_crm.Application.DTOs
{
	public class CreateFlowDTO
	{
        public string Name { get; set; }
        public int FlowSateId { get; set; }
    }
}

