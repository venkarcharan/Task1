using venkat.Common.DTOs;

namespace venkat.service.Abstraction
{
    public interface IAuthService
    {
        Task<LoginResponse?> LoginAsync(
            LoginRequest request);
    }
}