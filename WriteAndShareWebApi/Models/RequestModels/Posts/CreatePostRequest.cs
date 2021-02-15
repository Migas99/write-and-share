using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace WriteAndShareWebApi.Models.RequestModels
{
    public class CreatePostRequest
    {
        public IFormFile Upload { get; set; }
        public string Link { get; set; }
        public string Message { get; set; }
        public string Mentions { get; set; }
    }
}
