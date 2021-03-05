using System;
using System.Collections.Generic;
using System.Text;

namespace Webinar.TranslationDemo.Domain.Services
{
    public interface ITranslationDomainService
    {
        string GetTranslationTerm(string label);
    }
}
