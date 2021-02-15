using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WriteAndShareWebApi.Enums;
using WriteAndShareWebApi.Exceptions;
using WriteAndShareWebApi.Interfaces;
using WriteAndShareWebApi.Interfaces.Repository;
using WriteAndShareWebApi.Interfaces.Services;
using WriteAndShareWebApi.Models;
using WriteAndShareWebApi.Models.RequestModels;
using WriteAndShareWebApi.Models.ResponseModels;
using WriteAndShareWebApi.Models.ResponseModels.Posts;
using WriteAndShareWebApi.Utils;

namespace WriteAndShareWebApi.Services
{
    public class PostService : IPostService
    {
        private readonly IConfiguration config;
        private readonly IWebHostEnvironment env;
        private readonly IUserRepository userRepository;
        private readonly IFollowerRepository followerRepository;
        private readonly IPostRepository postRepository;
        private readonly IReactionRepository reactionRepository;
        private readonly INotificationRepository notificationRepository;

        public PostService(IConfiguration _config, IWebHostEnvironment _env, IUserRepository _userRepository, IFollowerRepository _followerRepository, 
            IPostRepository _postRepository, IReactionRepository _reactionRepository, INotificationRepository _notificationRepository)
        {
            config = _config;
            env = _env;
            userRepository = _userRepository;
            followerRepository = _followerRepository;
            postRepository = _postRepository;
            reactionRepository = _reactionRepository;
            notificationRepository = _notificationRepository;
        }

        public async Task<List<GetPostResponse>> ObtainFeed(string requester)
        {
            List<Post> posts = await postRepository.GetFeed(requester);
            List<GetPostResponse> res = new List<GetPostResponse>();

            foreach (Post post in posts)
            {
                byte[] upload = null;
                string contentType = null;
                if (post.UploadPath != null)
                {
                    upload = UploadsHandler.GetPublicationUpload(env.ContentRootPath, post.UploadPath);
                    contentType = UploadsHandler.GetPublicationContentType(post.UploadPath);
                }

                bool didYouUpvote = await reactionRepository.DidYouUpvote(requester, post.Id);
                bool didYouDownvote = false;
                if (!didYouUpvote) didYouDownvote = await reactionRepository.DidYouDownvote(requester, post.Id);

                res.Add(new GetPostResponse
                {
                    Id = post.Id,
                    Upload = upload,
                    ContentType = contentType,
                    Link = post.Link,
                    Message = post.Message,
                    Author = post.Author,
                    SubmitDate = post.SubmitDate,
                    Upvoted = post.Upvoted,
                    Downvoted = post.Downvoted,
                    Score = post.Score,
                    Mentions = post.Mentions,
                    CommentsNumber = post.CommentsNumber,
                    DidYouUpvote = didYouUpvote,
                    DidYouDownvote = didYouDownvote
                });
            }

            return res;
        }

        public async Task<List<GetPostResponse>> GetAllPosts(string requester, string role)
        {
            List<Post> posts;
            List <GetPostResponse> res = new List<GetPostResponse>();

            if (role == Roles.Administrator)
            {
                posts = await postRepository.GetAllPosts();
            }
            else
            {
                posts = await postRepository.GetAllWatchablePostsForUser(requester);
            }

            foreach (Post post in posts)
            {
                byte[] upload = null;
                string contentType = null;
                if (post.UploadPath != null)
                {
                    upload = UploadsHandler.GetPublicationUpload(env.ContentRootPath, post.UploadPath);
                    contentType = UploadsHandler.GetPublicationContentType(post.UploadPath);
                }

                bool didYouUpvote = await reactionRepository.DidYouUpvote(requester, post.Id);
                bool didYouDownvote = false;
                if (!didYouUpvote) didYouDownvote = await reactionRepository.DidYouDownvote(requester, post.Id);

                res.Add(new GetPostResponse
                {
                    Id = post.Id,
                    Upload = upload,
                    ContentType = contentType,
                    Link = post.Link,
                    Message = post.Message,
                    Author = post.Author,
                    SubmitDate = post.SubmitDate,
                    Upvoted = post.Upvoted,
                    Downvoted = post.Downvoted,
                    Score = post.Score,
                    Mentions = post.Mentions,
                    CommentsNumber = post.CommentsNumber,
                    DidYouUpvote = didYouUpvote,
                    DidYouDownvote = didYouDownvote
                });
            }

            return res;
        }

