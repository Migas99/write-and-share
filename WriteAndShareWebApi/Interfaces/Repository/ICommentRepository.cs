using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WriteAndShareWebApi.Models;
using WriteAndShareWebApi.Models.ResponseModels;

namespace WriteAndShareWebApi.Interfaces.Repository
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetCommentsByUsername(string username);
        Task<List<Comment>> GetCommentsByPublicationId(int postId);
        Task<List<Comment>> GetCommentsByCommentId(int commentId);
        Task<Comment> GetCommentById(int commentId);
        Task<Comment> AddComment(Comment comment);
        Task<SuccessResponse> DeleteCommentById(int commentId);
    }
}
