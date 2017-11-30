using System; using System.Linq; using System.Text; using System.Collections.Generic;  using Xamarin.Forms;

//using System.Xml.Linq;

namespace PTSPensjon {
	//INotifyPropertyChanged
	public partial class MainSwitchboard_XAML : ContentPage
	{ 		public static MainSwitchboard_XAML Instance = null;  		public int newsQtyUpdate = -1; 		private const string BASE_PREFS_STRING = "NewsItem_";

		//public delegate void WebViewNavigatedHandler(object sender, CookieNavigatedEventArgs args);

		/*protected override void OnSizeAllocated(double width, double height)
		{
			base.OnSizeAllocated(width, height); //must be called             if (this.width != width || this.height != height)
			{
				this.width = width;
				this.height = height;
				//reconfigure layout

				System.Diagnostics.Debug.WriteLine("OnSizeAllocated: " + width + " | height: " + height); 
                if (width > height)                 { 					if (Device.Idiom == TargetIdiom.Tablet) 					{ 						masterMenuButton.IsVisible = false; 					}                     //System.Diagnostics.Debug.WriteLine("We are in landscape mode");

					//row1.Height = 52;
                    //header.IsVisible = false;
                    //logo.IsVisible = false;                 }                 else                 { 					masterMenuButton.IsVisible = true;                     //row1.Height = PTSStyles._MainMenuRow_001;                     //header.IsVisible = true;                     //logo.IsVisible = true;                 }
			}
		}*/

		public MainSwitchboard_XAML()
		{ 			Instance = this;
			InitializeComponent();  			if (Device.RuntimePlatform == Device.Android) 				header.IsVisible = false;

#if DEBUG             /*var m_debugSwitchboardButton = new SwitchboardButton(); 			m_debugSwitchboardButton.AutomationId = "_debug"; 			m_debugSwitchboardButton.Icon = ImageSource.FromFile("Knapp.png"); 			m_debugSwitchboardButton.OverlayIcon = ImageSource.FromFile("IconSettings.png"); 			m_debugSwitchboardButton.Label = "Debug"; 			m_debugSwitchboardButton.GestureRecognizers.Add( new TapGestureRecognizer((obj) => DebugPanelTapped() ));              mainGrid.Children.Add(
				m_debugSwitchboardButton
			  );*/
#endif             //StartFetchNews();

