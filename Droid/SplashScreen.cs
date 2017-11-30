using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Util;

namespace PTSPensjon.Droid
{
	[Activity(Label = "Ditt PTS", MainLauncher = true, NoHistory = true, Theme = "@style/Theme.Splash",
	          ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
	public class SplashScreen : Activity
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			//Log.Debug("SplashScreenLauncher", "We got to the splash screen launcher. Verify it works.");

			var intent = new Intent(this, typeof(MainActivity));
			StartActivity(intent);
		}
	}
}