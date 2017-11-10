using System;
using System.Collections.Generic;
using System.Diagnostics;
using Android.Content;
using Android.Database;
using Android.Text;

namespace CrossProcessPreferences
{
    public class PreferenceProvider : ContentProvider
    {
        private const string TAG = nameof(PreferenceProvider);

        private const string AUTHORITY = "crossprocesspreferences.PreferenceProvider";

        public const string CONTENT_PREF_BOOLEAN_URI = "content://" + AUTHORITY + "/boolean/";
        public const string CONTENT_PREF_STRING_URI = "content://" + AUTHORITY + "/string/";
        public const string CONTENT_PREF_INT_URI = "content://" + AUTHORITY + "/integer/";
        public const string CONTENT_PREF_LONG_URI = "content://" + AUTHORITY + "/long/";
        public const string CONTENT_PREF_DATETIME_URI = "content://" + AUTHORITY + "/datetime/";

        public const string PREF_KEY = "key";
        public const string PREF_VALUE = "value";

        public const int PREF_BOOLEAN = 1;
        public const int PREF_STRING = 2;
        public const int PREF_INT = 3;
        public const int PREF_LONG = 4;
        public const int PREF_DATETIME = 5;

        private static readonly UriMatcher sUriMatcher;

        static PreferenceProvider()
        {
            sUriMatcher = new UriMatcher(UriMatcher.NoMatch);
            sUriMatcher.AddURI(AUTHORITY, "boolean/*/*", PREF_BOOLEAN);
            sUriMatcher.AddURI(AUTHORITY, "string/*/*", PREF_STRING);
            sUriMatcher.AddURI(AUTHORITY, "integer/*/*", PREF_INT);
            sUriMatcher.AddURI(AUTHORITY, "long/*/*", PREF_LONG);
            sUriMatcher.AddURI(AUTHORITY, "datetime/*/*", PREF_DATETIME);
        }

        public override bool OnCreate()
        {
            return true;
        }

        public override ICursor Query(Android.Net.Uri uri, string[] projection, string selection, string[] selectionArgs, string sortOrder)
        {
            MatrixCursor cursor = null;
            PrefModel model = GetPrefModelByUri(uri);

            try
            {
                switch (sUriMatcher.Match(uri))
                {
                    case PREF_BOOLEAN:
                        if (GetDPreference(model.Name).HasKey(model.Key))
                        {
                            cursor = PreferenceToCursor(new Java.Lang.Boolean((GetDPreference(model.Name).GetPrefBoolean(model.Key, false))));
                        }
                        break;
                    case PREF_STRING:
                        if (GetDPreference(model.Name).HasKey(model.Key))
                        {
                            cursor = PreferenceToCursor(new Java.Lang.String((GetDPreference(model.Name).GetPrefString(model.Key, ""))));
                        }
                        break;
                    case PREF_INT:
                        if (GetDPreference(model.Name).HasKey(model.Key))
                        {
                            cursor = PreferenceToCursor((Java.Lang.Integer)(GetDPreference(model.Name).GetPrefInt(model.Key, -1)));
                        }
                        break;
                    case PREF_LONG:
                        if (GetDPreference(model.Name).HasKey(model.Key))
                        {
                            cursor = PreferenceToCursor((Java.Lang.Long)(GetDPreference(model.Name).GetPrefLong(model.Key, -1)));
                        }
                        break;
                    case PREF_DATETIME:
                        if (GetDPreference(model.Name).HasKey(model.Key))
                        {
                            cursor = PreferenceToCursor((Java.Lang.Long)(GetDPreference(model.Name).GetPrefLong(model.Key, -1)));
                        }
                        break;
                }
                return cursor;
            }
            catch (Exception exQuery)
            {
                for (int i = 0; i < 10; i++)
                    Debug.WriteLine($"PreferenceProvider Query Exception: {exQuery.Message} ON pref: {sUriMatcher.Match(uri)}\nKey: {model.Key}");
                throw exQuery;
            }
        }

        public override string GetType(Android.Net.Uri uri)
        {
            return null;
        }

        public override Android.Net.Uri Insert(Android.Net.Uri uri, ContentValues values)
        {
            throw new InvalidOperationException("insert not supported!!!");
        }

        public override int Delete(Android.Net.Uri uri, string selection, string[] selectionArgs)
        {
            switch (sUriMatcher.Match(uri))
            {
                case PREF_BOOLEAN:
                case PREF_LONG:
                case PREF_STRING:
                case PREF_INT:
                case PREF_DATETIME:
                    PrefModel model = GetPrefModelByUri(uri);
                    if (model != null)
                    {
                        GetDPreference(model.Name).RemovePreference(model.Key);
                    }
                    break;
                default:
                    throw new InvalidOperationException(" unsupported uri : " + uri);
            }
            return 0;
        }

