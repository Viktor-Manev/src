using System;
using Xamarin.Forms;

namespace PTSPensjon
{
	public class PlayerPrefs
	{
		public PlayerPrefs() { }

		public static void SetString(string key, string value)
		{
			//System.Diagnostics.Debug.WriteLine("Trying to set string");

			DependencyService.Get<IStorage>().SetString(key, value);
		}

		public static string GetString(string key)
		{
			return DependencyService.Get<IStorage>().GetString(key);
		}

		public static void DeleteAll()
		{
			DependencyService.Get<IStorage>().DeleteCredentials();
		}
	}
}
