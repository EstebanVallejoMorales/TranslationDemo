using System;
using System.Collections.Generic;
using System.Text;

namespace Webinar.TranslationDemo.Domain.Entities
{
    public class Response
    {
        public List<Error> Errors { get; set; }
        public Object Data { get; set; }
        public string Message { get; set; }

        public Response()
        {
            Errors = new List<Error>();
            Data = new object();
            Message = string.Empty;
        }
    }
}
