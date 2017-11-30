using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace PTSPensjon
{
	public class MasterMenuConstructor
	{
		MasterDetailPage MD = null;
		public MasterDetailPage MDPage { get { return MD; } }

		public static void NavigteBackToRoot()
		{
			NavigateBack();
		}

		public static void SetDetail(string pageKey)
		{
			App.NavigationService.NavigateTo(pageKey);
		}

		public static void ShowMasterDetail()
		{
			App.MDPage.IsPresented = true;
		}

		public static void NavigateBack()
		{
			App.NavigationService.GoBack();
		}
	}
}
