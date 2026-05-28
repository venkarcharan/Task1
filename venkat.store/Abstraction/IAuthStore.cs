using venkat.Common.DTOs;

namespace venkat.store.Abstraction
{
    public interface IAuthStore
    {
        Task<LoginResponse> LoginAsync(
            LoginRequest request);
    }
}