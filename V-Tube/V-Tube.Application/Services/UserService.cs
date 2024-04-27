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
    public class UserService(IUserRepository repository,ITokenService service,IContextService contextService) : IUserService
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

        public async Task<APIResponse<int>> Logout()
        {
            var user = await repository.FindOneAsync(contextService.GetContextId());

            if (user is null)
                return APIResponse<int>.ErrorResponse("User does not exist");

            user.RefreshToken = string.Empty;

            var res = await repository.UpdateAsync(user);

            if (res > 0)
                return APIResponse<int>.SuccessResponse(res, "User logged out successfully");

            return APIResponse<int>.ErrorResponse();
        }

        public async Task<APIResponse<int>> Signup(UserRequest model)
        {
            if (
                string.IsNullOrEmpty(model.Email.Trim()) ||
                string.IsNullOrEmpty(model.UserName.Trim()) || 
                string.IsNullOrEmpty(model.Password.Trim())
                )
                return APIResponse<int>.ErrorResponse("Username ,email and Password is required");


            var isEmailOrUsernameTaken = await repository.ExistsAsync(_ => _.Email == model.Email || _.UserName == model.UserName);

            if (isEmailOrUsernameTaken)
                return APIResponse<int>.ErrorResponse("Username or Email is already taken");

            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email,
                Password = AppEncryption.HashPassword(model.Password),
            };

            user.RefreshToken = service.GenerateRefreshToken(user.Id);

            var response = await repository.InsertAsync(user);

            if (response > 0)
                return APIResponse<int>.SuccessResponse(response, "Signned Up Successfully");

            return APIResponse<int>.ErrorResponse();
        }


        public async Task<APIResponse<TokenResponse>> RefreshToken(RefreshTokenRequest model)
        {
            var user = await repository.FirstOrDefaultAsync(_ => _.RefreshToken == model.RefreshToken);

            if (user is null)
                return APIResponse<TokenResponse>.ErrorResponse("Invalid Token");

            var isTokenExpired = AppEncryption.IsTokenExpired(user.RefreshToken!);

            if (isTokenExpired)
                return APIResponse<TokenResponse>.ErrorResponse("Refresh Token has been expired please login again");

            user.RefreshToken = service.GenerateRefreshToken(user.Id);

            var res = await repository.UpdateAsync(user);

            if (res > 0)
            {
                var accessToken = service.GenerateAccessToken(user);
                var response = new TokenResponse(accessToken, user.RefreshToken);

                return APIResponse<TokenResponse>.SuccessResponse(response, "Token has been refreshed");
            }

            return APIResponse<TokenResponse>.ErrorResponse();
        }
    }
}
