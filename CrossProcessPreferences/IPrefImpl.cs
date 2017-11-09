using System;

namespace CrossProcessPreferences
{
    public interface IPrefImpl
    {
        String GetPrefString(String key, String defaultValue);

        void SetPrefString(String key, String value);

		bool GetPrefBoolean(String key, bool defaultValue);

        void SetPrefBoolean(String key, bool value);

        void SetPrefInt(String key, int value);

        int GetPrefInt(String key, int defaultValue);

        void SetPrefFloat(String key, float value);

        float GetPrefFloat(String key, float defaultValue);

		void SetPrefLong(String key, long value);

        long GetPrefLong(String key, long defaultValue);

        void RemovePreference(String key);

        bool HasKey(String key);
    }
}
