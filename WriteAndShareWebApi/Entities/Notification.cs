using System;

namespace WriteAndShareWebApi.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Author { get; set; }
        public string Target { get; set; }
        public string PostId { get; set; }
        public string CommentId { get; set; }
        public string AnswerId { get; set; }
        public DateTime SubmitDate { get; set; }
        public bool AlreadySeen { get; set; }
    }
}
