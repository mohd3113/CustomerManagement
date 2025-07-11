using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManagement.Application.Exceptions
{
    public class ValidationException
    {
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public ValidationException(string message, List<string> errors)
        {
            Message = message;
            Errors = errors ?? new List<string>();
        }
        public ValidationException(string message) : this(message, new List<string>())
        {
        }
    }
}
