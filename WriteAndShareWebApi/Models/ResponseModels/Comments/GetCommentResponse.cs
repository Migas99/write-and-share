using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WriteAndShareWebApi.Models.ResponseModels.Comments
{
    public class GetCommentResponse
    {
        public int Id { get; set; }
        public int Target { get; set; }
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
