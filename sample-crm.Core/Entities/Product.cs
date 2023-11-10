using System.ComponentModel.DataAnnotations;

namespace sample_crm.Core.Entities;

public class Product
{
    public int Id { get; set; }
    [StringLength(maximumLength: 100)]
    public string Name { get; set; }
}
