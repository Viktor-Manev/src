using System;
using System.Collections.Generic;
using PTSPensjon.Resx;

using Xamarin.Forms;

namespace PTSPensjon
{
	public partial class HelpPage : ContentPage
	{
		public HelpPage()
		{
			InitializeComponent();

			if (Device.RuntimePlatform == Device.Android)
				header.IsVisible = false;

			if (FAQReader.Instance == null)
				new FAQReader();

			SetCallUsText();
			SetCallUsOperatorTimes();

			//TODO Cache commands

			faqSection.GestureRecognizers.Add(new TapGestureRecognizer
			{
				Command = new Command(() => TapFAQ()),
			});
			emailPTS.GestureRecognizers.Add(new TapGestureRecognizer
			{
				Command = new Command(() => EmailPTS()),
			});
			callPTS.GestureRecognizers.Add(new TapGestureRecognizer
			{
				Command = new Command(() => CallPTS()),
			});
			/*startChat.GestureRecognizers.Add(new TapGestureRecognizer
			{
				Command = new Command(() => StartChat()),
			});*/

			SettingsPage.localizeApp -= LocalizeApp;
			SettingsPage.localizeApp += LocalizeApp;
		}

		void LocalizeApp()
		{
			//FAQReader.Instance = null;
			//new FAQReader();
		}

		void NavigateBack(object sender, EventArgs args)
		{
			MasterMenuConstructor.NavigteBackToRoot();
		}

		void MasterMenuTap(object sender, EventArgs args)
		{
			MasterMenuConstructor.ShowMasterDetail();
		}

		public void TapFAQ()
		{
		}

		public void EmailPTS()
		{
		}

		public void CallPTS()
		{
		}

		public void StartChat()
		{
		}

		public void SetCallUsText()
		{
			var fs = new FormattedString();

			fs.Spans.Add(new Span
			{
				Text = Localization.Get("Help_callus"),
				ForegroundColor = PTSPalette._001,	
				FontSize = 16,
				FontAttributes = FontAttributes.None
			});
			fs.Spans.Add(new Span
			{
				Text = Localization.Get("Static_phone_number"),
				ForegroundColor = PTSPalette._001,
				FontSize = 16,
				FontAttributes = FontAttributes.Bold
			});

			callUsId.FormattedText = fs;
		}

		public void SetCallUsOperatorTimes ()
		{
			callTimesId.Text = OperatorLocalizationWidget.GetOperatorTimeLocalization((DateTime.Now));
		}
	}
}
