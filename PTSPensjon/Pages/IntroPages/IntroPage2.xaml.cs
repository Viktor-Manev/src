using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace PTSPensjon
{
	public partial class IntroPage2 : ContentPage
	{
		public IntroPage2()
		{
			InitializeComponent();

			if (Device.RuntimePlatform == Device.Android)
				header.IsVisible = false;
			
			SkipIntroText.GestureRecognizers.Add(new TapGestureRecognizer
			{
				Command = new Command( () => IntroCarousel.NavigateToMainMenu() ),
			});
		}
	}
}
