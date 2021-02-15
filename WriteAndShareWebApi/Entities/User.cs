using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WriteAndShareWebApi.Models
{
    public class User
    {
        public string HeaderPath { get; set; }
        public string AvatarPath { get; set; }
        public string Username { get; set; }
        public string HashedPassword { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string BirthDate { get; set; }
        public string Telephone { get; set; }
        public string Address { get; set; }
        public string Privacy { get; set; }
        public string Role { get; set; }
    }
}
