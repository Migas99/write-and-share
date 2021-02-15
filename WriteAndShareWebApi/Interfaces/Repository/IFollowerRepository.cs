using WriteAndShareWebApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WriteAndShareWebApi.Interfaces.Repository
{
    public interface IFollowerRepository
    {
        Task<bool> IsHeYourFollower(string user, string follower);
        Task<bool> AreYouFollowingHim(string user, string following);
        Task<bool> DidYouRequestToFollow(string username, string target);
        Task<List<User>> GetUserFollowers(string username);
        Task<List<User>> GetUsersFollowing(string username);
        Task<Request> GetRequestToUser(string username, int requestId);
        Task<List<Request>> GetRequestsToUser(string username);
        Task<Request> GetRequestMadeByUser(string username, int requestId);
        Task<List<Request>> GetRequestsMadeByUser(string username);
        Task<bool> AddAsFollower(string requester, string target);
        Task<bool> AddRequestToFollow(string requester, string target);
        Task<bool> RemoveAsFollower(string follower, string target);
        Task<bool> RemoveRequest(int requestId);
        Task<bool> AcceptRequest(int requestId);
    }
}
