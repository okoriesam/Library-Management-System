using LibraryManagementSystems.Application.Dtos.Request.User;
using LibraryManagementSystems.Application.Responses;
using LibraryManagementSystems.Application.Services;
using LibraryManagementSystems.Domain.Entity;
using LibraryManagementSystems.Infrastructure.Helper;
using LibraryManagementSystems.Infrastructure.Persistence.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystems.Infrastructure.Persistence.Repository
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SecurityHelper _securityHelper;
        public UserService(UserManager<User> userManager, SecurityHelper securityHelper)
        {
            _userManager = userManager;
            _securityHelper = securityHelper;
        }
        public async Task<Result<string>> RegisterUserAsync(RegistrationRequestModel register)
        {
            try
            {
                //checking if user already exist
                var validateEmail = await _userManager.FindByEmailAsync(register.Email);
                if (validateEmail != null) 
                {
                    return Result<string>.Failure("Email Already Exist");
                }

                var newUser = new User
                {
                    Email = register.Email,
                    FirstName = register.FirstName,
                    LastName = register.LastName,
                    PhoneNumber = register.PhoneNumber,
                    UserName = register.Email
                };

                // creating a new User instance
                var createUserAsync =  await _userManager.CreateAsync(newUser, register.Password);
                if (!createUserAsync.Succeeded)
                {
                    var errors = string.Join(", ", createUserAsync.Errors.Select(e => e.Description));

                    return Result<string>.Failure(errors);
                }
                // returning user id and successful registration message
                return Result<string>.Success(newUser.Id, "Registration Done Successfully");
            }
            catch (Exception ex)
            {
                return Result<string>.Failure($"An unexpected error occurred. {ex.Message}");
            }
        }

        public async Task<Result<string>> UserLoginAsync(LoginRequestModel requestModel)
        {
            try
            {
                //checking if user already exist
                var findEmailAsync = await _userManager.FindByEmailAsync(requestModel.Email);
                if (findEmailAsync == null)
                {
                    return Result<string>.Failure("Invalid Email or Password");
                }

                var validateEmailAndPasswordAsync = await _userManager.CheckPasswordAsync(findEmailAsync, requestModel.Password);
                if (!validateEmailAndPasswordAsync)
                {
                    return Result<string>.Failure("Invalid Email or Password");
                }

                // Method that generate tokens
                var token = _securityHelper.TokenGenerator(findEmailAsync);
                return Result<string>.Success(token, "Login Successful");
            }
            catch (Exception ex)
            {
                return Result<string>.Failure($"An unexpected error occurred. {ex.Message}");
            }
        }
    }
}