        public async Task<List<GetPostResponse>> GetPostsByUsername(string requester, string role, string target)
        {
            User user = await userRepository.GetUserByUsername(target);
            if (user == null) throw new CustomException(404, "The target user was not found.");

            if (requester != user.Username && user.Privacy == Privacies.Private && role != Roles.Administrator)
                if (!await followerRepository.AreYouFollowingHim(requester, target)) 
                    throw new CustomException(403, "You're not authorized to see the publications of this user.");

            List<Post> posts = await postRepository.GetPostsMadeByUser(target);
            List<GetPostResponse> res = new List<GetPostResponse>();

            foreach (Post post in posts)
            {
                byte[] upload = null;
                string contentType = null;
                if (post.UploadPath != null)
                {
                    upload = UploadsHandler.GetPublicationUpload(env.ContentRootPath, post.UploadPath);
                    contentType = UploadsHandler.GetPublicationContentType(post.UploadPath);
                }

                bool didYouUpvote = await reactionRepository.DidYouUpvote(requester, post.Id);
                bool didYouDownvote = false;
                if (!didYouUpvote) didYouDownvote = await reactionRepository.DidYouDownvote(requester, post.Id);

                res.Add(new GetPostResponse
                {
                    Id = post.Id,
                    Upload = upload,
                    ContentType = contentType,
                    Link = post.Link,
                    Message = post.Message,
                    Author = post.Author,
                    SubmitDate = post.SubmitDate,
                    Upvoted = post.Upvoted,
                    Downvoted = post.Downvoted,
                    Score = post.Score,
                    Mentions = post.Mentions,
                    CommentsNumber = post.CommentsNumber,
                    DidYouUpvote = didYouUpvote,
                    DidYouDownvote = didYouDownvote
                });
            }

            return res;
        }

        public async Task<List<GetPostResponse>> GetMyPosts(string requester)
        {
            List<Post> posts = await postRepository.GetPostsMadeByUser(requester);
            List<GetPostResponse> res = new List<GetPostResponse>();

            foreach(Post post in posts)
            {
                byte[] upload = null;
                string contentType = null;
                if (post.UploadPath != null)
                {
                    upload = UploadsHandler.GetPublicationUpload(env.ContentRootPath, post.UploadPath);
                    contentType = UploadsHandler.GetPublicationContentType(post.UploadPath);
                }

                bool didYouUpvote = await reactionRepository.DidYouUpvote(requester, post.Id);
                bool didYouDownvote = false;
                if (!didYouUpvote) didYouDownvote = await reactionRepository.DidYouDownvote(requester, post.Id);

                res.Add(new GetPostResponse
                {
                    Id = post.Id,
                    Upload = upload,
                    ContentType = contentType,
                    Link = post.Link,
                    Message = post.Message,
                    Author = post.Author,
                    SubmitDate = post.SubmitDate,
                    Upvoted = post.Upvoted,
                    Downvoted = post.Downvoted,
                    Score = post.Score,
                    Mentions = post.Mentions,
                    CommentsNumber = post.CommentsNumber,
                    DidYouUpvote = didYouUpvote,
                    DidYouDownvote = didYouDownvote
                });
            }

            return res;
        }

        public async Task<List<GetPostResponse>> GetPostsMentioned(string requester)
        {
            List<Post> posts = await postRepository.GetPostsUserWasMentioned(requester);
            List<GetPostResponse> res = new List<GetPostResponse>();

            foreach (Post post in posts)
            {
                byte[] upload = null;
                string contentType = null;
                if (post.UploadPath != null)
                {
                    upload = UploadsHandler.GetPublicationUpload(env.ContentRootPath, post.UploadPath);
                    contentType = UploadsHandler.GetPublicationContentType(post.UploadPath);
                }

                bool didYouUpvote = await reactionRepository.DidYouUpvote(requester, post.Id);
                bool didYouDownvote = false;
                if (!didYouUpvote) didYouDownvote = await reactionRepository.DidYouDownvote(requester, post.Id);

                res.Add(new GetPostResponse
                {
                    Id = post.Id,
                    Upload = upload,
                    ContentType = contentType,
                    Link = post.Link,
                    Message = post.Message,
                    Author = post.Author,
                    SubmitDate = post.SubmitDate,
                    Upvoted = post.Upvoted,
                    Downvoted = post.Downvoted,
                    Score = post.Score,
                    Mentions = post.Mentions,
                    CommentsNumber = post.CommentsNumber,
                    DidYouUpvote = didYouUpvote,
                    DidYouDownvote = didYouDownvote
                });
            }

            return res;
        }

