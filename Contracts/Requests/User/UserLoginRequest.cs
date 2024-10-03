using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace BillioIntegrationTest.Contracts.Requests.User;

public class UserLoginRequest
{
    [Required]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;

    public override string ToString()
    {
        return $"Email: {Email}, Password: {Password}";
    }
}
