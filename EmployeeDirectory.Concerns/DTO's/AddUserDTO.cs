﻿namespace EmployeeDirectory.Concerns.DTO_s;

public class AddUserDTO
{
    public required string Name { get; set; }

    public required string Email { get; set; }

    public required string Password { get; set; }

    public required string ImageData { get; set; }
}
