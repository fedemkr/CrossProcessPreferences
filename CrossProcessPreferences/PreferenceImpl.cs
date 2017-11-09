using Android.Content;

namespace CrossProcessPreferences
{
    public class PreferenceImpl : IPrefImpl
    {
        private readonly Context context;

        private readonly string prefName;

        public PreferenceImpl(Context context, string prefName)
        {
            this.context = context;
            this.prefName = prefName;
        }

        private ISharedPreferences Settings => this.context.GetSharedPreferences(this.prefName, FileCreationMode.Private);

        public bool GetPrefBoolean(string key, bool defaultValue)
        {
            return this.Settings.GetBoolean(key, defaultValue);
        }

        public float GetPrefFloat(string key, float defaultValue)
        {
            return this.Settings.GetFloat(key, defaultValue);
        }

        public int GetPrefInt(string key, int defaultValue)
        {
            return this.Settings.GetInt(key, defaultValue);
        }

        public long GetPrefLong(string key, long defaultValue)
        {
            return this.Settings.GetLong(key, defaultValue);
        }

        public string GetPrefString(string key, string defaultValue)
        {
            return this.Settings.GetString(key, defaultValue);
        }

        public bool HasKey(string key)
        {
            return this.Settings.Contains(key);
        }

        public void RemovePreference(string key)
        {
            this.Settings.Edit().Remove(key).Apply();
        }

        public void SetPrefBoolean(string key, bool value)
        {
            this.Settings.Edit().PutBoolean(key, value).Apply();
        }

        public void SetPrefFloat(string key, float value)
        {
            this.Settings.Edit().PutFloat(key, value).Apply();
        }

        public void SetPrefInt(string key, int value)
        {
            this.Settings.Edit().PutInt(key, value).Apply();
        }

        public void SetPrefLong(string key, long value)
        {
            this.Settings.Edit().PutLong(key, value).Apply();
        }

        public void SetPrefString(string key, string value)
        {
            this.Settings.Edit().PutString(key, value).Apply();
        }

        public void ClearPreference(ISharedPreferences p)
        {
            var editor = p.Edit();
            editor.Clear();
            editor.Apply();
        }
    }
}
