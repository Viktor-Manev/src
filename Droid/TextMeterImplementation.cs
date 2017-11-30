using System;
using Android.Widget;
using Android.Views;
using Android.Graphics;
using Android.Util;
using Android.Text;
using PTSPensjon.Droid;
using Xamarin.Forms;
using Views = Android.Views;

[assembly: Xamarin.Forms.Dependency(typeof(TextMeterImplementation))]
namespace PTSPensjon.Droid
{
	public class TextMeterImplementation : ITextMeter
	{
		private Typeface textTypeface;

		public double MeasureTextSize(string text, double width, double fontSize, string fontName)
		{
			var textView = new TextView(global::Android.App.Application.Context);
			textView.Typeface = GetTypeface(fontName);
			textView.SetText(text, TextView.BufferType.Normal);
			textView.SetTextSize(ComplexUnitType.Px, (float)fontSize);
			//textView.SetTextSize(ComplexUnitType.Dip, (float)fontSize);

			int widthMeasureSpec = (int)/*App.ScreenWidth; //*/Views.View.MeasureSpec.MakeMeasureSpec((int)width, MeasureSpecMode.AtMost);
			int heightMeasureSpec =(int)/*App.ScreenHeight;//*/Views.View.MeasureSpec.MakeMeasureSpec(0, MeasureSpecMode.Unspecified);

			textView.Measure(widthMeasureSpec, heightMeasureSpec);

			//var density = global::Android.App.Application.Context.Resources.DisplayMetrics.Density;
			//return ((double)textView.MeasuredHeight / density) + 5;

			return (double)textView.MeasuredHeight + 5;
		}

		private Typeface GetTypeface(string fontName)
		{
			if (fontName == null)
			{
				return Typeface.Default;
			}

			if (textTypeface == null)
			{
				textTypeface = Typeface.Create(fontName, TypefaceStyle.Normal);
			}

			return textTypeface;
		}
	}
}