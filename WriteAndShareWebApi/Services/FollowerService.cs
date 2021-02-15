using WriteAndShareWebApi.Models;
using WriteAndShareWebApi.Enums;
using WriteAndShareWebApi.Exceptions;
using WriteAndShareWebApi.Interfaces;
using WriteAndShareWebApi.Interfaces.Repository;
using WriteAndShareWebApi.Interfaces.Services;
using WriteAndShareWebApi.Models.ResponseModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using WriteAndShareWebApi.Utils;
using Microsoft.AspNetCore.Hosting;

namespace WriteAndShareWebApi.Services
{
    public class FollowerService : IFollowerService
    {
        private readonly IWebHostEnvironment env;
        private readonly IUserRepository userRepository;
        private readonly IFollowerRepository followerRepository;
        private readonly INotificationRepository notificationRepository;

        public FollowerService(IWebHostEnvironment _env, IUserRepository _userRepository, IFollowerRepository _followerRepository, INotificationRepository _notificationRepository)
        {
            env = _env;
            userRepository = _userRepository;
            followerRepository = _followerRepository;
            notificationRepository = _notificationRepository;
        }

        public async Task<List<GetUserBasicInfoResponse>> GetFollowers(string requester)
        {
            List<GetUserBasicInfoResponse> res = new List<GetUserBasicInfoResponse>();
            List<User> users = await followerRepository.GetUserFollowers(requester);

            foreach(User user in users)
            {
                bool areYouHisFollower = await followerRepository.AreYouFollowingHim(requester, user.Username);
                bool didYouRequestToFollow = false;
                if (!areYouHisFollower) didYouRequestToFollow = await followerRepository.DidYouRequestToFollow(requester, user.Username);

                res.Add(new GetUserBasicInfoResponse
                {
                    Avatar = UploadsHandler.GetUserAvatar(env.ContentRootPath, user.AvatarPath),
                    Username = user.Username,
                    Privacy = user.Privacy,
                    IsHeYourFollower = true,
                    AreYouHisFollower = areYouHisFollower,
                    DidYouRequestToFollow = didYouRequestToFollow
                });
            }

            return res;
        }

        public async Task<List<GetUserBasicInfoResponse>> GetFollowing(string requester)
        {
            List<GetUserBasicInfoResponse> res = new List<GetUserBasicInfoResponse>();
            List<User> users = await followerRepository.GetUsersFollowing(requester);

            foreach (User user in users)
            {
                bool isHeYourFollower = await followerRepository.IsHeYourFollower(requester, user.Username);

                res.Add(new GetUserBasicInfoResponse
                {
                    Avatar = UploadsHandler.GetUserAvatar(env.ContentRootPath, user.AvatarPath),
                    Username = user.Username,
                    Privacy = user.Privacy,
                    IsHeYourFollower = isHeYourFollower,
                    AreYouHisFollower = true,
                    DidYouRequestToFollow = false
                });
            }

            return res;
        }

        public async Task<List<GetRequestResponse>> GetRequestsReceived(string requester)
        {
            List<GetRequestResponse> res = new List<GetRequestResponse>();
            List<Request> requests = await followerRepository.GetRequestsToUser(requester);

            foreach(Request request in requests)
            {
                res.Add(new GetRequestResponse
                {
                    Id = request.Id,
                    Requester = request.Requester,
                    Target = request.Target,
                    SubmitDate = request.SubmitDate
                });
            }

            return res;
        }

        public async Task<List<GetRequestResponse>> GetRequestsMade(string requester)
        {
            List<GetRequestResponse> res = new List<GetRequestResponse>();
            List<Request> requests = await followerRepository.GetRequestsMadeByUser(requester);

            foreach (Request request in requests)
            {
                res.Add(new GetRequestResponse
                {
                    Id = request.Id,
                    Requester = request.Requester,
                    Target = request.Target,
                    SubmitDate = request.SubmitDate
                });
            }

            return res;
        }

        public async Task<List<GetUserBasicInfoResponse>> GetUserFollowers(string requester, string role, string target)
        {
            User targetUser = await userRepository.GetUserByUsername(target);
            if (targetUser == null) throw new CustomException(404, "The target user was not found.");

            if (targetUser.Privacy != Privacies.Public && requester != target && role != Roles.Administrator)
                if (!await followerRepository.AreYouFollowingHim(requester, target))
                    throw new CustomException(403, "You're not authorized to see who follows this user.");

            List<GetUserBasicInfoResponse> res = new List<GetUserBasicInfoResponse>();
            List<User> users = await followerRepository.GetUserFollowers(target);

            foreach (User user in users)
            {
                bool isHeYourFollower = await followerRepository.IsHeYourFollower(requester, user.Username);
                bool areYouHisFollower = await followerRepository.AreYouFollowingHim(requester, user.Username);
                bool didYouRequestToFollow = false;
                if (!areYouHisFollower) didYouRequestToFollow = await followerRepository.DidYouRequestToFollow(requester, user.Username);

                res.Add(new GetUserBasicInfoResponse
                {
                    Avatar = UploadsHandler.GetUserAvatar(env.ContentRootPath, user.AvatarPath),
                    Username = user.Username,
                    Privacy = user.Privacy,
                    IsHeYourFollower = isHeYourFollower,
                    AreYouHisFollower = areYouHisFollower,
                    DidYouRequestToFollow = didYouRequestToFollow
                });
            }

            return res;
        }

