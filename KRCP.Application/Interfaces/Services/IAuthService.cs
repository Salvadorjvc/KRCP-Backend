using KRCP.Application.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace KRCP.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<LoginResponseDto> LoginAsync(LoginRequestDto dto);
    }
}
