using System.ComponentModel.DataAnnotations;

namespace sample_crm.Core.Entities;

public class FlowState
{
    public int Id { get; set; }
    [StringLength(maximumLength: 50)]
    public string Name { get; set; }
    public bool Default { get; set; } = false;
    public ICollection<Flow> Flows { get; set; }
}
