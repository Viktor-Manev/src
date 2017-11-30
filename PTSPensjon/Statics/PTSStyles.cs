using System;
using Xamarin.Forms;

namespace PTSPensjon
{
	public static class PTSStyles
	{
		/// <summary>
		///  Intro row heights
		/// </summary>
		public static readonly GridLength _IntroRow_001 = new GridLength(Device.OnPlatform(50, 20, 50), GridUnitType.Absolute);
		public static readonly GridLength _IntroRow_002 = new GridLength( 85, GridUnitType.Absolute);
		public static readonly GridLength _IntroRow_003 = new GridLength(500, GridUnitType.Star);
		public static readonly GridLength _IntroRow_004 = new GridLength( 50, GridUnitType.Absolute);
		public static readonly GridLength _IntroRow_005 = new GridLength(140, GridUnitType.Absolute);

		/// <summary>
		///  Main menu screen row heights
		/// </summary>
		public static readonly GridLength _MainMenuRow_001 = new GridLength(Device.OnPlatform(85, 55, 85), GridUnitType.Absolute);
		public static readonly GridLength _MainMenuRow_002 = new GridLength(500, GridUnitType.Star);


		/// <summary>
		///  Master menu page
		/// </summary>



	}
}
