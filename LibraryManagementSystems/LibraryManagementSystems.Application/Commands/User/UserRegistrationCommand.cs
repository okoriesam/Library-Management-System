using LibraryManagementSystems.Application.Commands.Book;
using LibraryManagementSystems.Application.Dtos.Request.Book;
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
    public class UserRegistrationCommand : RegistrationRequestModel, IRequest<Result<string>>
    {
       
    }

    public class UserRegistrationCommandHandler : IRequestHandler<UserRegistrationCommand, Result<string>>
    {
        private readonly IUserService _userService;
        public UserRegistrationCommandHandler(IUserService userService)
        {
           _userService = userService;
        }
        public async Task<Result<string>> Handle(UserRegistrationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _userService.RegisterUserAsync(request);
                if (response == null)
                {
                    return Result<string>.Failure("User registration failed");
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
