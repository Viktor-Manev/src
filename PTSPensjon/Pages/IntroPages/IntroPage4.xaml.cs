using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace PTSPensjon
{
	public partial class IntroPage4 : ContentPage
	{
		public IntroPage4()
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
