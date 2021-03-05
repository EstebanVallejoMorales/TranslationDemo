using System;
using System.Collections.Generic;
using System.Text;

namespace Webinar.TranslationDemo.Domain.Entities
{
    public class Error
    {
        public string Code { get; set; }
        public string Message { get; set; }

        public Error(string code, string message)
        {
            Code = code;
            Message = message;
        }

        public Error()
        {
            Code = string.Empty;
            Message = string.Empty;
        }
    }
}
