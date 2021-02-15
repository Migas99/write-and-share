using System;

namespace WriteAndShareWebApi.Models
{
    public class Request
    {
        public int Id { get; set; }
        public string Requester { get; set; }
        public string Target { get; set; }
        public DateTime SubmitDate { get; set; }
    }
}
