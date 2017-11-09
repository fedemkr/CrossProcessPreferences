using System;
using Android.Content;
using Android.Database;

namespace CrossProcessPreferences
{
    public class PrefAccessor
    {
        public static String GetString(Context context, String name, String key, String defaultValue)
        {
            Android.Net.Uri URI = PreferenceProvider.BuildUri(name, key, PreferenceProvider.PREF_STRING);
            String value = defaultValue;
            ICursor cursor = context.ContentResolver.Query(URI, null, null, null, null);
            if (cursor != null && cursor.MoveToFirst())
            {
                value = cursor.GetString(cursor.GetColumnIndex(PreferenceProvider.PREF_VALUE));
            }
            IOUtils.CloseQuietly(cursor);
            return value;
        }

        public static int GetInt(Context context, String name, String key, int defaultValue)
        {
            Android.Net.Uri URI = PreferenceProvider.BuildUri(name, key, PreferenceProvider.PREF_INT);
            int value = defaultValue;
            ICursor cursor = context.ContentResolver.Query(URI, null, null, null, null);
            if (cursor != null && cursor.MoveToFirst())
            {
                value = cursor.GetInt(cursor.GetColumnIndex(PreferenceProvider.PREF_VALUE));
            }
            IOUtils.CloseQuietly(cursor);
            return value;
        }

        public static long GetLong(Context context, String name, String key, long defaultValue)
        {
            Android.Net.Uri URI = PreferenceProvider.BuildUri(name, key, PreferenceProvider.PREF_LONG);
            long value = defaultValue;
            ICursor cursor = context.ContentResolver.Query(URI, null, null, null, null);
            if (cursor != null && cursor.MoveToFirst())
            {
                value = cursor.GetLong(cursor.GetColumnIndex(PreferenceProvider.PREF_VALUE));
            }
            IOUtils.CloseQuietly(cursor);
            return value;
        }

        public static DateTime GetDateTime(Context context, String name, String key, DateTime defaultValue)
        {
            Android.Net.Uri URI = PreferenceProvider.BuildUri(name, key, PreferenceProvider.PREF_LONG);
            DateTime value = defaultValue;
            ICursor cursor = context.ContentResolver.Query(URI, null, null, null, null);
            if (cursor != null && cursor.MoveToFirst())
            {
                var ticks = cursor.GetLong(cursor.GetColumnIndex(PreferenceProvider.PREF_VALUE));
                value = new DateTime(ticks, DateTimeKind.Utc);
            }
            IOUtils.CloseQuietly(cursor);
            return value;
        }

        public static bool HasKey<T>(Context context, string key, string name)
        {
            var type = typeof(T);
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                type = Nullable.GetUnderlyingType(type);

            int pref;
            if (type == typeof(bool))
                pref = PreferenceProvider.PREF_BOOLEAN;
            else if (type == typeof(string))
                pref = PreferenceProvider.PREF_STRING;
            else if (type == typeof(int))
                pref = PreferenceProvider.PREF_INT;
            else if (type == typeof(long))
                pref = PreferenceProvider.PREF_LONG;
            else if (type == typeof(DateTime))
                pref = PreferenceProvider.PREF_DATETIME;
            else
                throw new NotImplementedException($"There is no PREF for the type {type.Name}");

            Android.Net.Uri URI = PreferenceProvider.BuildUri(name, key, pref);
            return context.ContentResolver.Query(URI, null, null, null, null) != null;
        }

        public static bool GetBoolean(Context context, String name, String key, bool defaultValue)
        {
            Android.Net.Uri URI = PreferenceProvider.BuildUri(name, key, PreferenceProvider.PREF_BOOLEAN);
            int value = defaultValue ? 1 : 0;
            ICursor cursor = context.ContentResolver.Query(URI, null, null, null, null);
            if (cursor != null && cursor.MoveToFirst())
            {
                var cursorValue = cursor.GetString(cursor.GetColumnIndex(PreferenceProvider.PREF_VALUE));
                value = cursorValue == "true" ? 1 : 0;
            }
            IOUtils.CloseQuietly(cursor);
            return value == 1;
        }

        public static void Remove(Context context, String name, String key)
        {
            Android.Net.Uri URI = PreferenceProvider.BuildUri(name, key, PreferenceProvider.PREF_STRING);
            context.ContentResolver.Delete(URI, null, null);
        }

        public static void SetString(Context context, String name, String key, String value)
        {
            Android.Net.Uri URI = PreferenceProvider.BuildUri(name, key, PreferenceProvider.PREF_STRING);
            ContentValues cv = new ContentValues();
            cv.Put(PreferenceProvider.PREF_KEY, key);
            cv.Put(PreferenceProvider.PREF_VALUE, value);
            context.ContentResolver.Update(URI, cv, null, null);
        }

        public static void SetBoolean(Context context, String name, String key, bool value)
        {
            Android.Net.Uri URI = PreferenceProvider.BuildUri(name, key, PreferenceProvider.PREF_BOOLEAN);
            ContentValues cv = new ContentValues();
            cv.Put(PreferenceProvider.PREF_KEY, key);
            cv.Put(PreferenceProvider.PREF_VALUE, value ? 1 : 0);
            context.ContentResolver.Update(URI, cv, null, null);
        }

        public static void SetInt(Context context, String name, String key, int value)
        {
            Android.Net.Uri URI = PreferenceProvider.BuildUri(name, key, PreferenceProvider.PREF_INT);
            ContentValues cv = new ContentValues();
            cv.Put(PreferenceProvider.PREF_KEY, key);
            cv.Put(PreferenceProvider.PREF_VALUE, value);
            context.ContentResolver.Update(URI, cv, null, null);
        }

        public static void SetLong(Context context, String name, String key, long value)
        {
            Android.Net.Uri URI = PreferenceProvider.BuildUri(name, key, PreferenceProvider.PREF_LONG);
            ContentValues cv = new ContentValues();
            cv.Put(PreferenceProvider.PREF_KEY, key);
            cv.Put(PreferenceProvider.PREF_VALUE, value);
            context.ContentResolver.Update(URI, cv, null, null);
        }

        public static void SetDateTime(Context context, String name, String key, DateTime value)
        {
            Android.Net.Uri URI = PreferenceProvider.BuildUri(name, key, PreferenceProvider.PREF_LONG);
            ContentValues cv = new ContentValues();
            cv.Put(PreferenceProvider.PREF_KEY, key);
            cv.Put(PreferenceProvider.PREF_VALUE, value.ToUniversalTime().Ticks);
            context.ContentResolver.Update(URI, cv, null, null);
        }
    }
}
