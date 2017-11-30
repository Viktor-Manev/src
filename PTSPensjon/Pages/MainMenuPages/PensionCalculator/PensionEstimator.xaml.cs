using System;
using System.ComponentModel;
using System.Collections.Generic;

using Xamarin.Forms;

namespace PTSPensjon
{
	public partial class PensionEstimator : ContentPage
	{
		public PensionEstimator()
		{
			InitializeComponent();

			if (Device.RuntimePlatform == Device.Android)
				header.IsVisible = false;

			//RegisterInputFieldCallbacks();

			/*EstimatePensionButton.GestureRecognizers.Add(new TapGestureRecognizer
			{
				Command = new Command( () => PressNextButton() ),
			});*/
		}

		public void RegisterInputFieldCallbacks ()
		{
			birthyearEntry.PropertyChanged += (sender, e) => OnValidateInputLength(sender, 4);

			superiorPeriod.PropertyChanged += (sender, e) => OnValidateInputLength(sender, 3);
			postSubordinatePeriod.PropertyChanged += (sender, e) => OnValidateInputLength(sender, 3);
			preSubordinatePeriod.PropertyChanged += (sender, e) => OnValidateInputLength(sender, 3);

			childSupport.PropertyChanged += (sender, e) => OnValidateInputLength(sender, 1);
		}

		void NavigateBack(object sender, EventArgs args)
		{
			MasterMenuConstructor.NavigteBackToRoot();
		}

		void MasterMenuTap(object sender, EventArgs args)
		{
			MasterMenuConstructor.ShowMasterDetail();
		}

		public void OnValidateInputLength(object sender, int restrictCount)
		{
			Entry entry = sender as Entry;
			String val = entry.Text;

			for (int i = 0; i < val.Length; i++)
			{
				var c = val[i];

				//System.Diagnostics.Debug.WriteLine("c: " + c + " as ASCII: " + (int)c);

				if ((int)c < 48 || (int)c > 57)
				{
					val = val.Remove(i);
					entry.Text = val;
				}
			}

			if (val.Length > restrictCount)
			{
				val = val.Remove(val.Length - 1);
				entry.Text = val;
			}
		}

		void PressNextButton()
		{
			var pensionInput = new PensionInput(
				birthyearEntry.Text, 
				superiorPeriod.Text,
				postSubordinatePeriod.Text,
				preSubordinatePeriod.Text,
				childSupport.Text 
			);
		
			App.NavigationService.NavigateTo("PensionResult", pensionInput);
		}
	}
}
