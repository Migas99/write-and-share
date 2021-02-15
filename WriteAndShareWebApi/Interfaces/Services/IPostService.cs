using System.Collections.Generic;
using System.Threading.Tasks;
using WriteAndShareWebApi.Models.RequestModels;
using WriteAndShareWebApi.Models.ResponseModels;
using WriteAndShareWebApi.Models.ResponseModels.Posts;

namespace WriteAndShareWebApi.Interfaces.Services
{
    public interface IPostService
    {
        Task<List<GetPostResponse>> ObtainFeed(string requester);
        Task<List<GetPostResponse>> GetAllPosts(string requester, string role);
        Task<List<GetPostResponse>> GetPostsByUsername(string requester, string role, string target);
        Task<List<GetPostResponse>> GetMyPosts(string requester);
        Task<List<GetPostResponse>> GetPostsMentioned(string requester);
        Task<GetPostResponse> GetPostById(string requester, string role, int postId);
        Task<GetPostResponse> CreatePost(string requester, CreatePostRequest req);
        Task<SuccessResponse> DeletePost(string requester, int postId);
    }
}
