using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using V_Tube.Application.Abstractions.IRepository;
using V_Tube.Application.Abstractions.IServices;
using V_Tube.Application.API_Response;
using V_Tube.Application.DTO;
using V_Tube.Application.Utilis;
using V_Tube.Domain.Models;

namespace V_Tube.Application.Services
{
    public class UserService(IUserRepository repository,ITokenService service) : IUserService
    {
        public async Task<APIResponse<LoginResponse>> Login(UserRequest model)
        {
            if (String.IsNullOrEmpty(model.Email.Trim()) || String.IsNullOrEmpty(model.Password.Trim()))
                return APIResponse<LoginResponse>.ErrorResponse("Username or Password is required");

            var user = await repository.FirstOrDefaultAsync(_ => _.Email == model.Email);

            if (user is null)
                return APIResponse<LoginResponse>.ErrorResponse("Invalid email and or password");

            var isPasswordCorrect = AppEncryption.VerifyPassword(model.Password, user.Password);

            if(!isPasswordCorrect)
                return APIResponse<LoginResponse>.ErrorResponse("Invalid email and or password");

            var accessToken = service.GenerateAccessToken(user);

            return APIResponse<LoginResponse>.SuccessResponse(new LoginResponse(accessToken,user.RefreshToken),"Logged in successfully");
        }

        public async Task<APIResponse<int>> Signup(UserRequest model)
        {
            if (String.IsNullOrEmpty(model.Email.Trim()) || String.IsNullOrEmpty(model.Password.Trim()))
                return APIResponse<int>.ErrorResponse("Username or Password is required");


            var isEmailTaken = await repository.ExistsAsync(_ => _.Email == model.Email);

            if (isEmailTaken)
                return APIResponse<int>.ErrorResponse("Email is already taken");

            var user = new User
            {
                Email = model.Email,
                Password = AppEncryption.HashPassword(model.Password),
            };

            user.RefreshToken = service.GenerateRefreshToken(user.Id);

            var response = await repository.InsertAsync(user);

            if (response > 0)
                return APIResponse<int>.SuccessResponse(response, "Signned Up Successfully");

            return APIResponse<int>.ErrorResponse();
        }
    }
}
