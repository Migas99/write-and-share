using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.Threading.Tasks;
using WriteAndShareWebApi.Enums;
using WriteAndShareWebApi.Exceptions;
using WriteAndShareWebApi.Interfaces;
using WriteAndShareWebApi.Interfaces.Repository;
using WriteAndShareWebApi.Interfaces.Services;
using WriteAndShareWebApi.Models;
using WriteAndShareWebApi.Models.ResponseModels;
using WriteAndShareWebApi.Models.ResponseModels.Comments;
using WriteAndShareWebApi.Models.ResponseModels.Posts;
using static WriteAndShareWebApi.Utils.UploadsHandler;

namespace WriteAndShareWebApi.Services
{
    public class ReactionService : IReactionService
    {
        private readonly IWebHostEnvironment env;
        private readonly IUserRepository userRepository;
        private readonly IFollowerRepository followerRepository;
        private readonly IPostRepository postRepository;
        private readonly ICommentRepository commentRepository;
        private readonly IReactionRepository reactionRepository;
        private readonly INotificationRepository notificationRepository;

        public ReactionService(IWebHostEnvironment _env, IUserRepository _userRepository, IFollowerRepository _followerRepository, IPostRepository _postRepository, 
            ICommentRepository _commentRepository, IReactionRepository _reactionRepository, INotificationRepository _notificationRepository)
        {
            env = _env;
            userRepository = _userRepository;
            followerRepository = _followerRepository;
            postRepository = _postRepository;
            commentRepository = _commentRepository;
            reactionRepository = _reactionRepository;
            notificationRepository = _notificationRepository;
        }

