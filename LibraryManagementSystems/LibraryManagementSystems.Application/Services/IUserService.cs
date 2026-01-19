using LibraryManagementSystems.Application.Dtos.Request.User;
using LibraryManagementSystems.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystems.Application.Services
{
    public interface IUserService
    {
        Task<Result<string>> RegisterUserAsync(RegistrationRequestModel register);
        Task<Result<string>> UserLoginAsync(LoginRequestModel requestModel);
    }
}
