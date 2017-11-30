using System;
using System.Collections.Generic;
using PTSPensjon.Resx;

using Xamarin.Forms;

namespace PTSPensjon
{
	public partial class AboutPage : ContentPage
	{
		public AboutPage()
		{
			InitializeComponent();

			if (Device.RuntimePlatform == Device.Android)
				header.IsVisible = false;

			SetPostalAddressText();
			SetCallUsText();
			SetEmailText();
		}

		void NavigateBack(object sender, EventArgs args)
		{
			MasterMenuConstructor.NavigteBackToRoot();
		}

		void MasterMenuTap(object sender, EventArgs args)
		{
			MasterMenuConstructor.ShowMasterDetail();
		}

		public void CallPTS(object sender, EventArgs args)
		{
			//System.Diagnostics.Debug.WriteLine("CallPTS. Try to ShowAlertView");
		}

		public void EmailPTS(object sender, EventArgs args)
		{
		}

		public void SetPostalAddressText()
		{
			var fs = new FormattedString();

			fs.Spans.Add(new Span { Text = Localization.Get("About_postbox"), ForegroundColor = PTSPalette._001, 
									FontSize = 12, FontAttributes = FontAttributes.Bold });
			fs.Spans.Add(new Span { Text = Localization.Get("Static_address"), ForegroundColor = PTSPalette._001, 
									FontSize = 12, FontAttributes = FontAttributes.Bold });

			postalAddress.FormattedText = fs;
		}

		public void SetCallUsText()
		{
			var fs = new FormattedString();

			fs.Spans.Add(new Span { Text = Localization.Get("About_telephone"), ForegroundColor = PTSPalette._001, 
									FontSize = 12, FontAttributes = FontAttributes.None });
			fs.Spans.Add(new Span { Text = Localization.Get("Static_phone_number"), ForegroundColor = PTSPalette._001, 
									FontSize = 12, FontAttributes = FontAttributes.Bold });

			phoneLabel.FormattedText = fs;
		}

		public void SetEmailText()
		{
			var fs = new FormattedString();

			fs.Spans.Add(new Span { Text = Localization.Get("About_email"), ForegroundColor = PTSPalette._001, 
									FontSize = 12, FontAttributes = FontAttributes.None });
			fs.Spans.Add(new Span { Text = Localization.Get("Static_email"), ForegroundColor = PTSPalette._001, 
									FontSize = 12, FontAttributes = FontAttributes.Bold });

			emailLabel.FormattedText = fs;
		}
	}
}
