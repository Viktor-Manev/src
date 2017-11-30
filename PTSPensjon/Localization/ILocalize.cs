using System.Globalization;

namespace PTSPensjon
{
    public interface ILocalize
	{
		CultureInfo GetCurrentCultureInfo();
		void SetLocale(CultureInfo ci);
	}
}