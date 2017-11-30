using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace PTSPensjon
{
	public partial class MasterMenuPage_XAML : ContentPage
	{
		public class MasterDetailChild
		{
			public string Id = string.Empty;
			public string NavigationId = string.Empty;

			public string AssociatedWebsite;
			public string TopBarLocalizationKey;

			public MasterDetailChild(string Id, string NavigationId, string AssociatedWebsite = null, string TopBarLocalizationKey = null)
			{
				this.Id = Id;
				this.NavigationId = NavigationId;
				this.AssociatedWebsite = AssociatedWebsite;
				this.TopBarLocalizationKey = TopBarLocalizationKey;
			}
		}

		private List<MasterDetailChild> MasterDetailChildren = new List<MasterDetailChild>
		{
			new MasterDetailChild("news", "NewsPage"),
			new MasterDetailChild("calc", "PensionEstimator"),
			new MasterDetailChild("rules", "RulesPage"),

			new MasterDetailChild("help", "HelpPage"),
			new MasterDetailChild("settings", "SettingsPage"),
			new MasterDetailChild("about", "AboutPage"),
        };

		public MasterMenuPage_XAML()
		{
			InitializeComponent();

			//System.Diagnostics.Debug.WriteLine("OnLocalize App callback should be set");

			SetMasterDetailChildren();

			SettingsPage.localizeApp -= SettingsPage_LocalizeApp;
			SettingsPage.localizeApp += SettingsPage_LocalizeApp;
		}

#if DEBUG
		private Button DebugPanelButton ()
		{
			var m_button = new Button
			{
				Text = "Debug Panel",
				BackgroundColor = Color.White,
				TextColor = Color.Black,
			};
			m_button.Clicked += (Object sender, EventArgs args) => App.NavigationService.NavigateTo("DebugPanel");

			return m_button;
		}
#endif

		void SettingsPage_LocalizeApp()
		{
			//System.Diagnostics.Debug.WriteLine("OnLocalize App in master menu. Localize");

			InitializeComponent();
			SetMasterDetailChildren();

			SettingsPage.localizeApp -= SettingsPage_LocalizeApp;
			SettingsPage.localizeApp += SettingsPage_LocalizeApp;
		}

		public void SetMasterDetailChildren ()
        {
			foreach (var m_child in MasterDetailChildren)
			{
				var m_layout = (RelativeLayout)this.FindByName<RelativeLayout>(m_child.Id);

				if (!string.IsNullOrEmpty(m_child.AssociatedWebsite))
				{
					SetOpenWebsitecallback(m_child, m_layout);
				}
				else
				{
					SetContentPageCallback(m_child, m_layout);
				}
			}
        }

		private void SetContentPageCallback(MasterDetailChild _child, RelativeLayout _layout)
		{
			_layout.GestureRecognizers.Add(new TapGestureRecognizer
			{
				Command = new Command(() =>
				{
					App.NavigationService.NavigateTo(_child.NavigationId);
				}),
			});
		}

		public void SetOpenWebsitecallback(MasterDetailChild _child, RelativeLayout _layout)
		{
			_layout.GestureRecognizers.Add(new TapGestureRecognizer
			{
				Command = new Command(() =>
				{
				}),
			});
		}

		public void TapBackToMainMenu(object sender, EventArgs args)
		{
			App.NavigationService.NavigateTo("MainSwitchboard_XAML", null, HistoryBehavior.ClearHistory);
		}
	}
}
