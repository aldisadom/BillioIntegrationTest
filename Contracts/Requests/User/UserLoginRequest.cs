﻿namespace BillioIntegrationTest.Contracts.Requests.User;

public class UserLoginRequest
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
