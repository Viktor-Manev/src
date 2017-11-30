using System;
using Xamarin.Forms;
using PTSPensjon.HTML;

namespace PTSPensjon
{
	public class TextService
	{
		const double WIDTH_PADDING = 20.0f * 2.0f;

		public TextService() {}

		public static double MeasureTextSize(string text, double width, double fontSize, string fontFamily)
		{
			/*var m_width = App.ScreenWidth;
			fontFamily = "HelveticaNeue-Bold";

			var plainText = HTMLUtils.ConvertHTMLToPlainText(text);
			plainText = plainText.TrimEnd( new char[] { ' ', '\n', '\t' } );
			//System.Diagnostics.Debug.WriteLine("1: " + plainText[plainText.Length - 1] + " | 2: " + plainText[plainText.Length - 2] + " | 3: " + plainText[plainText.Length - 3]);

			//System.Diagnostics.Debug.WriteLine("Label. m_width: " + m_width + " | App.ScreenWidth: " + (App.ScreenWidth - WIDTH_PADDING) + " | FontFamily: " + fontFamily +
			//                                   " | HtmlText (plain): " + plainText);

			return DependencyService.Get<ITextMeter>().MeasureTextSize(HTMLUtils.ConvertHTMLToPlainText(text), m_width, fontSize, fontFamily);*/
			return 0;
		}
	}
}
