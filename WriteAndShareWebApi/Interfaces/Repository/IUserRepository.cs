using WriteAndShareWebApi.Models;
using WriteAndShareWebApi.Models.RequestModels;
using WriteAndShareWebApi.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WriteAndShareWebApi.Interfaces
{
    public interface IUserRepository
    {
        Task<User> AddUser(User user);
        Task<User> UpdateUser(User user);
        Task<SuccessResponse> DeleteUserByUsername(string username);
        Task<User> UpdateUserHeader(string username, string headerPath);
        Task<User> UpdateUserAvatar(string username, string avatarPath);
        Task<List<User>> GetAllUsers();
        Task<List<User>> GetListOfUsersByUsernames(List<string> usernames);
        Task<User> GetUserByUsername(string username);
        Task<User> GetUserByEmail(string email);
    }
}
