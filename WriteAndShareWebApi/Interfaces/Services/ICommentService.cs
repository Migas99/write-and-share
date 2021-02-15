using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WriteAndShareWebApi.Models;
using WriteAndShareWebApi.Models.RequestModels.Comments;
using WriteAndShareWebApi.Models.ResponseModels;
using WriteAndShareWebApi.Models.ResponseModels.Comments;

namespace WriteAndShareWebApi.Interfaces.Services
{
    public interface ICommentService
    {
        Task<List<GetCommentResponse>> GetCommentsByUser(string requester, string role, string username);
        Task<List<GetCommentResponse>> GetCommentsByPublication(string requester, string role, int postId);
        Task<List<GetCommentResponse>> GetCommentsByComment(string requester, string role, int commentId);
        Task<List<GetCommentResponse>> GetMyComments(string requester); 
        Task<GetCommentResponse> GetCommentById(string requester, string role, int commentId); 
        Task<GetCommentResponse> AddComment(string requester, CreateCommentRequest req);
        Task<SuccessResponse> DeleteComment(string requester, int commentId);
    }
}
