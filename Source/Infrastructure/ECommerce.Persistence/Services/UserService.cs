using ECommerce.Application.Abstractions.Services;
using ECommerce.Application.DTOs;
using ECommerce.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace ECommerce.Persistence.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;

        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CreateUserResponse> CreateAsync(CreateUser createUser)
        {
            var result = await _userManager.CreateAsync(new AppUser()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = createUser.Username,
                Email = createUser.Email,
                NameSurname = createUser.NameSurname,

            }, createUser.Password);

            CreateUserResponse response = new() { Succeeded = result.Succeeded };


            return new()
            {
                Succeeded = response.Succeeded,
                Message = response.Succeeded ? "Successful" : String.Join(",", result.Errors.Select(p => p.Description))
            };
        }
    }
}
