using WriteAndShareWebApi.Models.RequestModels;
using WriteAndShareWebApi.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WriteAndShareWebApi.Interfaces
{
    public interface IAuthenticationService
    {
        Task<UserAuthenticationResponse> Register(UserRegisterRequest user);
        Task<UserAuthenticationResponse> Authenticate(UserAuthenticationRequest user);
    }
}
