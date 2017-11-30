using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using Forms9Patch;

using Xamarin.Forms;

namespace PTSPensjon
{
	public partial class RulesPage : ContentPage
	{
		public bool IsEnglish { get; set; }

		public RulesPage()
		{
			InitializeComponent();

			if (Device.RuntimePlatform == Device.Android)
				header.IsVisible = false;

			SetLabelCallback(rulesDesc);
			SetLabelCallback(rulesDesc3);
		}

		public void SetLabelCallback (Forms9Patch.Label _label)
		{
			_label.ActionTagTapped += (sender, e) =>
			{
				//System.Diagnostics.Debug.WriteLine("ActionTagTapped. e.ToString: " + e.ToString() + " | e.Href: " + e.Href + " | e.Id: " + e.Id);

				if (!string.IsNullOrEmpty(e.Href))
				{
					
				}
			};
		}

		void NavigateBack(object sender, EventArgs args)
		{
			MasterMenuConstructor.NavigteBackToRoot();
		}

		void MasterMenuTap(object sender, EventArgs args)
		{
			MasterMenuConstructor.ShowMasterDetail();
		}
	}
}