        public async Task<GetPostResponse> GetPostById(string requester, string role, int postId)
        {
            Post post = await postRepository.GetPostById(postId);
            if (post == null) throw new CustomException(404, "There is no post with such Id.");

            User author = await userRepository.GetUserByUsername(post.Author);
            if (requester != author.Username && author.Privacy == Privacies.Private 
                && role != Roles.Administrator && !post.Mentions.Contains(requester))
                if (!await followerRepository.AreYouFollowingHim(requester, post.Author)) 
                    throw new CustomException(403, "You're not authorized to see this publication.");

            byte[] upload = null;
            string contentType = null;
            if (post.UploadPath != null)
            {
                upload = UploadsHandler.GetPublicationUpload(env.ContentRootPath, post.UploadPath);
                contentType = UploadsHandler.GetPublicationContentType(post.UploadPath);
            }

            bool didYouUpvote = await reactionRepository.DidYouUpvote(requester, post.Id);
            bool didYouDownvote = false;
            if (!didYouUpvote) didYouDownvote = await reactionRepository.DidYouDownvote(requester, post.Id);

            return new GetPostResponse
            {
                Id = post.Id,
                Upload = upload,
                ContentType = contentType,
                Link = post.Link,
                Message = post.Message,
                Author = post.Author,
                SubmitDate = post.SubmitDate,
                Upvoted = post.Upvoted,
                Downvoted = post.Downvoted,
                Score = post.Score,
                Mentions = post.Mentions,
                CommentsNumber = post.CommentsNumber,
                DidYouUpvote = didYouUpvote,
                DidYouDownvote = didYouDownvote
            };
        }

        public async Task<GetPostResponse> CreatePost(string requester, CreatePostRequest req)
        {
            if (req.Upload == null && req.Link == null && req.Message == null)
                throw new CustomException(400, "Your post can't be empty.");

            List<string> mentions = new List<string>();
            if (req.Mentions != null && req.Mentions != "") mentions = req.Mentions.Split(";").ToList();

            foreach (string username in mentions)
            {
                if (await userRepository.GetUserByUsername(username) == null) 
                    throw new CustomException(404, "One of the users you mentioned does not exist.");
            }

            if (req.Upload != null && req.Link != null) 
                throw new CustomException(400, "Your post can only contain either an upload or an link.");

            string uploadPath = null;
            if (req.Upload != null) 
                uploadPath = await
                    UploadsHandler.SavePublicationUpload(
                        config["SightEngine:api_user"], config["SightEngine:api_secret"], env.ContentRootPath, req.Upload);
          

            Post post = new Post
            {
                UploadPath = uploadPath,
                Link = req.Link,
                Message = req.Message,
                Author = requester,
                Mentions = mentions
            };

            Post postCreated = await postRepository.AddPost(post);

            if (postCreated.Mentions != null || postCreated.Mentions.Count > 0)
            {
                foreach (string mention in mentions)
                {
                    await notificationRepository.AddNotification(
                    new Notification
                    {
                        Type = Notifications.UserMentionedInPost,
                        Author = requester,
                        Target = mention,
                        PostId = postCreated.Id.ToString(),
                        CommentId = null,
                        AnswerId = null
                    });
                }
            }

            byte[] upload = null;
            if (postCreated.UploadPath != null) upload = UploadsHandler.GetPublicationUpload(env.ContentRootPath, postCreated.UploadPath);

            return new GetPostResponse { 
                Id = postCreated.Id,
                Upload = upload,
                Link = postCreated.Link,
                Message = postCreated.Message,
                Author = postCreated.Author,
                SubmitDate = postCreated.SubmitDate,
                Upvoted = postCreated.Upvoted,
                Downvoted = postCreated.Downvoted,
                Score = postCreated.Score,
                Mentions = postCreated.Mentions,
                CommentsNumber = postCreated.CommentsNumber,
                DidYouUpvote = false,
                DidYouDownvote = false
            };
        }

        public async Task<SuccessResponse > DeletePost(string requester, int postId)
        {
            Post post = await postRepository.GetPostById(postId);
            if (post == null) throw new CustomException(404, "The target post to delete was not found.");
            if (post.Author != requester) throw new CustomException(403, "You're not authorized to delete other user's posts.");
            if(post.UploadPath != null) UploadsHandler.DeletePublicationUpload(env.ContentRootPath, post.UploadPath);
            return await postRepository.DeletePostById(postId);
        }
    }
}
