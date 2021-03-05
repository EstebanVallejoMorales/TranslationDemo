using System;
using System.Collections.Generic;
using System.Text;

namespace Webinar.TranslationDemo.Models
{
    public class ResponseViewModel
    {
        public List<ErrorViewModel> Errors { get; set; }
        public Object Data { get; set; }
        public string Message { get; set; }

        public ResponseViewModel()
        {
            Errors = new List<ErrorViewModel>();
            Data = new object();
            Message = string.Empty;
        }

        public ResponseViewModel(List<ErrorViewModel> errors, object data, string message)
        {
            Errors = errors;
            Data = data;
            Message = message;
        }       
    }
}
