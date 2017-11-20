using System;

using Android.App;
using Android.Content;
using CrossProcessPreferences;

namespace Sample.Core.Droid
{
    public class CrossProcessPreferencesHelper
    {
        public Context Context => Application.Context;

        public string PreferencesName { get; set; } = "crossProcessPreferences";

        private CrossProcessSharedPreferences _preferences;
        private CrossProcessSharedPreferences Preferences
        {
            get
            {
                this._preferences = this._preferences ?? new CrossProcessSharedPreferences(this.Context, this.PreferencesName);
                return this._preferences;
            }
        }

        public void AddOrUpdateValue<T>(string key, T value)
        {
            if (value == null)
            {
                this.Remove(key);
                return;
            }

            var type = typeof(T);
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                type = Nullable.GetUnderlyingType(type);

            if (type == typeof(int))
            {
                this.Preferences.SetPrefInt(key, value as int? ?? 0);
                return;
            }

            if (type == typeof(bool))
            {
                this.Preferences.SetPrefBoolean(key, value as bool? ?? false);
                return;
            }

            if (type == typeof(long))
            {
                this.Preferences.SetPrefLong(key, value as long? ?? 0);
                return;
            }

            if (type == typeof(string))
            {
                this.Preferences.SetPrefString(key, value as string);
                return;
            }

            if (type == typeof(DateTime))
            {
                this.Preferences.SetPrefDateTime(key, value as DateTime? ?? default(DateTime));
                return;
            }

            throw new NotImplementedException($"Not implemented CrossProcessSettingsHandler -> AddOrUpdateValue for type {type.Name}");
        }

        public T GetValueOrDefault<T>(string key, T defaultValue)
        {
            if (!this.Preferences.HasKey<T>(key))
                return defaultValue;

            object returnValue = null;

            var type = typeof(T);
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                type = Nullable.GetUnderlyingType(type);

            if (type == typeof(int))
                returnValue = this.Preferences.GetPrefInt(key, defaultValue as int? ?? 0);
            else if (type == typeof(bool))
                returnValue = this.Preferences.GetPrefBoolean(key, defaultValue as bool? ?? false);
            else if (type == typeof(long))
                returnValue = this.Preferences.GetPrefLong(key, defaultValue as long? ?? 0);
            else if (type == typeof(string))
                returnValue = this.Preferences.GetPrefString(key, defaultValue as string);
            else if (type == typeof(DateTime))
                returnValue = this.Preferences.GetPrefDateTime(key, defaultValue as DateTime? ?? default(DateTime).ToUniversalTime());

            return null != returnValue ? (T)returnValue : defaultValue;
        }

        public void Remove(string key)
        {
            this.Preferences.RemovePreference(key);
        }
    }
}