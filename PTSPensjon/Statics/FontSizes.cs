using System;
using Xamarin.Forms;

namespace PTSPensjon.Statics
{
	public enum FontSize
	{
		Small = 100,
		Medium = 125,
		Large = 150,
	}

    public static class FontSizes
    {
		const string FONT_SIZE_KEY = "FontSize";
		
		public readonly static int TITLE_FONT_SIZE_BASE = 32;
		public readonly static int HEADER_FONT_SIZE_BASE = 24;
		public readonly static int DESC_FONT_SIZE_BASE = 16;
		public readonly static int LABEL_FONT_SIZE_BASE = 14;

		//News page
		public readonly static int NEWS_PUBLISHED_FONT_SIZE_BASE = 8;
		public readonly static int NEWS_DESC_FONT_SIZE_BASE = 10;

		public readonly static double SMALL_FONT_SIZE_MULTIPLIER  = 0.75f;
		public readonly static double MEDIUM_FONT_SIZE_MULTIPLIER = 1.0f;
		public readonly static double LARGE_FONT_SIZE_MULTIPLIER  = 1.25f;

		public static double TitleSize = 32d;
		public static double HeaderSize = 24d;
		public static double DescSize = 16d;
		public static double LabelSize = 14d;

		public static double NewsPublishedSize = 8d;
		public static double NewsDescSize = 10d;

		static FontSizes()
		{
			try
			{
				int fontSize = 1;
				int.TryParse( PlayerPrefs.GetString(FONT_SIZE_KEY), out fontSize );
				ReCalculateFontSizes( (FontSize)fontSize );
			}
			catch(Exception e)
			{
				System.Diagnostics.Debug.WriteLine("FontSizes constructor. e.ToString(): " + e.ToString());
			}
		}

		public static void ReCalculateFontSizes(FontSize fontSize)
		{
			PlayerPrefs.SetString(FONT_SIZE_KEY, ( (int)fontSize ).ToString());

			if (fontSize == FontSize.Small)  SetFontSizes(SMALL_FONT_SIZE_MULTIPLIER);
			if (fontSize == FontSize.Medium) SetFontSizes(MEDIUM_FONT_SIZE_MULTIPLIER);
			if (fontSize == FontSize.Large)  SetFontSizes(LARGE_FONT_SIZE_MULTIPLIER);
		}

		private static void SetFontSizes(double multiplier)
		{
			TitleSize 	= (double)TITLE_FONT_SIZE_BASE  * multiplier;
			HeaderSize 	= (double)HEADER_FONT_SIZE_BASE * multiplier;
			DescSize 	= (double)DESC_FONT_SIZE_BASE   * multiplier;
			LabelSize 	= (double)LABEL_FONT_SIZE_BASE  * multiplier;

			NewsPublishedSize 	= (double)NEWS_PUBLISHED_FONT_SIZE_BASE * multiplier;
			NewsDescSize 		= (double)NEWS_DESC_FONT_SIZE_BASE * multiplier;
		}
    }
}

