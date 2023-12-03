using System.ComponentModel.DataAnnotations;

namespace sample_crm.Application;

public class GrantAdminAuthorizationDTO
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}