        public async Task<List<GetPostResponse>> GetPublicationsReactedByRequester(string requester)
        {
            List<Post> posts = await reactionRepository.GetPostsReactedByUsername(requester);
            List<GetPostResponse> res = new List<GetPostResponse>();

            foreach (Post post in posts)
            {
                bool add = false;

                if (requester == post.Author)
                {
                    add = true;
                }
                else
                {
                    if (await followerRepository.AreYouFollowingHim(requester, post.Author))
                    {
                        add = true;
                    }
                    else
                    {
                        User author = await userRepository.GetUserByUsername(post.Author);
                        if (author.Privacy == Privacies.Public) add = true;
                    }
                }

                if (add)
                {
                    byte[] upload = null;
                    string contentType = null;
                    if (post.UploadPath != null)
                    {
                        upload = GetPublicationUpload(env.ContentRootPath, post.UploadPath);
                        contentType = GetPublicationContentType(post.UploadPath);
                    }

                    bool didYouUpvote = await reactionRepository.DidYouUpvote(requester, post.Id);
                    bool didYouDownvote = false;
                    if (!didYouUpvote) didYouUpvote = await reactionRepository.DidYouDownvote(requester, post.Id);

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
            }

            return res;
        }

        public async Task<List<GetPostResponse>> GetPublicationsReactedByUser(string requester, string role, string target)
        {
            List<Post> posts = await reactionRepository.GetPostsReactedByUsername(target);
            List<GetPostResponse> res = new List<GetPostResponse>();

            foreach (Post post in posts)
            {
                bool add = false;

                if (requester == post.Author)
                {
                    add = true;
                }
                else
                {
                    if (await followerRepository.AreYouFollowingHim(requester, post.Author))
                    {
                        add = true;
                    }
                    else
                    {
                        User author = await userRepository.GetUserByUsername(post.Author);
                        if (author.Privacy == Privacies.Public) add = true;
                    }
                }

                if (add)
                {
                    byte[] upload = null;
                    string contentType = null;
                    if (post.UploadPath != null)
                    {
                        upload = GetPublicationUpload(env.ContentRootPath, post.UploadPath);
                        contentType = GetPublicationContentType(post.UploadPath);
                    }

                    bool didYouUpvote = await reactionRepository.DidYouUpvote(requester, post.Id);
                    bool didYouDownvote = false;
                    if (!didYouUpvote) didYouUpvote = await reactionRepository.DidYouDownvote(requester, post.Id);

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
            }

            return res;
        }

        public async Task<List<GetCommentResponse>> GetCommentsReactedByRequester(string requester)
        {
            List<GetCommentResponse> res = new List<GetCommentResponse>();
            List<Comment> comments = await reactionRepository.GetCommentsReactedByUsername(requester);

            foreach (Comment comment in comments)
            {
                bool add = false;

                if (requester == comment.Author)
                {
                    add = true;
                }
                else
                {
                    if (await followerRepository.AreYouFollowingHim(requester, comment.Author))
                    {
                        add = true;
                    }
                    else
                    {
                        User author = await userRepository.GetUserByUsername(comment.Author);
                        if (author.Privacy == Privacies.Public) add = true;
                    }
                }

                if (add)
                {
                    bool didYouUpvote = await reactionRepository.DidYouUpvote(requester, comment.Id);
                    bool didYouDownvote = false;
                    if (!didYouUpvote) didYouUpvote = await reactionRepository.DidYouDownvote(requester, comment.Id);

                    res.Add(new GetCommentResponse
                    {
                        Id = comment.Id,
                        Target = comment.Target,
                        Message = comment.Message,
                        Author = comment.Author,
                        SubmitDate = comment.SubmitDate,
                        Upvoted = comment.Upvoted,
                        Downvoted = comment.Downvoted,
                        Score = comment.Score,
                        Mentions = comment.Mentions,
                        CommentsNumber = comment.CommentsNumber,
                        DidYouUpvote = didYouUpvote,
                        DidYouDownvote = didYouDownvote
                    });
                }
            }

            return res;
        }

        public async Task<List<GetCommentResponse>> GetCommentsReactedByUser(string requester, string role, string target)
        {
            List<GetCommentResponse> res = new List<GetCommentResponse>();
            List<Comment> comments = await reactionRepository.GetCommentsReactedByUsername(target);

            foreach (Comment comment in comments)
            {
                bool add = false;

                if (requester == comment.Author || role == Roles.Administrator)
                {
                    add = true;
                }
                else
                {
                    if (await followerRepository.AreYouFollowingHim(requester, comment.Author))
                    {
                        add = true;
                    }
                    else
                    {
                        User author = await userRepository.GetUserByUsername(comment.Author);
                        if (author.Privacy == Privacies.Public) add = true;
                    }
                }

                if (add)
                {
                    bool didYouUpvote = await reactionRepository.DidYouUpvote(requester, comment.Id);
                    bool didYouDownvote = false;
                    if (!didYouUpvote) didYouUpvote = await reactionRepository.DidYouDownvote(requester, comment.Id);

                    res.Add(new GetCommentResponse
                    {
                        Id = comment.Id,
                        Target = comment.Target,
                        Message = comment.Message,
                        Author = comment.Author,
                        SubmitDate = comment.SubmitDate,
                        Upvoted = comment.Upvoted,
                        Downvoted = comment.Downvoted,
                        Score = comment.Score,
                        Mentions = comment.Mentions,
                        CommentsNumber = comment.CommentsNumber,
                        DidYouUpvote = didYouUpvote,
                        DidYouDownvote = didYouDownvote
                    });
                }
            }

            return res;
        }

        public async Task<SuccessResponse> Upvote(string requester, int id)
        {
            Post targetPost = await postRepository.GetPostById(id);
            Comment targetComment = await commentRepository.GetCommentById(id);
            if (targetPost == null && targetComment == null) 
                throw new CustomException(404, "The publication or comment that you wanted to upvote was not found.");

            if (await reactionRepository.DidYouUpvote(requester, id)) 
                throw new CustomException(400, "You already upvoted this post or comment.");

            string targetAuthor;
            string type;
            string postId = null;
            string commentId = null;
            if (targetPost != null)
            {
                targetAuthor = targetPost.Author;
                type = Notifications.UserUpvotedThePost;
                postId = targetPost.Id.ToString();
            }
            else
            {
                targetAuthor = targetComment.Author;
                type = Notifications.UserUpvotedTheComment;
                commentId = targetComment.Id.ToString();
            }

            if (requester != targetAuthor && !await followerRepository.AreYouFollowingHim(requester, targetAuthor))
            {
                User author = await userRepository.GetUserByUsername(targetAuthor);
                if (author.Privacy == Privacies.Private)
                    throw new CustomException(403, "You're not authorized to upvote this publication or comment.");
            }

            bool upvote = await reactionRepository.Upvote(requester, id);
            if (!upvote) throw new CustomException(500, "An very unexpected error has occured.");

            await notificationRepository.AddNotification(
                    new Notification
                    {
                        Type = type,
                        Author = requester,
                        Target = targetAuthor,
                        PostId = postId,
                        CommentId = commentId,
                        AnswerId = null
                    });

            return new SuccessResponse();
        }

        public async Task<SuccessResponse> Downvote(string requester, int id)
        {
            Post targetPost = await postRepository.GetPostById(id);
            Comment targetComment = await commentRepository.GetCommentById(id);
            if (targetPost == null && targetComment == null) 
                throw new CustomException(404, "The publication or comment that you wanted to downvote was not found.");

            if (await reactionRepository.DidYouDownvote(requester, id)) 
                throw new CustomException(400, "You already downvoted this post or comment.");

            string targetAuthor;
            string type;
            string postId = null;
            string commentId = null;
            if (targetPost != null)
            {
                targetAuthor = targetPost.Author;
                type = Notifications.UserDownvotedThePost;
                postId = targetPost.Id.ToString();
            }
            else
            {
                targetAuthor = targetComment.Author;
                type = Notifications.UserDownvotedTheComment;
                commentId = targetComment.Id.ToString();
            }

            if (requester != targetAuthor && !await followerRepository.AreYouFollowingHim(requester, targetAuthor))
            {
                User author = await userRepository.GetUserByUsername(targetAuthor);
                if (author.Privacy == Privacies.Private)
                    throw new CustomException(403, "You're not authorized to downvote this publication or comment.");
            }

            bool downvote = await reactionRepository.Downvote(requester, id);
            if (!downvote) throw new CustomException(500, "An very unexpected error has occured.");

            await notificationRepository.AddNotification(
                    new Notification
                    {
                        Type = type,
                        Author = requester,
                        Target = targetAuthor,
                        PostId = postId,
                        CommentId = commentId,
                        AnswerId = null
                    });

            return new SuccessResponse();
        }

        public async Task<SuccessResponse> DeleteVote(string requester, int id)
        {
            Post targetPost = await postRepository.GetPostById(id);
            Comment targetComment = await commentRepository.GetCommentById(id);
            if (targetPost == null && targetComment == null) 
                throw new CustomException(404, "The publication or comment that you wanted to remove your vote was not found.");

            if(!await reactionRepository.DidYouVote(requester, id)) 
                throw new CustomException(400, "You can't remove your vote because you didn't vote to begin with.");

            bool delete = await reactionRepository.DeleteReaction(requester, id);
            if(!delete) throw new CustomException(500, "An very unexpected error has occured.");

            return new SuccessResponse();
        }
    }
}
