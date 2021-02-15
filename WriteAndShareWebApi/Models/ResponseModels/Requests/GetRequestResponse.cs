using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WriteAndShareWebApi.Models.ResponseModels
{
    public class GetRequestResponse
    {
        public int Id { get; set; }
        public string Requester { get; set; }
        public string Target { get; set; }
        public DateTime SubmitDate { get; set; }
    }
}
