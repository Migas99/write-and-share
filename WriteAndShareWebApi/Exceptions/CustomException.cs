using System;
using System.Collections.Generic;

namespace WriteAndShareWebApi.Exceptions
{
    public class CustomException : Exception
    {
        private readonly int statusCode;
        private readonly List<string> errors;

        public CustomException(int _statusCode, string _error)
        {
            statusCode = _statusCode;
            errors = new List<string>
            {
                _error
            };
        }

        public CustomException(int _statusCode, List<string> _errors)
        {
            statusCode = _statusCode;
            errors = _errors;
        }

        public int GetStatusCode()
        {
            return statusCode;
        }

        public List<string> GetErrors()
        {
            return errors;
        }
    }
}
