using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WriteAndShareWebApi.Models.ResponseModels
{
    public class ErrorResponse
    {
        public List<string> Errors { get; set; }

        public ErrorResponse()
        {
            Errors = new List<string>
            {
                "Ups! Something went wrong! Try again later!"
            };
        }
    }
}
