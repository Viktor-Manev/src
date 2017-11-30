using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PTSPensjon
{
	// You exclude the 'Extension' suffix when using in Xaml markup
	[ContentProperty("Text")]
	public class TranslateExtension : IMarkupExtension
	{
		public CultureInfo ci;
		const string ResourceId = "PTSPensjon.Resx.TextResources";

		const string F9pDoubleNewline = "*n2*";
		const string F9pNewlineReplacement = "</br>";

		public TranslateExtension()
		{
			var m_ci = PlayerPrefs.GetString("currentCulture");
			if (string.IsNullOrEmpty(m_ci))
			{
				ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
				PlayerPrefs.SetString("currentCulture", ci.ToString());
			}
			else
			{
				ci = new CultureInfo(m_ci);
			}
		}

		public string Text { get; set; }

		public object ProvideValue(IServiceProvider serviceProvider)
		{
            if (Text == null)
                return string.Empty;

            ResourceManager resmgr = new ResourceManager(ResourceId, typeof(TranslateExtension).GetTypeInfo().Assembly);

			var translation = resmgr.GetString(Text, ci);

			//System.Diagnostics.Debug.WriteLine("Translation: " + translation + " | cultureInfo: " + ci);
			if (translation == null)
			{
#if DEBUG
				throw new ArgumentException(
					String.Format("Key '{0}' was not found in resources '{1}' for culture '{2}'.", Text, resmgr.GetType() /*ResourceId*/, ci.Name),
					"Text");
#else
                translation = Text; // HACK: returns the key, which GETS DISPLAYED TO THE USER
#endif
			}

			if (translation.Contains(F9pDoubleNewline))
			{
				//System.Diagnostics.Debug.WriteLine("Providing translation. Was: " + translation);
				return translation.Replace(F9pDoubleNewline, F9pNewlineReplacement + F9pNewlineReplacement);
			}
			return translation;
		}
	}
}