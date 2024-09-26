﻿using System.ComponentModel.DataAnnotations;

namespace BillioIntegrationTest.Contracts.Requests.User;

public class UserLoginRequest
{
    [Required]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
}
