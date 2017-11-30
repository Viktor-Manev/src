﻿using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using Xamarin.Forms;

namespace PTSPensjon
{
	public class Localization
	{
		public CultureInfo ci;
		const string ResourceId = "PTSPensjon.Resx.TextResources";

		const string F9pDoubleNewline = "*n2*";
		const string F9pNewlineReplacement = "</br>";

		public static Localization Instance = null;

		public Localization()
		{
			Instance = this;

			if (Device.OS == TargetPlatform.iOS || Device.OS == TargetPlatform.Android)
			{
				var m_ci = PlayerPrefs.GetString("currentCulture");
				if (string.IsNullOrEmpty(m_ci))
				{
					ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
					PlayerPrefs.SetString("currentCulture", ci.ToString());
				}
				else
				{
					ci = new CultureInfo(m_ci);
				}
			}

			SettingsPage.localizeApp -= LocalizeApp;
			SettingsPage.localizeApp += LocalizeApp;
		}

		void LocalizeApp()
		{
			var m_ci = PlayerPrefs.GetString("currentCulture");
			ci = new CultureInfo(m_ci);
		}

		public static string Get(string key)
		{
			if (Instance == null)
				Instance = new Localization();
			
			if (string.IsNullOrEmpty(key))
				return string.Empty;

			ResourceManager resmgr = new ResourceManager(ResourceId, typeof(TranslateExtension).GetTypeInfo().Assembly);

			System.Diagnostics.Debug.WriteLine("CultureInfo: " + (Instance == null) + " | key: " + key);

			var translation = resmgr.GetString(key, Instance.ci);

			System.Diagnostics.Debug.WriteLine("Translation: " + translation + " | cultureInfo: " + Instance.ci.ToJson());
			if (translation == null)
			{
#if DEBUG
				throw new ArgumentException(
					String.Format("Key '{0}' was not found in resources '{1}' for culture '{2}'.", key, resmgr.GetType() /*ResourceId*/, Instance.ci.Name),
					"Text");
#else
                translation = key; // HACK: returns the key, which GETS DISPLAYED TO THE USER
#endif
			}

			if (translation.Contains(F9pDoubleNewline))
			{
				//System.Diagnostics.Debug.WriteLine("Providing translation. Was: " + translation);
				return translation.Replace(F9pDoubleNewline, F9pNewlineReplacement + F9pNewlineReplacement);
			}
			return translation;
		}
	}
}
