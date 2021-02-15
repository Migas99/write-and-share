using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WriteAndShareWebApi.Models.ResponseModels.Users
{
    public class GetUserProfileBasicInfoResponse
    {
        public byte[] Header { get; set; }
        public byte[] Avatar { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Privacy { get; set; }
        public bool IsHeYourFollower { get; set; }
        public bool AreYouHisFollower { get; set; }
        public bool DidYouRequestToFollow { get; set; }
    }
}
