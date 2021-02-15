using WriteAndShareWebApi.Enums;
using WriteAndShareWebApi.Exceptions;
using WriteAndShareWebApi.Interfaces;
using WriteAndShareWebApi.Models;
using WriteAndShareWebApi.Models.RequestModels;
using WriteAndShareWebApi.Models.ResponseModels;
using WriteAndShareWebApi.Utils;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using static WriteAndShareWebApi.Enums.Uploads;

namespace WriteAndShareWebApi.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConfiguration configuration;
        private readonly IUserRepository userRepository;

        public AuthenticationService(IConfiguration _configuration, IUserRepository _userRepository)
        {
            configuration = _configuration;
            userRepository = _userRepository;
        }

        public async Task<UserAuthenticationResponse> Register(UserRegisterRequest req)
        {
            List<string> errors = new List<string>();
            if (await userRepository.GetUserByUsername(req.Username) != null) errors.Add("The username is already in use.");
            if (req.Username.Length < 6) errors.Add("The username is too short. Minimum 6 characters.");
            if (req.Username.Any(ch => !char.IsLetterOrDigit(ch))) errors.Add("The username can't contain special characters.");
            if (req.Password.Length < 6) errors.Add("The password is too short. Minimum 6 characters.");
            if (await userRepository.GetUserByEmail(req.Email) != null) errors.Add("The email is already in use.");
            if (!Genders.IsGenderValid(req.Gender)) errors.Add("Not a valid value for gender.");
            if (!Privacies.IsPrivacyValid(req.Privacy)) errors.Add("Not a valid value for privacy.");
            if (errors.Count > 0) throw new CustomException(400, errors);

            User user = new User
            {
                HeaderPath = DefaultHeader,
                AvatarPath = DefaultAvatar,
                Username = req.Username,
                HashedPassword = HashPassword.Hash(req.Password),
                Email = req.Email,
                FirstName = req.FirstName,
                LastName = req.LastName,
                Gender = req.Gender,
                BirthDate = req.BirthDate,
                Telephone = req.Telephone,
                Address = req.Address,
                Role = Roles.User,
                Privacy = req.Privacy
            };

            await userRepository.AddUser(user);
            return new UserAuthenticationResponse
            {
                Token = JwtHandler.GenerateJwtToken(user, configuration["JwtSettings:Secret"])
            };
        }

        public async Task<UserAuthenticationResponse> Authenticate(UserAuthenticationRequest req)
        {
            User user = await userRepository.GetUserByUsername(req.Username);
            if (user == null) throw new CustomException(404, "There is no user with such username.");
            if (!HashPassword.Verify(req.Password, user.HashedPassword)) throw new CustomException(400, "Wrong password.");
            if (user.Privacy == Privacies.Desactivated) throw new CustomException(400, "This account is desactivated.");

            return new UserAuthenticationResponse
            {
                Token = JwtHandler.GenerateJwtToken(user, configuration["JwtSettings:Secret"])
            };
        }
    }
}
