using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WriteAndShareWebApi.Models;
using WriteAndShareWebApi.Models.RequestModels;
using WriteAndShareWebApi.Models.ResponseModels;

namespace WriteAndShareWebApi.Interfaces.Repository
{
    public interface IPostRepository
    {
        Task<Post> AddPost(Post post);
        Task<List<Post>> GetFeed(string username);
        Task<List<Post>> GetAllPosts();
        Task<List<Post>> GetAllWatchablePostsForUser(string username);
        Task<List<Post>> GetPostsMadeByUser(string username);
        Task<List<Post>> GetPostsUserWasMentioned(string username);
        Task<Post> GetPostById(int postId);
        Task<SuccessResponse> DeletePostById(int postId);
    }
}
