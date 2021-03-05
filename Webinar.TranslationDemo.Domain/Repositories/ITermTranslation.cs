using System;
using System.Collections.Generic;
using System.Text;
using Webinar.TranslationDemo.Domain.Entities;

namespace Webinar.TranslationDemo.Domain.Repositories
{
    public interface ITermTranslationRepository 
    {
        List<TermTranslation> GetTranslations();
    }
}
