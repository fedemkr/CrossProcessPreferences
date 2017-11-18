# CrossProcessPreferences

[SharedPreferences](https://developer.android.com/reference/android/content/SharedPreferences.html)'s MULTI_PROCESS_MODE has been deprecated in API 23 for being not so reliable. Thus if you want to use SharedPreferences in multiple processes then you can use this.

It has an easy API that is similar to the one in SharedPreferences and works using a ContentProvider that must be registered in the AndroidManifest:

```xml
<application android:label="MyProject" android:icon="@mipmap/ic_launcher">
    <provider android:name="crossprocesspreferences.PreferenceProvider" android:authorities="crossprocesspreferences.PreferenceProvider" android:exported="false" />
</application>
```

Then you can use it like this:

```c#
var preferences = new CrossProcessSharedPreferences(this.Context, "myPreferencesName");
preferences.SetPrefString("myKey", "myValue");
var valueOfMyKey = preferences.GetPrefString("myKey", "defaultValue");
var isMyKeyStored = preferences.HasKey<string>("myKey");
// remove the key from the preferences
preferences.Remove("myKey");
```

It supports int, long, bool (also if they are nullable), string and DateTime (it will store it as UTC).

Roadmap:

- Add support for decimal, double
