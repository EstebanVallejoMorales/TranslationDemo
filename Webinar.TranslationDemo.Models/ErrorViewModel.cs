using System;
using System.Collections.Generic;
using System.Text;

namespace Webinar.TranslationDemo.Models
{
    public class ErrorViewModel
    {
        public string Code { get; set; }
        public string Message { get; set; }

        public ErrorViewModel(string code, string message)
        {
            this.Code = code;
            this.Message = message;
        }

        public ErrorViewModel()
        {
            this.Code = string.Empty;
            this.Message = string.Empty;
        }
    }
}