        public override int Update(Android.Net.Uri uri, ContentValues values, string selection, string[] selectionArgs)
        {
            PrefModel model = GetPrefModelByUri(uri);
            if (model == null)
            {
                throw new InvalidOperationException("update prefModel is null");
            }
            switch (sUriMatcher.Match(uri))
            {
                case PREF_BOOLEAN:
                    PersistBoolean(model.Name, values);
                    break;
                case PREF_LONG:
                    PersistLong(model.Name, values);
                    break;
                case PREF_STRING:
                    PersistString(model.Name, values);
                    break;
                case PREF_INT:
                    PersistInt(model.Name, values);
                    break;
                case PREF_DATETIME:
                    PersistDateTime(model.Name, values);
                    break;
                default:
                    throw new InvalidOperationException("update unsupported uri : " + uri);
            }
            return 0;
        }

        private static String[] PREFERENCE_COLUMNS = { PREF_VALUE };

        private MatrixCursor PreferenceToCursor<T>(T value)
                where T : Java.Lang.Object
        {
            MatrixCursor matrixCursor = new MatrixCursor(PREFERENCE_COLUMNS, 1);
            MatrixCursor.RowBuilder builder = matrixCursor.NewRow();
            builder.Add(value);
            return matrixCursor;
        }

        private void PersistInt(String name, ContentValues values)
        {
            if (values == null)
            {
                throw new InvalidOperationException(" values is null!!!");
            }
            String kInteger = values.GetAsString(PREF_KEY);
            int vInteger = values.GetAsInteger(PREF_VALUE);
            GetDPreference(name).SetPrefInt(kInteger, vInteger);
        }

        private void PersistBoolean(String name, ContentValues values)
        {
            if (values == null)
            {
                throw new InvalidOperationException(" values is null!!!");
            }
            String kBoolean = values.GetAsString(PREF_KEY);
            bool vBoolean = values.GetAsBoolean(PREF_VALUE);
            GetDPreference(name).SetPrefBoolean(kBoolean, vBoolean);
        }

        private void PersistLong(String name, ContentValues values)
        {
            if (values == null)
            {
                throw new InvalidOperationException(" values is null!!!");
            }
            String kLong = values.GetAsString(PREF_KEY);
            long vLong = values.GetAsLong(PREF_VALUE);
            GetDPreference(name).SetPrefLong(kLong, vLong);
        }

        private void PersistDateTime(string name, ContentValues values)
        {
            if (values == null)
            {
                throw new InvalidOperationException(" values is null!!!");
            }
            String kDatetime = values.GetAsString(PREF_KEY);
            long vDateTimeTicks = values.GetAsLong(PREF_VALUE);
            GetDPreference(name).SetPrefLong(kDatetime, vDateTimeTicks);
        }

        private void PersistString(String name, ContentValues values)
        {
            if (values == null)
            {
                throw new InvalidOperationException(" values is null!!!");
            }
            String kString = values.GetAsString(PREF_KEY);
            String vString = values.GetAsString(PREF_VALUE);
            GetDPreference(name).SetPrefString(kString, vString);
        }

        //private static Map<String, IPrefImpl> sPreferences = new ArrayMap<>();
        private Dictionary<string, IPrefImpl> sPreferences = new Dictionary<string, IPrefImpl>();

        private IPrefImpl GetDPreference(String name)
        {
            if (TextUtils.IsEmpty(name))
            {
                throw new InvalidOperationException("getDPreference name is null!!!");
            }
            if (!sPreferences.ContainsKey(name))
            {
                IPrefImpl pref = new PreferenceImpl(this.Context, name);
                sPreferences.Add(name, pref);
            }
            return sPreferences[name];
        }

        private PrefModel GetPrefModelByUri(Android.Net.Uri uri)
        {
            if (uri == null || uri.PathSegments.Count != 3)
            {
                throw new InvalidOperationException("getPrefModelByUri uri is wrong : " + uri);
            }
            String name = uri.PathSegments[1];
            String key = uri.PathSegments[2];
            return new PrefModel(name, key);
        }


        public static Android.Net.Uri BuildUri(String name, String key, int type)
        {
            return Android.Net.Uri.Parse(GetUriByType(type) + name + "/" + key);
        }

        private static String GetUriByType(int type)
        {
            switch (type)
            {
                case PREF_BOOLEAN:
                    return CONTENT_PREF_BOOLEAN_URI;
                case PREF_INT:
                    return CONTENT_PREF_INT_URI;
                case PREF_LONG:
                    return CONTENT_PREF_LONG_URI;
                case PREF_STRING:
                    return CONTENT_PREF_STRING_URI;
                case PREF_DATETIME:
                    return CONTENT_PREF_DATETIME_URI;
            }
            throw new InvalidOperationException("unsupport preftype : " + type);
        }

        private class PrefModel
        {
            public PrefModel(string name, string key)
            {
                this.Name = name;
                this.Key = key;
            }

            public string Name { get; set; }

            public string Key { get; set; }
        }
    }
}
