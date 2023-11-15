using System.ComponentModel.DataAnnotations;

namespace sample_crm.Core.Entities;

public class Flow
{
    public int Id { get; set; }
    [StringLength(maximumLength: 50)]
    public string Name { get; set; }
    public int FlowStateId { get; set; }
    public FlowState FlowState { get; set; }
    public bool Freeze { get; set; } = false;
}
