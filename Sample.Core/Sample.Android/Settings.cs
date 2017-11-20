using System;

namespace Sample.Core.Droid
{
    public class Settings
    {
        private Lazy<CrossProcessPreferencesHelper> helper = new Lazy<CrossProcessPreferencesHelper>(() => new CrossProcessPreferencesHelper());

        private static Lazy<Settings> _instance = new Lazy<Settings>(() => new Settings());
        public static Settings Instance => _instance.Value;

        private Settings()
        {
        }

        private const string KEY = "key";
        private const string TIMES_CHANGED = "timesChanged";

        public string MyValue
        {
            get => this.helper.Value.GetValueOrDefault<string>(KEY, "default");
            set => this.helper.Value.AddOrUpdateValue<string>(KEY, value);
        }

        public int TimesChangedMyValue
        {
            get => this.helper.Value.GetValueOrDefault<int>(TIMES_CHANGED, 0);
            set => this.helper.Value.AddOrUpdateValue<int>(TIMES_CHANGED, value);
        }
    }
}
