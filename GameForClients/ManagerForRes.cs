using System;
using System.Globalization;
using System.Threading;

namespace GameForClients.Localization
{
    public static class ManagerForRes
    {
        public static event Action LanguageChanged;

        public static void SetLanguage(string languageCode)
        {
            if (string.IsNullOrEmpty(languageCode))
                return;

            var culture = new CultureInfo(languageCode);

            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            LanguageChanged?.Invoke(); 
        }

        public static string CurrentLanguage => Thread.CurrentThread.CurrentUICulture.Name;
    }
}