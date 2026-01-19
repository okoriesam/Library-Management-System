using LibraryManagementSystems.Application.Dtos.Request.User;
using LibraryManagementSystems.Application.Responses;
using LibraryManagementSystems.Application.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystems.Application.Commands.User
{
    public class UserLoginCommand : LoginRequestModel, IRequest<Result<string>>
    {

    }

    public class UserLoginCommandHandler : IRequestHandler<UserLoginCommand, Result<string>>
    {
        private readonly IUserService _userService;
        public UserLoginCommandHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<Result<string>> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _userService.UserLoginAsync(request);
                if (response == null)
                {
                    return Result<string>.Failure("User login failed");
                }

                return response;
            }
            catch (Exception ex)
            {
                return Result<string>.Failure($"An unexpected error occurred. {ex.Message}");
            }
        }
    }
}
