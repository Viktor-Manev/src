
using System;
using System.Collections.Generic;

using Xamarin.Forms;
//using PTSPensjon.ViewModels.Base;
using Xamarin.Forms.Xaml;



namespace PTSPensjon.Pages
{
	public partial class MenuPage : MasterDetailPage
	{
		//RootPage root;
		//List<HomeMenuItem> menuItems;

		public MenuPage(RootPage root)
		{
			/*this.root = root;
			InitializeComponent();
			BindingContext = new BaseViewModel(Navigation)
			{
				Title = "XamarinCRM",
				Subtitle = "XamarinCRM",
				Icon = "slideout.png"
			};

			ListViewMenu.ItemsSource = menuItems = new List<HomeMenuItem>
				{
				new HomeMenuItem { Title = "Contact", BottomBarMenu = BottomBarMenu.Contact, Icon ="contact.png" },
				new HomeMenuItem { Title = "About", BottomBarMenu = BottomBarMenu.About, Icon = "about.png" },
				new HomeMenuItem { Title = "Help", BottomBarMenu = BottomBarMenu.Help, Icon = "help.png" },
				new HomeMenuItem { Title = "Settings", BottomBarMenu = BottomBarMenu.Settings, Icon = "settings.png" },

				};

			ListViewMenu.SelectedItem = menuItems[0];

			ListViewMenu.ItemSelected += async (sender, e) =>
			{
				if (ListViewMenu.SelectedItem == null)
					return;

				await this.root.NavigateAsync(((HomeMenuItem)e.SelectedItem).BottomBarMenu);
			};*/

			var menuPage = new MasterMenuPage(); 			menuPage.OnMenuTap = (page) =>
			{
				IsPresented = false; 				Detail = new NavigationPage(page); 			};  			Master = menuPage; 			Detail = new NavigationPage(new HelpTab());
		}
	}
}

