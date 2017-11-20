using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;

namespace Sample.Core.Droid
{
    [Service(Exported = true, Process = ":someProcess")]
    public class SomeService : Service
    {
        public const int DELAY = 10000;

        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            if (Settings.Instance.TimesChangedMyValue > 20)
                return StartCommandResult.NotSticky;

            this.ChangeValue();

            this.ScheduleNextStart();

            return StartCommandResult.Sticky;
        }

        private static PendingIntent GetPendingIntent(Context context)
        {
            var intent = new Intent(context, typeof(SomeService));
            return PendingIntent.GetService(context, 0, intent, PendingIntentFlags.UpdateCurrent);
        }

        private void ScheduleNextStart()
        {
            ((AlarmManager)this.GetSystemService(Context.AlarmService))
                .Set(AlarmType.ElapsedRealtime, SystemClock.ElapsedRealtime() + DELAY, GetPendingIntent(this));
        }

        private void ChangeValue()
        {
            Settings.Instance.MyValue = "Some service in another process changed this value";
            Settings.Instance.TimesChangedMyValue++;
        }

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }
    }
}