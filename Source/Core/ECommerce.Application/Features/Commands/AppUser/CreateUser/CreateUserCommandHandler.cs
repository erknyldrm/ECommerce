using ECommerce.Application.Abstractions.Services;
using ECommerce.Application.DTOs;
using MediatR;


namespace ECommerce.Application.Features.Commands.AppUser.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        private readonly IUserService _userService;

        public CreateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            CreateUserResponse response = await _userService.CreateAsync(new()
            {
                Email = request.Email,
                NameSurname = request.NameSurname,
                Username = request.Username,
                Password = request.Password,
                PasswordConfirm = request.PasswordConfirm
            });

            return new()
            {
                Message = response.Message,
                Succeeded = response.Succeeded
            };
        }
    }
}
