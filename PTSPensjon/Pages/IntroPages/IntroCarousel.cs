﻿using System;
using Xamarin.Forms;

namespace PTSPensjon
{
	public class IntroCarousel : CarouselPage
	{
		static CarouselPage Instance = null;

		public IntroCarousel()
		{
			Instance = this;

            this.Children.Add( new IntroPage1() );
			this.Children.Add( new IntroPage2() );
			this.Children.Add( new IntroPage3() );
			this.Children.Add( new IntroPage4() );
			this.Children.Add( new IntroPage5() );
		}

		public static void AddIntroPageChild(Type pageType)
		{
			var displayPage = (ContentPage)Activator.CreateInstance(pageType);
			Instance.Children.Add(displayPage);
		}

		public static void SetIndex(int newIndex)
		{
			Instance.CurrentPage = Instance.Children[newIndex];
		}

		public static void NavigateToMainMenu ()
		{
			PlayerPrefs.SetString("_hasSeenIntro", "true");

			App.NavigationService.NavigateTo("MainSwitchboard_XAML", null,
				HistoryBehavior.ClearHistory);
		}
	}
}