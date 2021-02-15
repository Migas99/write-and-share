using System;
using System.Collections.Generic;

namespace WriteAndShareWebApi.Models.ResponseModels.Posts
{
    public class GetPostResponse
    {
        public int Id { get; set; }
        public byte[] Upload { get; set; }
        public string ContentType { get; set; }
        public string Link { get; set; }
        public string Message { get; set; }
        public string Author { get; set; }
        public DateTime SubmitDate { get; set; }
        public List<string> Upvoted { get; set; }
        public List<string> Downvoted { get; set; }
        public int Score { get; set; }
        public List<string> Mentions { get; set; }
        public int CommentsNumber { get; set; }
        public bool DidYouUpvote { get; set; }
        public bool DidYouDownvote { get; set; }
    }
}
