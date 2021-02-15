using System.ComponentModel.DataAnnotations;

namespace WriteAndShareWebApi.Models.RequestModels
{
    public class UserAuthenticationRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
