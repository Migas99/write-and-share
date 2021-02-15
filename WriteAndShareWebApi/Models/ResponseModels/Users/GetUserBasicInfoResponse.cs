namespace WriteAndShareWebApi.Models.ResponseModels
{
    public class GetUserBasicInfoResponse
    {
        public byte[] Avatar { get; set; }
        public string Username { get; set; }
        public string Privacy { get; set; }
        public bool IsHeYourFollower { get; set; }
        public bool AreYouHisFollower { get; set; }
        public bool DidYouRequestToFollow { get; set; }
    }
}
