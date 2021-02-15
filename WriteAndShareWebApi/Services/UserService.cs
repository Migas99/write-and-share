using WriteAndShareWebApi.Enums;
using WriteAndShareWebApi.Exceptions;
using WriteAndShareWebApi.Interfaces;
using WriteAndShareWebApi.Interfaces.Repository;
using WriteAndShareWebApi.Models;
using WriteAndShareWebApi.Models.RequestModels;
using WriteAndShareWebApi.Models.ResponseModels;
using WriteAndShareWebApi.Utils;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using static WriteAndShareWebApi.Enums.Uploads;
using static WriteAndShareWebApi.Utils.UploadsHandler;
using Microsoft.Extensions.Configuration;
using System.IO;
using WriteAndShareWebApi.Models.ResponseModels.Users;

namespace WriteAndShareWebApi.Services
{
    public class UserService : IUserService
    {
        private readonly IConfiguration config;
        private readonly IWebHostEnvironment env;
        private readonly IUserRepository userRepository;
        private readonly IFollowerRepository followerRepository;

        public UserService(IConfiguration _config, IWebHostEnvironment _env, 
            IUserRepository _userRepository, IFollowerRepository _followerRepository)
        {
            config = _config;
            env = _env;
            userRepository = _userRepository;
            followerRepository = _followerRepository;
        } 

        public async Task<List<GetUserBasicInfoResponse>> GetAllUsersBasicInfo(string requester)
        {
            List<GetUserBasicInfoResponse> res = new List<GetUserBasicInfoResponse>();
            List<User> users = await userRepository.GetAllUsers();
            foreach(User user in users)
            {
                if(user.Privacy != Privacies.Desactivated)
                {
                    bool isHeYourFollower = await followerRepository.IsHeYourFollower(requester, user.Username);
                    bool areYouHisFollower = await followerRepository.AreYouFollowingHim(requester, user.Username);
                    bool didYouRequestToFollow = false;
                    if (!areYouHisFollower) didYouRequestToFollow = await followerRepository.DidYouRequestToFollow(requester, user.Username);

                    res.Add(new GetUserBasicInfoResponse
                    {
                        Avatar = GetUserAvatar(env.ContentRootPath, user.AvatarPath),
                        Username = user.Username,
                        Privacy = user.Privacy,
                        IsHeYourFollower = isHeYourFollower,
                        AreYouHisFollower = areYouHisFollower,
                        DidYouRequestToFollow = didYouRequestToFollow
                    });
                }
            }

            return res;
        }

        public async Task<List<GetUserBasicInfoResponse>> GetListOfUsersBasicInfoByUsername(string requester, List<string> usernames)
        {
            List<GetUserBasicInfoResponse> res = new List<GetUserBasicInfoResponse>();
            List<User> users = await userRepository.GetListOfUsersByUsernames(usernames);
            foreach (User user in users)
            {
                if(user.Privacy != Privacies.Desactivated)
                {
                    bool isHeYourFollower = await followerRepository.IsHeYourFollower(requester, user.Username);
                    bool areYouHisFollower = await followerRepository.AreYouFollowingHim(requester, user.Username);
                    bool didYouRequestToFollow = false;
                    if (!areYouHisFollower) didYouRequestToFollow = await followerRepository.DidYouRequestToFollow(requester, user.Username);

                    res.Add(new GetUserBasicInfoResponse
                    {
                        Avatar = GetUserAvatar(env.ContentRootPath, user.AvatarPath),
                        Username = user.Username,
                        Privacy = user.Privacy,
                        IsHeYourFollower = isHeYourFollower,
                        AreYouHisFollower = areYouHisFollower,
                        DidYouRequestToFollow = didYouRequestToFollow
                    });
                }
            }

            return res;
        }

        public async Task<GetUserProfileBasicInfoResponse> GetUserBasicInfo(string requester, string target)
        {
            User user = await userRepository.GetUserByUsername(target);
            if (user == null) throw new CustomException(404, "The user was not found.");
            if (user.Privacy == Privacies.Desactivated) throw new CustomException(400, "This user desactivated his account.");
            bool isHeYourFollower = await followerRepository.IsHeYourFollower(requester, user.Username);
            bool didYouRequestToFollow = await followerRepository.DidYouRequestToFollow(requester, user.Username);

            return new GetUserProfileBasicInfoResponse
            {
                Header = GetUserHeader(env.ContentRootPath, user.HeaderPath),
                Avatar = GetUserAvatar(env.ContentRootPath, user.AvatarPath),
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Privacy = user.Privacy,
                IsHeYourFollower = isHeYourFollower,
                AreYouHisFollower = false,
                DidYouRequestToFollow = didYouRequestToFollow
            };
        }

        public async Task<GetUserResponse> GetUserByUsername(string requester, string role, string target)
        {
            User user = await userRepository.GetUserByUsername(target);
            if (user == null) throw new CustomException(404, "The user was not found.");
            if (user.Privacy == Privacies.Desactivated) throw new CustomException(400, "This user desactivated his account.");

            if (requester != user.Username && user.Privacy == Privacies.Private && role != Roles.Administrator)
                if (!await followerRepository.AreYouFollowingHim(requester, target)) 
                    throw new CustomException(403, "You're not authorized to see this profile.");

            bool isHeYourFollower = await followerRepository.IsHeYourFollower(requester, user.Username);
            bool areYouHisFollower = await followerRepository.AreYouFollowingHim(requester, user.Username);
            bool didYouRequestToFollow = false;
            if (!areYouHisFollower) didYouRequestToFollow = await followerRepository.DidYouRequestToFollow(requester, user.Username);

            return new GetUserResponse
            {
                Header = GetUserHeader(env.ContentRootPath, user.HeaderPath),
                Avatar = GetUserAvatar(env.ContentRootPath, user.AvatarPath),
                Username = user.Username,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Gender = user.Gender,
                BirthDate = user.BirthDate,
                Telephone = user.Telephone,
                Address = user.Address,
                Privacy = user.Privacy,
                IsHeYourFollower = isHeYourFollower,
                AreYouHisFollower = areYouHisFollower,
                DidYouRequestToFollow = didYouRequestToFollow
            };
        }

