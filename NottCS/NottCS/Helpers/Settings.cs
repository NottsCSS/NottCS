using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace NottCS.Helpers
{
    public static class Settings
    {
        private static ISettings AppSettings => CrossSettings.Current;
        
        public static string RefreshToken
        {
            get => AppSettings.GetValueOrDefault(nameof(RefreshToken), string.Empty);
            set => AppSettings.AddOrUpdateValue(nameof(RefreshToken), value);
        }

        }
}

