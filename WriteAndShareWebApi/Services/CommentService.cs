using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WriteAndShareWebApi.Enums;
using WriteAndShareWebApi.Exceptions;
using WriteAndShareWebApi.Interfaces;
using WriteAndShareWebApi.Interfaces.Repository;
using WriteAndShareWebApi.Interfaces.Services;
using WriteAndShareWebApi.Models;
using WriteAndShareWebApi.Models.RequestModels.Comments;
using WriteAndShareWebApi.Models.ResponseModels;
using WriteAndShareWebApi.Models.ResponseModels.Comments;

namespace WriteAndShareWebApi.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUserRepository userRepository;
        private readonly IFollowerRepository followerRepository;
        private readonly IPostRepository postRepository;
        private readonly ICommentRepository commentRepository;
        private readonly IReactionRepository reactionRepository;
        private readonly INotificationRepository notificationRepository;

        public CommentService(IUserRepository _userRepository, IFollowerRepository _followerRepository, IPostRepository _postRepository, 
            ICommentRepository _commentRepository, IReactionRepository _reactionRepository, INotificationRepository _notificationRepository)
        {
            userRepository = _userRepository;
            followerRepository = _followerRepository;
            postRepository = _postRepository;
            commentRepository = _commentRepository;
            reactionRepository = _reactionRepository;
            notificationRepository = _notificationRepository;
        }

        public async Task<List<GetCommentResponse>> GetCommentsByUser(string requester, string role, string username)
        {
            if(requester != username && role != Roles.Administrator)
            {
                User author = await userRepository.GetUserByUsername(username);
                if (author == null) throw new CustomException(404, "The user was not found.");
                if(author.Privacy == Privacies.Private && !await followerRepository.AreYouFollowingHim(requester, username))
                    throw new CustomException(403, "You're not authorized to see the comments made by this user.");
            }
                    
            List<GetCommentResponse> res = new List<GetCommentResponse>();
            List<Comment> comments = await commentRepository.GetCommentsByUsername(requester);

            foreach (Comment comment in comments)
            {
                bool didYouUpvote = await reactionRepository.DidYouUpvote(requester, comment.Id);
                bool didYouDownvote = false;
                if (!didYouUpvote) didYouDownvote = await reactionRepository.DidYouDownvote(requester, comment.Id);

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

            return res;
        }

        public async Task<List<GetCommentResponse>> GetCommentsByPublication(string requester, string role, int postId)
        {
            Post post = await postRepository.GetPostById(postId);
            if (post == null) throw new CustomException(404, "The post was not found.");

            List<GetCommentResponse> res = new List<GetCommentResponse>();
            List<Comment> comments = await commentRepository.GetCommentsByPublicationId(postId);

            foreach (Comment comment in comments)
            {
                bool add = false;

                if (requester == post.Author || role == Roles.Administrator || requester == comment.Author)
                {
                    add = true;

                } 
                else
                {
                    if(await followerRepository.AreYouFollowingHim(requester, comment.Author))
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
                    if (!didYouUpvote) didYouDownvote = await reactionRepository.DidYouDownvote(requester, comment.Id);

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

        public async Task<List<GetCommentResponse>> GetCommentsByComment(string requester, string role, int commentId)
        {
            Comment targetComment = await commentRepository.GetCommentById(commentId);
            if (targetComment == null) throw new CustomException(404, "The comment was not found.");

            List<GetCommentResponse> res = new List<GetCommentResponse>();
            List<Comment> comments = await commentRepository.GetCommentsByCommentId(commentId);

            foreach (Comment comment in comments)
            {
                bool add = false;

                if (requester == comment.Author || role == Roles.Administrator || requester == targetComment.Author)
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
                    if (!didYouUpvote) didYouDownvote = await reactionRepository.DidYouDownvote(requester, comment.Id);

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

        public async Task<List<GetCommentResponse>> GetMyComments(string requester)
        {
            List<GetCommentResponse> res = new List<GetCommentResponse>();
            List<Comment> comments = await commentRepository.GetCommentsByUsername(requester);

            foreach(Comment comment in comments)
            {
                bool didYouUpvote = await reactionRepository.DidYouUpvote(requester, comment.Id);
                bool didYouDownvote = false;
                if (!didYouUpvote) didYouDownvote = await reactionRepository.DidYouDownvote(requester, comment.Id);

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

            return res;
        }

        public async Task<GetCommentResponse> GetCommentById(string requester, string role, int commentId)
        {
            Comment comment = await commentRepository.GetCommentById(commentId);
            if (comment == null) throw new CustomException(404, "The comment was not found.");

            User author = await userRepository.GetUserByUsername(comment.Author);
            if (requester != author.Username && author.Privacy == Privacies.Private && role != Roles.Administrator)
                if (!await followerRepository.AreYouFollowingHim(requester, comment.Author))
                    throw new CustomException(403, "You're not authorized to see this comment.");

            bool didYouUpvote = await reactionRepository.DidYouUpvote(requester, comment.Id);
            bool didYouDownvote = false;
            if (!didYouUpvote) didYouDownvote = await reactionRepository.DidYouDownvote(requester, comment.Id);

            return new GetCommentResponse
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
            };
        }

        public async Task<GetCommentResponse> AddComment(string requester, CreateCommentRequest req)
        {
            Post targetPost = await postRepository.GetPostById(req.Target);
            Comment targetComment = await commentRepository.GetCommentById(req.Target);
            if(targetPost == null && targetComment == null) 
                throw new CustomException(404, "The publication or comment that you wanted to make a comment on was not found.");

            string targetAuthor;
            string type;
            string postId = null;
            string commentId = null;
            if (targetPost != null)
            {
                targetAuthor = targetPost.Author;
                type = Notifications.UserCommentedThePost;
                postId = targetPost.Id.ToString();
            }
            else
            {
                targetAuthor = targetComment.Author;
                type = Notifications.UserCommentedTheComment;
                commentId = targetComment.Id.ToString();
            }

            if(requester != targetAuthor && !await followerRepository.AreYouFollowingHim(requester, targetAuthor))
            {
                User author = await userRepository.GetUserByUsername(targetAuthor);
                if (author.Privacy == Privacies.Private)
                    throw new CustomException(403, "You're not authorized to comment on this publication or comment.");
            }

            if(req.Mentions != null)
            {
                foreach (string username in req.Mentions)
                {
                    if (await userRepository.GetUserByUsername(username) == null)
                        throw new CustomException(404, "One of the users you mentioned does not exist.");
                }
            }
            else
            {
                req.Mentions = new List<string>();
            }

            Comment comment = new Comment
            {
                Target = req.Target,
                Message = req.Message,
                Author = requester,
                Mentions = req.Mentions
            };

            Comment commentCreated = await commentRepository.AddComment(comment);

            await notificationRepository.AddNotification(
                    new Notification
                    {
                        Type = type,
                        Author = requester,
                        Target = targetAuthor,
                        PostId = postId,
                        CommentId = commentId,
                        AnswerId = commentCreated.Id.ToString()
                    });

            if (commentCreated.Mentions != null || commentCreated.Mentions.Count > 0)
            {
                foreach (string mention in commentCreated.Mentions)
                {
                    await notificationRepository.AddNotification(
                    new Notification
                    {
                        Type = Notifications.UserMentionedInComment,
                        Author = requester,
                        Target = mention,
                        PostId = null,
                        CommentId = commentCreated.Id.ToString(),
                        AnswerId = null
                    });
                }
            }

            return new GetCommentResponse
            {
                Id = commentCreated.Id,
                Target = commentCreated.Target,
                Message = commentCreated.Message,
                Author = commentCreated.Author,
                SubmitDate = commentCreated.SubmitDate,
                Upvoted = commentCreated.Upvoted,
                Downvoted = commentCreated.Downvoted,
                Score = commentCreated.Score,
                Mentions = commentCreated.Mentions,
                CommentsNumber = commentCreated.CommentsNumber,
                DidYouUpvote = false,
                DidYouDownvote = false
            };
        }

        public async Task<SuccessResponse> DeleteComment(string requester, int commentId)
        {
            Comment comment = await commentRepository.GetCommentById(commentId);
            if(comment == null) throw new CustomException(404, "The target comment to delete was not found.");
            if (comment.Author != requester) throw new CustomException(403, "You can't delete other user's comments.");
            return await commentRepository.DeleteCommentById(commentId);
        }
    }
}
