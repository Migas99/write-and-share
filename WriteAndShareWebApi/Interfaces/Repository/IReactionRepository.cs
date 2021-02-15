using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WriteAndShareWebApi.Models;

namespace WriteAndShareWebApi.Interfaces.Repository
{
    public interface IReactionRepository
    {
        Task<List<Post>> GetPostsReactedByUsername(string username);
        Task<List<Comment>> GetCommentsReactedByUsername(string username);
        Task<bool> DidYouVote(string username, int id);
        Task<bool> DidYouUpvote(string username, int id);
        Task<bool> DidYouDownvote(string username, int id);
        Task<bool> Upvote(string username, int id);
        Task<bool> Downvote(string username, int id);
        Task<bool> DeleteReaction(string username, int id);
    }
}