        public async Task<List<GetUserBasicInfoResponse>> GetUserFollowing(string requester, string role, string target)
        {
            User targetUser = await userRepository.GetUserByUsername(target);
            if (targetUser == null) throw new CustomException(404, "The target user was not found.");

            if (targetUser.Privacy != Privacies.Public && requester != target && role != Roles.Administrator)
                if (!await followerRepository.AreYouFollowingHim(requester, target))
                    throw new CustomException(403, "You're not authorized to see whose this user follows.");

            List<GetUserBasicInfoResponse> res = new List<GetUserBasicInfoResponse>();
            List<User> users = await followerRepository.GetUsersFollowing(target);

            foreach (User user in users)
            {
                bool isHeYourFollower = await followerRepository.IsHeYourFollower(requester, user.Username);
                bool areYouHisFollower = await followerRepository.AreYouFollowingHim(requester, user.Username);
                bool didYouRequestToFollow = false;
                if (!areYouHisFollower) didYouRequestToFollow = await followerRepository.DidYouRequestToFollow(requester, user.Username);

                res.Add(new GetUserBasicInfoResponse
                {
                    Avatar = UploadsHandler.GetUserAvatar(env.ContentRootPath, user.AvatarPath),
                    Username = user.Username,
                    Privacy = user.Privacy,
                    IsHeYourFollower = isHeYourFollower,
                    AreYouHisFollower = areYouHisFollower,
                    DidYouRequestToFollow = didYouRequestToFollow
                });
            }

            return res;
        }

        public async Task<SuccessResponse> FollowUser(string requester, string target)
        {
            if (requester == target) throw new CustomException(400, "You can't follow yourself.");
            User user = await userRepository.GetUserByUsername(target);
            if (user == null) throw new CustomException(404, "The user you want to follow was not found!");
            if (await followerRepository.AreYouFollowingHim(requester, target)) 
                throw new CustomException(400, "You already follow this user.");

            SuccessResponse res;
            if (user.Privacy == Privacies.Private)
            {
                await followerRepository.AddRequestToFollow(requester, target);
                res = new SuccessResponse { Success = "The request to follow was sent with success." };
            }
            else
            {
                await followerRepository.AddAsFollower(requester, target);
                await notificationRepository.AddNotification(
                    new Notification
                    {
                        Type = Notifications.UserFollowed,
                        Author = requester,
                        Target = target,
                        PostId = null,
                        CommentId = null,
                        AnswerId = null
                    });
                res = new SuccessResponse();
            }

            return res;
        }

        public async Task<SuccessResponse> UnfollowUser(string requester, string target)
        {
            if (requester == target) throw new CustomException(400, "You can't unfollow yourself.");
            User user = await userRepository.GetUserByUsername(target);
            if (user == null) throw new CustomException(404, "The user you want to unfollow was not found!");
            if (!await followerRepository.AreYouFollowingHim(requester, target)) 
                throw new CustomException(400, "You can't unfollow a user that you don't even follow in first place.");

            await followerRepository.RemoveAsFollower(requester, target);
            return new SuccessResponse();
        }

        public async Task<SuccessResponse> RemoveUserAsFollower(string requester, string target)
        {
            if (requester == target) throw new CustomException(400, "You can't remove yourself as follower. You can't follow yourself in first place.");
            User user = await userRepository.GetUserByUsername(target);
            if (user == null) throw new CustomException(404, "The user you want to unfollow was not found!");
            if (!await followerRepository.IsHeYourFollower(requester, target)) 
                throw new CustomException(400, "You can't remove as follower an user that doesn't follow you in first place.");

            await followerRepository.RemoveAsFollower(target, requester);
            return new SuccessResponse();
        }

        public async Task<SuccessResponse> CancelRequest(string requester, int requestId)
        {
            if (await followerRepository.GetRequestMadeByUser(requester, requestId) == null) throw new CustomException(404, "Unable to find the target request");
            await followerRepository.RemoveRequest(requestId);
            return new SuccessResponse();
        }

        public async Task<SuccessResponse> AcceptRequest(string requester, int requestId)
        {
            Request request = await followerRepository.GetRequestToUser(requester, requestId);
            if (request == null) throw new CustomException(404, "Unable to find the target request.");
            await followerRepository.AcceptRequest(requestId);
            await notificationRepository.AddNotification(
                    new Notification
                    {
                        Type = Notifications.UserAcceptedRequest,
                        Author = requester,
                        Target = request.Requester,
                        PostId = null,
                        CommentId = null,
                        AnswerId = null
                    });
            return new SuccessResponse();
        }

        public async Task<SuccessResponse> RefuseRequest(string requester, int requestId)
        {
            if (await followerRepository.GetRequestToUser(requester, requestId) == null) throw new CustomException(404, "Unable to find the target request");
            await followerRepository.RemoveRequest(requestId);
            return new SuccessResponse();
        }
    }
}
