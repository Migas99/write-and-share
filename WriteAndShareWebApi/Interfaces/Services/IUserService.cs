using WriteAndShareWebApi.Models.RequestModels;
using WriteAndShareWebApi.Models.ResponseModels;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using WriteAndShareWebApi.Models.ResponseModels.Users;

namespace WriteAndShareWebApi.Interfaces
{
    public interface IUserService
    {
        Task<List<GetUserBasicInfoResponse>> GetAllUsersBasicInfo(string requester);
        Task<List<GetUserBasicInfoResponse>> GetListOfUsersBasicInfoByUsername(string requester, List<string> usernames);
        Task<GetUserProfileBasicInfoResponse> GetUserBasicInfo(string requester, string target);
        Task<GetUserResponse> GetUserByUsername(string requester, string role, string target);
        Task<GetUserResponse> GetProfile(string requester);
        Task<SuccessResponse> UpdateHeader(string requester, IFormFile header);
        Task<SuccessResponse> UpdateAvatar(string requester, IFormFile avatar);
        Task<GetUserResponse> UpdateUser(string requester, UpdateUserRequest update);
        Task<SuccessResponse> DeleteUser(string requester);
    }
}
