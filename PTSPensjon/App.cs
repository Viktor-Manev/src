using System;
using Xamarin.Forms;

namespace PTSPensjon
{
	public class App : Application
	{
		static MasterDetailPage MD = null;
		public static MasterDetailPage MDPage { get { return MD; } } 

		public static string AppName = "PTSPensjon";

		public static NavigationService NavigationService { get; }
			= new NavigationService();

		public App()
		{
			var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
			Resx.TextResources.Culture = ci; // set the RESX for resource localization
			DependencyService.Get<ILocalize>().SetLocale(ci); // set the Thread for locale-aware methods

			RegisterServices();

			if (PlayerPrefs.GetString("_hasSeenIntro") == "true")
			{
				SetMainMenuPage(typeof(MainSwitchboard_XAML));
			}
			else
			{
				SetMainMenuPage(typeof(IntroCarousel));
			}
		}

		private void RegisterServices()
		{
			NavigationService.Configure("IntroCarousel", typeof(IntroCarousel));

			NavigationService.Configure("MainSwitchboard_XAML", typeof(MainSwitchboard_XAML));

			NavigationService.Configure("PensionEstimator", typeof(PensionEstimator));
			NavigationService.Configure("PensionResult", typeof(PensionResult));
			NavigationService.Configure("RulesPage", typeof(RulesPage));
			NavigationService.Configure("HelpPage", typeof(HelpPage));

			NavigationService.Configure("SettingsPage", typeof(SettingsPage));
			NavigationService.Configure("AboutPage", typeof(AboutPage));
		}

		public void SetMainMenuPage(Type page)
		{
			MD = new MasterDetailPage();

			Page m_navPage = (Page)Activator.CreateInstance(page);
			NavigationPage.SetHasNavigationBar(m_navPage, false);

			MD.Master = (Page)Activator.CreateInstance(typeof(MasterMenuPage_XAML));
			MD.Detail = new NavigationPage(m_navPage);

			MainPage = MD;
		}

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}