using System.ComponentModel.DataAnnotations;

namespace sample_crm.Application;

public class AuthRequestDTO
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}