        public async Task<GetUserResponse> GetProfile(string requester)
        {
            User user = await userRepository.GetUserByUsername(requester);
            if (user.Privacy == Privacies.Desactivated) throw new CustomException(400, "This account is desactivated.");

            return new GetUserResponse
            {
                Header = GetUserHeader(env.ContentRootPath, user.HeaderPath),
                Avatar = GetUserAvatar(env.ContentRootPath, user.AvatarPath),
                Username = user.Username,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Gender = user.Gender,
                BirthDate = user.BirthDate,
                Telephone = user.Telephone,
                Address = user.Address,
                Privacy = user.Privacy,
                AreYouHisFollower = false,
                IsHeYourFollower = false,
                DidYouRequestToFollow = false
            };
        }

        public async Task<SuccessResponse> UpdateHeader(string requester, IFormFile header)
        {
            User user = await userRepository.GetUserByUsername(requester);
            if (user.Privacy == Privacies.Desactivated) throw new CustomException(400, "This account is desactivated.");
            string updatedHeader;
            if(header == null)
            {
                string relativePath = Path.Combine(env.ContentRootPath, GetHeaderFolderPath());
                string previousHeader = Path.Combine(relativePath, user.HeaderPath);
                if (user.HeaderPath != DefaultHeader && File.Exists(previousHeader)) File.Delete(previousHeader);
                updatedHeader = DefaultHeader;
            }
            else
            {
                updatedHeader = await
                SaveUserHeader(config["SightEngine:api_user"], config["SightEngine:api_secret"], env.ContentRootPath, user.HeaderPath, header);
            }

            await userRepository.UpdateUserHeader(requester, updatedHeader);
            return new SuccessResponse();
        }

        public async Task<SuccessResponse> UpdateAvatar(string requester, IFormFile avatar)
        {
            User user = await userRepository.GetUserByUsername(requester);
            if (user.Privacy == Privacies.Desactivated) throw new CustomException(400, "This account is desactivated.");
            string updatedAvatar;
            if(avatar == null)
            {
                string relativePath = Path.Combine(env.ContentRootPath, GetAvatarFolderPath());
                string previousAvatar = Path.Combine(relativePath, user.AvatarPath);
                if (user.AvatarPath != DefaultAvatar && File.Exists(previousAvatar)) File.Delete(previousAvatar);
                updatedAvatar = DefaultAvatar;
            }
            else
            {
                updatedAvatar = await
                SaveUserAvatar(config["SightEngine:api_user"], config["SightEngine:api_secret"], env.ContentRootPath, user.AvatarPath, avatar);
            }

            await userRepository.UpdateUserAvatar(requester, updatedAvatar);
            return new SuccessResponse();
        }

        public async Task<GetUserResponse> UpdateUser(string requester, UpdateUserRequest update)
        {
            User user = await userRepository.GetUserByUsername(requester);
            if (user.Privacy == Privacies.Desactivated) throw new CustomException(400, "This account is desactivated.");
            if (!HashPassword.Verify(update.CurrentPassword, user.HashedPassword)) throw new CustomException(403, "Wrong current password.");

            string errors = "";
            if (update.NewPassword.Length < 6) errors += "The new password is too short. Minimum 6 characters.\n";
            if (user.Email != update.Email && await userRepository.GetUserByEmail(update.Email) != null) errors += "The new email is already in use.\n";
            if (!Privacies.IsPrivacyValid(update.Privacy)) errors += "Not a valid value for privacy.\n";
            if (errors != "") throw new CustomException(400, errors);

            user.HashedPassword = HashPassword.Hash(update.NewPassword);
            user.Email = update.Email;
            user.Telephone = update.Telephone;
            user.Address = update.Address;
            user.Privacy = update.Privacy;

            User updatedUser = await userRepository.UpdateUser(user);
            return new GetUserResponse
            {
                Header = GetUserHeader(env.ContentRootPath, updatedUser.HeaderPath),
                Avatar = GetUserAvatar(env.ContentRootPath, updatedUser.AvatarPath),
                Username = updatedUser.Username,
                Email = updatedUser.Email,
                FirstName = updatedUser.FirstName,
                LastName = updatedUser.LastName,
                Gender = updatedUser.Gender,
                BirthDate = updatedUser.BirthDate,
                Telephone = updatedUser.Telephone,
                Address = updatedUser.Address,
                Privacy = updatedUser.Privacy,
                AreYouHisFollower = false,
                IsHeYourFollower = false,
                DidYouRequestToFollow = false
            };
        }

        public async Task<SuccessResponse> DeleteUser(string requester)
        {
            User user = await userRepository.GetUserByUsername(requester);
            if (user.Privacy == Privacies.Desactivated) throw new CustomException(400, "This account is already desactivated.");
            return await userRepository.DeleteUserByUsername(requester);
        }
    }
}
