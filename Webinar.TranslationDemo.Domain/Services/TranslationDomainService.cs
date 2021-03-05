using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Webinar.TranslationDemo.Domain.Entities;
using Webinar.TranslationDemo.Domain.Repositories;

namespace Webinar.TranslationDemo.Domain.Services
{
    public class TranslationDomainService : ITranslationDomainService
    {
        private readonly ITermTranslationRepository TermTranslationRepository;

        public TranslationDomainService(ITermTranslationRepository termTranslationRepository)
        {
            TermTranslationRepository = termTranslationRepository;
        }

        public string GetTranslationTerm(string label)
        {
            try
            {
                string culture = Thread.CurrentThread.CurrentCulture.ToString();

                List<TermTranslation> termTranslations = TermTranslationRepository.GetTranslations();

                List<TermTranslation> termList = termTranslations.Where(t => t.Label == $"{culture}#{label}").ToList();
                string translationTerm = null;

                if (termList.Count > 0)
                {
                    translationTerm = termTranslations.Where(t => t.Label == $"{culture}#{label}").ToList()[0].Message;
                }
                else
                {
                    translationTerm = termTranslations.Where(t => t.Label == $"es-CO#{label}").ToList()[0].Message;
                }
                return translationTerm;
            }
            catch (Exception ex)
            {
                Console.Write($"TranslationDemo TranslationDomainService GetTranslationTerm, Error: {ex.ToString()}");
            }
            return label;
        }
    }
}
