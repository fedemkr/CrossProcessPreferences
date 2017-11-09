using System;
using Android.Content;

namespace CrossProcessPreferences
{
    public class CrossProcessSharedPreferences
    {
        private readonly Context context;

        private readonly string preferenceFileName;

        public CrossProcessSharedPreferences(Context context, string preferenceFileName)
        {
            this.context = context;
            this.preferenceFileName = preferenceFileName;
        }

        public string GetPrefString(string key, string defaultValue)
        {
            return PrefAccessor.GetString(this.context, this.preferenceFileName, key, defaultValue);
        }

        public void SetPrefString(string key, string value)
        {
            PrefAccessor.SetString(this.context, this.preferenceFileName, key, value);
        }

        public bool GetPrefBoolean(string key, bool defaultValue)
        {
            return PrefAccessor.GetBoolean(this.context, this.preferenceFileName, key, defaultValue);
        }

        public void SetPrefBoolean(string key, bool value)
        {
            PrefAccessor.SetBoolean(this.context, this.preferenceFileName, key, value);
        }

        public void SetPrefInt(string key, int value)
        {
            PrefAccessor.SetInt(this.context, this.preferenceFileName, key, value);
        }

        public int GetPrefInt(string key, int defaultValue)
        {
            return PrefAccessor.GetInt(this.context, this.preferenceFileName, key, defaultValue);
        }

        public void SetPrefLong(string key, long value)
        {
            PrefAccessor.SetLong(this.context, this.preferenceFileName, key, value);
        }

        public long GetPrefLong(string key, long defaultValue)
        {
            return PrefAccessor.GetLong(this.context, this.preferenceFileName, key, defaultValue);
        }

        public void SetPrefDateTime(string key, DateTime value)
        {
            PrefAccessor.SetDateTime(this.context, this.preferenceFileName, key, value);
        }

        public DateTime GetPrefDateTime(string key, DateTime defaultValue)
        {
            return PrefAccessor.GetDateTime(this.context, this.preferenceFileName, key, defaultValue);
        }

        public bool HasKey<T>(string key)
        {
            return PrefAccessor.HasKey<T>(this.context, key, this.preferenceFileName);
        }

        public void RemovePreference(string key)
        {
            PrefAccessor.Remove(this.context, this.preferenceFileName, key);
        }
    }
}
