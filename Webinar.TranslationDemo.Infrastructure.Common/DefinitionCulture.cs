using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Webinar.TranslationDemo.Infrastructure.Common
{
    public class DefinitionCulture
    {
        protected DefinitionCulture() { }

        private static List<CultureInfo> supportedCultures = new List<CultureInfo>
    {
        new CultureInfo("es-CO"),
        new CultureInfo("en-US"),
        new CultureInfo("fr-FR")
    };

        public static CultureInfo GetCulture(string culture)
        {
            var supportCulture = supportedCultures.Find(c => c.Name.Equals(culture));
            return supportCulture ?? supportedCultures.First();
        }

        public static List<CultureInfo> DefineCulture()
        {
            foreach (var culture in supportedCultures)
            {
                culture.NumberFormat.CurrencyDecimalSeparator = ",";
                culture.NumberFormat.CurrencyGroupSeparator = ".";
                culture.NumberFormat.NumberDecimalSeparator = ",";
                culture.NumberFormat.NumberGroupSeparator = ".";
                culture.NumberFormat.PercentDecimalSeparator = ",";
                culture.NumberFormat.PercentGroupSeparator = ".";
            }

            return supportedCultures;
        }
    }
}
