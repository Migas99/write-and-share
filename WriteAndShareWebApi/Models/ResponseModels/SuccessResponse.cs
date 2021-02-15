using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WriteAndShareWebApi.Models.ResponseModels
{
    public class SuccessResponse
    {
        public string Success { get; set; }

        public SuccessResponse()
        {
            Success = "The operation was successfully done.";
        }
    }
}
