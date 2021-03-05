using System;
using System.Collections.Generic;
using System.Text;

namespace Webinar.TranslationDemo.Infrastructure.Common.Enumerators
{
    public static class EnumHelper
    {
        public static T Parse<T>(string input)
        {
            return (T)Enum.Parse(typeof(T), input, false);
        }

        public static bool ExistsValue<T>(object input)
        {
            return Enum.IsDefined(typeof(T), input);
        }
    }
}
