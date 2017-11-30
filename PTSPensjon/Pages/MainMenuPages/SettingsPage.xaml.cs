﻿using System;
using System.Globalization;
using PTSPensjon.Statics;

using Xamarin.Forms;

namespace PTSPensjon
{
    public partial class SettingsPage : ContentPage
    {
		public static event Action localizeApp;
		public static event Action updateFontSizes;
		public static event Action updatedAudioState;

		private bool IsAudioEnabled => !(PlayerPrefs.GetString("audioEnabled") == "false");

		public SettingsPage()
        {
            InitializeComponent();

			if (Device.RuntimePlatform == Device.Android)
				header.IsVisible = false;

            FormatText();

			ConfigureCallbacks();

			audioButton.Source = IsAudioEnabled ? "SoundOn" : "SoundOff";

			ConfigurePushNotificationSection();

            SetBuildVersion();
        }

		void ConfigureCallbacks ()
		{
			RegisterCallback(language_bokmal, TappedNorwegianButton);
			RegisterCallback(language_english, TappedEnglishButton);

			RegisterCallback(fontsize_small, TappedFontSizeSmall);
			RegisterCallback(fontsize_medium, TappedFontSizeMedium);
			RegisterCallback(fontsize_large, TappedFontSizeLarge);

			RegisterCallback(audioButton, ToggleAudioButton);
			watchIntroAbsoluteLayout.GestureRecognizers.Add(new TapGestureRecognizer
			{
				Command = new Command(() => TappedPlayIntro())
			});
		}

		void RegisterCallback(Image m_image, Action m_action)
		{
			while (m_image.GestureRecognizers.Count > 0)
			{
				m_image.GestureRecognizers.RemoveAt(0);
			}
			m_image.GestureRecognizers.Add(new TapGestureRecognizer
			{
				Command = new Command(() => m_action?.Invoke()),
			});
		}
		void RegisterCallback(StackLayout m_stackLayout, Action m_action)
		{
			while(m_stackLayout.GestureRecognizers.Count > 0)
			{
				m_stackLayout.GestureRecognizers.RemoveAt(0);
			}
			m_stackLayout.GestureRecognizers.Add(new TapGestureRecognizer
			{
				Command = new Command( () => m_action?.Invoke() ),
			});
		}

		void ConfigurePushNotificationSection ()
		{
		}

		void PopPushNotificationPopup ()
		{
			
		}

		void ToggleAudioButton ()
		{
			var m_audioIsEnabled = IsAudioEnabled;
			if (m_audioIsEnabled)
			{
				PlayerPrefs.SetString("audioEnabled", "false");
			}
			else
			{
				PlayerPrefs.SetString("audioEnabled", "true");
			}

			//System.Diagnostics.Debug.WriteLine("SettingsPage constructor. m_audioEnabled: " + m_audioIsEnabled);

			audioButton.Source = !m_audioIsEnabled ? "SoundOn" : "SoundOff";
			updatedAudioState?.Invoke();
		}

        void TappedPlayIntro()
        {
			App.NavigationService.NavigateTo("IntroCarousel", null, HistoryBehavior.ClearHistory);
        }

        public void FormatText()
        {
            var fs = new FormattedString();

            fs.Spans.Add(new Span
            {
				Text = Localization.Get("Settings_about_desc"),
                ForegroundColor = PTSPalette._001,
                FontSize = FontSizes.LabelSize,
                FontAttributes = FontAttributes.None
            });

            developedByLabel.FormattedText = fs;
        }

        public void TappedNorwegianButton()
        {
			UpdateLanguage("nb-NO");
        }
        public void TappedEnglishButton()
        {
			UpdateLanguage("en");
        }

		private void UpdateLanguage(string cultureName)
		{
			PlayerPrefs.SetString("currentCulture", cultureName);

			var ci = new CultureInfo(cultureName);
			DependencyService.Get<ILocalize>().SetLocale(ci);

			localizeApp?.Invoke();
			ReConfigureSettingsPage();
		}

		private void ReConfigureSettingsPage ()
		{
			InitializeComponent();

			if (Device.RuntimePlatform == Device.Android)
				header.IsVisible = false;
			
			FormatText();
			ConfigureCallbacks();
			audioButton.Source = IsAudioEnabled ? "SoundOn" : "SoundOff";
			ConfigurePushNotificationSection();
			SetBuildVersion();
		}

        public void TappedFontSizeSmall()
        {
            UpdateFontSize(FontSize.Small);
        }
        public void TappedFontSizeMedium()
        {
            UpdateFontSize(FontSize.Medium);
        }
        public void TappedFontSizeLarge()
        {
			UpdateFontSize(FontSize.Large);
        }

		public void UpdateFontSize(FontSize fontSize)
		{
			FontSizes.ReCalculateFontSizes(fontSize);

			updateFontSizes?.Invoke();
			ReConfigureSettingsPage();
		}

        void NavigateBack(object sender, EventArgs args)
        {
            MasterMenuConstructor.NavigteBackToRoot();
        }

        void MasterMenuTap(object sender, EventArgs args)
        {
            MasterMenuConstructor.ShowMasterDetail();
        }

        private void SetBuildVersion()
        {
			
		}
	}
}
