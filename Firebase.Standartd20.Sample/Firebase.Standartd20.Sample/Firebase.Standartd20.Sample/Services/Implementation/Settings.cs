using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Firebase.Standartd20.Sample.Services
{
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        public static string FirebaseAuthJson
        {
            get => AppSettings.GetValueOrDefault(nameof(FirebaseAuthJson), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(FirebaseAuthJson), value);
        }
    }
}