			Device.BeginInvokeOnMainThread(() => 			{ 				bg_image.Source = "Bkg_01.png";  				var animate = new Animation(d => bg_image.Opacity = d, 0.0f, 1.0f, Easing.SinInOut); 				animate.Commit(bg_image, "anim1", 16, 1000); 			});  			//Device.BeginInvokeOnMainThread( () => DetermineMeldingerLabel() ); 			//Device.BeginInvokeOnMainThread( () => DetermineFartstidLabel() );  			SettingsPage.localizeApp -= SettingsPage_LocalizeApp; 			SettingsPage.localizeApp += SettingsPage_LocalizeApp;  			Device.BeginInvokeOnMainThread(() => 			{
				bool _isFirstSession = PlayerPrefs.GetString("FirstSession") != "false";
				if (_isFirstSession)
				{
					PlayerPrefs.SetString("FirstSession", "false");
					App.NavigationService.PopPushNotificationsScreenModal();
				} 			}); 		}  		public void RefreshNotifications () 		{ 			System.Diagnostics.Debug.WriteLine("RefreshNotifications. Updated labels"); 
			Device.BeginInvokeOnMainThread( () => DetermineMeldingerLabel() );
			Device.BeginInvokeOnMainThread( () => DetermineFartstidLabel() ); 		}

		void SettingsPage_LocalizeApp()
		{
			System.Diagnostics.Debug.WriteLine("Localize App");  			InitializeComponent();  			if (Device.RuntimePlatform == Device.Android) 				header.IsVisible = false;  			Device.BeginInvokeOnMainThread(() => 			{ 				bg_image.Source = "Bkg_01.png";  				var animate = new Animation(d => bg_image.Opacity = d, 0.0f, 1.0f, Easing.SinInOut); 				animate.Commit(bg_image, "anim1", 16, 1000); 			});  			SettingsPage.localizeApp -= SettingsPage_LocalizeApp; 			SettingsPage.localizeApp += SettingsPage_LocalizeApp;  			//App.hideMasterMenuButton += (obj) => masterMenuButton.IsVisible = obj;
		}  		protected override void OnAppearing() 		{ 			base.OnAppearing();  			StartFetchNews();  			Device.BeginInvokeOnMainThread(() => DetermineMeldingerLabel()); 			Device.BeginInvokeOnMainThread(() => DetermineFartstidLabel()); 		}  		private void StartFetchNews () 		{ 			Device.BeginInvokeOnMainThread(() => 			{ 				try 				{ 					var m_posts = new RSSReader().ReadFeed(@"http://www.pts.no/feed/"); 					UpdateNewsLabel(m_posts); 				} 				catch (Exception e) 				{  				} 			}); 		}  		public void UpdateNewsLabel(IEnumerable<RSSReader.Post> posts) 		{ 			var m_postsList = posts.ToList(); 			var m_filteredList = m_postsList.FindAll(c => c.Category == "For medlemmer" || c.Category == "Ukategorisert");

			//System.Diagnostics.Debug.WriteLine("m_filteredList.Count: " + m_filteredList.Count);  			var m_unreadCount = m_filteredList.FindAll( c => string.IsNullOrEmpty(PlayerPrefs.GetString(BASE_PREFS_STRING + m_filteredList.IndexOf(c)))).Count; 			if (m_unreadCount > 0) 			{ 				newsButton.PushIconText = m_unreadCount.ToString(); 			} 			else 			{ 				newsButton.PushIconText = ""; 			}  			//System.Diagnostics.Debug.WriteLine("m_unreadCount.Count: " + m_unreadCount); 		}  		public void DetermineMeldingerLabel() 		{ 			int m_newMessages = 0; 			int m_unreadMessages = 0;  			var antallNyeMeldinger = PlayerPrefs.GetString("AntallNyeMeldinger");
			var antallUlesteMeldinger = PlayerPrefs.GetString("AntallUlesteMeldinger");  			System.Diagnostics.Debug.WriteLine("antallNyeMeldinger: " + antallNyeMeldinger + " | AntallUlesteMeldinger: " + antallUlesteMeldinger);  			Int32.TryParse(antallNyeMeldinger, out m_newMessages); 			Int32.TryParse(antallUlesteMeldinger, out m_unreadMessages);  			System.Diagnostics.Debug.WriteLine("m_newMessages: " + m_newMessages + " | m_unreadMessages: " + m_unreadMessages);  			if (m_newMessages > 0 || m_unreadMessages > 0) 			{ 				//If either of new messages are 0, select Max. Else, select then min.  				var m_msgDisplayQty = m_newMessages == 0 || m_unreadMessages == 0 ?  					Math.Max(m_newMessages, m_unreadMessages) :  					Math.Min(m_newMessages, m_unreadMessages);  				System.Diagnostics.Debug.WriteLine( 					string.Format("Messages. New={0}, Unread={1}, ShowToEndUser={2}", m_newMessages, m_unreadMessages, m_msgDisplayQty));  				messagesButton.PushIconText = m_msgDisplayQty.ToString(); 			} 			else 			{ 				System.Diagnostics.Debug.WriteLine("Set messages label to zero"); 				messagesButton.PushIconText = "0"; 			} 		}  		public void DetermineFartstidLabel() 		{ 			int m_newSeatime = 0; 			Int32.TryParse(PlayerPrefs.GetString("NyFartstid"), out m_newSeatime);  			//System.Diagnostics.Debug.WriteLine("DetermineFartstidLabel. m_newSeatime: " + m_newSeatime + " | PlayerPrefs.GetString(NyFartstid): " + PlayerPrefs.GetString("NyFartstid"));  			if (m_newSeatime > 0) 			{ 				seatimeButton.PushIconText = "1"; 			} 			else 			{ 				seatimeButton.PushIconText = "0"; 			} 		}  		protected override void OnParentSet()
		{
			base.OnParentSet();

			if (Device.Idiom == TargetIdiom.Tablet)
			{ 				scrollView.VerticalOptions = LayoutOptions.Start;
				scrollView.Padding = new Thickness(10, 120, 10, 10);
				mainGrid.RowSpacing = 60; 				mainGrid.Padding = 40; 			}
		}

		public void MessageButtonTapped(object sender, EventArgs args)
		{ 		}

		public void SeatimeButtonTapped(object sender, EventArgs args)
		{ 		}

		public void ApplypensionButtonTapped(object sender, EventArgs args)
		{ 		}

		public void NewsButtonTapped(object sender, EventArgs args)
		{ 		}  		public void EstimatorButtonTapped(object sender, EventArgs args) 		{ 			MasterMenuConstructor.SetDetail( "PensionEstimator"); 		}  		public void RulesButtonTapped(object sender, EventArgs args) 		{ 			MasterMenuConstructor.SetDetail( "RulesPage"); 		}  		public void HelpButtonTapped(object sender, EventArgs args) 		{ 			MasterMenuConstructor.SetDetail( "HelpPage"); 		}  		public void SettingsButtonTapped(object sender, EventArgs args) 		{ 			MasterMenuConstructor.SetDetail("SettingsPage"); 		}  		public void AboutButtonTapped(object sender, EventArgs args) 		{ 			MasterMenuConstructor.SetDetail("AboutPage"); 		}  		public void DebugPanelTapped(object sender, EventArgs args) 		{
#if DEBUG             MasterMenuConstructor.SetDetail("DebugPanel");
#endif 		}
 		public void ShowWebsite(string url, string topBarTitle)
		{ 		}  		public void MasterMenuTap(object sender, EventArgs args) 		{ 			MasterMenuConstructor.ShowMasterDetail(); 		}
	} } 