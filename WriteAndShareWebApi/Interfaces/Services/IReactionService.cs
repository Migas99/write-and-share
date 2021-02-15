using System.Collections.Generic;
using System.Threading.Tasks;
using WriteAndShareWebApi.Models;
using WriteAndShareWebApi.Models.ResponseModels;
using WriteAndShareWebApi.Models.ResponseModels.Comments;
using WriteAndShareWebApi.Models.ResponseModels.Posts;

namespace WriteAndShareWebApi.Interfaces.Services
{
    public interface IReactionService
    {
        Task<List<GetPostResponse>> GetPublicationsReactedByRequester(string requester);
        Task<List<GetPostResponse>> GetPublicationsReactedByUser(string requester, string role, string target);
        Task<List<GetCommentResponse>> GetCommentsReactedByRequester(string requester);
        Task<List<GetCommentResponse>> GetCommentsReactedByUser(string requester, string role, string target);
        Task<SuccessResponse> Upvote(string requester, int id);
        Task<SuccessResponse> Downvote(string requester, int id);
        Task<SuccessResponse> DeleteVote(string requester, int id);
    }
}
