﻿using Mango.Services.AuthAPI.Models;

namespace Mango.Services.AuthAPI.Services.IService
{
    public interface IJWTTokenGenerator
    {
        string GenerateToken(ApplicationUser applicationUser);
    }
}
