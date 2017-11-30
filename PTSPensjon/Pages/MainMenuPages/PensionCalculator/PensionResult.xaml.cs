using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace PTSPensjon
{
	public partial class PensionResult : ContentPage
	{
		const float s1 = 852.07f;
		const float s2 = 711.62f;
		const float s3 = 608.63f;

		public PensionResult()
		{
			InitializeComponent();

			if (Device.RuntimePlatform == Device.Android)
				header.IsVisible = false;
		}

		protected override void OnAppearing()
		{
			var args = this.GetNavigationArgs() as PensionInput;

			base.OnAppearing();

			Calculate_Result(args);
		}

		/*public PensionResult(PensionInput pensionInput)
		{
			InitializeComponent();
			Calculate_Result(pensionInput);
		}*/

		void NavigateBack(object sender, EventArgs args)
		{
			MasterMenuConstructor.NavigateBack();
		}

		void MasterMenuTap(object sender, EventArgs args)
		{
			MasterMenuConstructor.ShowMasterDetail();
		}

		public static void SetCalculatorResult(PensionInput pensionInput)
		{
			
		}

		public void Calculate_Result(PensionInput pensionInput)
		{
			var birthYear = pensionInput.BirthYear;
			var m1 = (float)pensionInput.SuperiorPeriod;
			var m2 = (float)pensionInput.PreSubordinatePeriod;
			var m3 = (float)pensionInput.PostSubordinatePeriod;
			var children = (float)pensionInput.ChildSupport;

			float percentage = GetPercentage(birthYear);

			// Limit to the best 360 months
			var sum = m1 + m2 + m3;
			var dif = 0.0f;
			if (sum > 360)
			{
				dif = sum - 360;
			}
			if (dif > m3)
			{
				dif = dif - m3;
				m3 = 0;
				if (dif > m2)
				{
					m1 = 360;
					m2 = 0;
				}
				else
				{
					m2 = m2 - dif;
				}
			}
			else
			{
				m3 = m3 - dif;
			}

			// Temporary amount based on sailing
			var superiorAmount = m1 * s1;
			var subordinateAmount = m2 * s2 + m3 * s3;
			superiorAmount = superiorAmount.toFixed(2);
			subordinateAmount = subordinateAmount.toFixed(2);

			// Child support
			var childAmount = ((superiorAmount + subordinateAmount) * children / 10f).toFixed(2);

			// Pension
			var pensionAmount = ((superiorAmount + subordinateAmount + childAmount) * percentage / 100f).toFixed(2);

			// Pension per month per year
			var annualIncomeSampledValue = 0f;
			var monthlyIncomeSampledValue = 0f;
			var annualRegularValue = 0f;
			var monthlyRegularValue = 0f;

			if (sum >= 150)
			{
				annualRegularValue = superiorAmount + subordinateAmount + childAmount;
				monthlyRegularValue = annualRegularValue / 12f;
				annualIncomeSampledValue = superiorAmount + subordinateAmount + childAmount + pensionAmount;
				monthlyIncomeSampledValue = annualIncomeSampledValue / 12f;
			}

			// Payback of pension fee
			var paybackValue = 0f;
			if (sum >= 36 && sum <= 149)
			{
				paybackValue = (superiorAmount + subordinateAmount) * 2f / 3f;
			}

			field1_mo.Text = FormatCurrency(monthlyIncomeSampledValue);
			field1_yr.Text = FormatCurrency(annualIncomeSampledValue);

			field2_mo.Text = FormatCurrency(monthlyRegularValue);
			field2_yr.Text = FormatCurrency(annualRegularValue);

			refund.Text = FormatCurrency(paybackValue);

			calcField1.Text = string.Format(calcField1.Text, percentage);

			/*System.Diagnostics.Debug.WriteLine("s1:" + s1);
			System.Diagnostics.Debug.WriteLine("s2:" + s2);
			System.Diagnostics.Debug.WriteLine("s3:" + s3);
			System.Diagnostics.Debug.WriteLine("percentage:" + percentage);
			System.Diagnostics.Debug.WriteLine("m1:" + m1);
			System.Diagnostics.Debug.WriteLine("m2:" + m2);
			System.Diagnostics.Debug.WriteLine("m3:" + m3);
			System.Diagnostics.Debug.WriteLine("children:" + children);
			System.Diagnostics.Debug.WriteLine("sum:" + sum);
			System.Diagnostics.Debug.WriteLine("dif:" + dif);
			System.Diagnostics.Debug.WriteLine("superiorAmount:" + superiorAmount);
			System.Diagnostics.Debug.WriteLine("subordinateAmount:" + subordinateAmount);
			System.Diagnostics.Debug.WriteLine("childAmount:" + childAmount);
			System.Diagnostics.Debug.WriteLine("pensionAmount:" + pensionAmount);
			System.Diagnostics.Debug.WriteLine("annualIncomeSampledValue:" + annualIncomeSampledValue);
			System.Diagnostics.Debug.WriteLine("monthlyIncomeSampledValue:" + monthlyIncomeSampledValue);
			System.Diagnostics.Debug.WriteLine("annualRegularValue:" + annualRegularValue);
			System.Diagnostics.Debug.WriteLine("monthlyRegularValue:" + monthlyRegularValue);
			System.Diagnostics.Debug.WriteLine("paybackValue:" + paybackValue);*/
		}

		public float GetPercentage(int birthYear)
		{
			int percentage = 0;
			
			// Calculate percentage
			if 		(birthYear >= 1934 && birthYear <= 1942) percentage = 23;
			else if (birthYear >= 1943 && birthYear <= 1945) percentage = 20;
			else if (birthYear >= 1946 && birthYear <= 1948) percentage = 17;
			else if (birthYear >= 1949 && birthYear <= 1951) percentage = 14;
			else if (birthYear >= 1952 && birthYear <= 1954) percentage = 11;
			else if (birthYear >= 1955 && birthYear <= 1959) percentage = 8;
			else if (birthYear >= 1960 && birthYear <= 1964) percentage = 5;
			else 											 percentage = 0;

			return (float)percentage;
		}

		public string FormatCurrency(float amount)
		{
			return string.Format("{0,12:N0}", amount);
		}
	}
}