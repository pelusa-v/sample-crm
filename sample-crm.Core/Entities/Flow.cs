using System.ComponentModel.DataAnnotations;

namespace sample_crm.Core.Entities;

public class Flow
{
    public int Id { get; set; }
    [StringLength(maximumLength: 50)]
    public int Name { get; set; }
    public int FlowSateId { get; set; }
    public FlowState FlowState { get; set; }
}
