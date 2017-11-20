
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace Sample.Core.Droid
{
    [Activity (Label = "Sample.Core.Android", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
        private Intent someServiceIntent;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            this.someServiceIntent = new Intent(this, typeof(SomeService));
            this.StartService(this.someServiceIntent);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.myButton);

            button.Click += delegate {
                button.Text = button.Text = $"{Settings.Instance.MyValue}. {Settings.Instance.TimesChangedMyValue} times";
            };

            button.LongClick += delegate
            {
                Settings.Instance.MyValue = "MainActivity changed this value";
                Settings.Instance.TimesChangedMyValue++;
                button.Text = $"{Settings.Instance.MyValue}. {Settings.Instance.TimesChangedMyValue} times";
            };
		}
	}
}


