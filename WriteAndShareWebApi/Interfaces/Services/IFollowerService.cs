using WriteAndShareWebApi.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WriteAndShareWebApi.Interfaces.Services
{
    public interface IFollowerService
    {
        Task<List<GetUserBasicInfoResponse>> GetFollowers(string requester);
        Task<List<GetUserBasicInfoResponse>> GetFollowing(string requester);
        Task<List<GetRequestResponse>> GetRequestsReceived(string requester);
        Task<List<GetRequestResponse>> GetRequestsMade(string requester);
        Task<List<GetUserBasicInfoResponse>> GetUserFollowers(string requester, string role, string target);
        Task<List<GetUserBasicInfoResponse>> GetUserFollowing(string requester, string role, string target);
        Task<SuccessResponse> FollowUser(string requester, string target);
        Task<SuccessResponse> UnfollowUser(string requester, string target);
        Task<SuccessResponse> RemoveUserAsFollower(string requester, string target);
        Task<SuccessResponse> CancelRequest(string requester, int requestId);
        Task<SuccessResponse> AcceptRequest(string requester, int requestId);
        Task<SuccessResponse> RefuseRequest(string request, int requestId);
    }
}
