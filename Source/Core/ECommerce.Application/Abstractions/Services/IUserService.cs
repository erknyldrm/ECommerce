using ECommerce.Application.DTOs;

namespace ECommerce.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<CreateUserResponse> CreateAsync(CreateUser createUser);
    }
}
