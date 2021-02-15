using System.ComponentModel.DataAnnotations;

namespace WriteAndShareWebApi.Models.RequestModels
{
    public class UpdateUserRequest
    {
        [Required]
        public string CurrentPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Telephone { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Privacy { get; set; }
    }
}
