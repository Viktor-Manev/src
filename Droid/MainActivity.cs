﻿using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Android.Gms.Common;

using Android.Util;

using Java.Interop;

namespace PTSPensjon.Droid
{
	[Activity(Label = "Ditt PTS", Exported = true, Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		const string TAG = "MainActivity";

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			global::Xamarin.Forms.Forms.Init(this, bundle);

			LoadApplication(new App());
		}

		protected override void OnResume()
		{
			base.OnResume();
		}

		public override void OnAttachedToWindow ()
		{
			base.OnAttachedToWindow ();
		}
	}
}
