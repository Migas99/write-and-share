using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WriteAndShareWebApi.Models.RequestModels.Comments
{
    public class CreateCommentRequest
    {
        [Required]
        public int Target { get; set; }
        [Required]
        public string Message { get; set; }
        public List<string> Mentions { get; set; }
    }
}
