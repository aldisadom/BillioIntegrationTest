﻿using System.ComponentModel.DataAnnotations;

namespace BillioIntegrationTest.Contracts.Requests.User;

public class UserAddRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string LastName { get; set; } = string.Empty;
    [Required]
    public string Email { get; set; } = string.Empty;
    [Required]
    [MinLength(8)]
    public string Password { get; set; } = string.Empty;

    public override string ToString()
    {
        return $"Name: {Name}, LastName: {LastName}, Email: {Email}, Password: {Password}";
    }
}
