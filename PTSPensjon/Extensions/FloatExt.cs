using System;

namespace PTSPensjon
{
	public static class FloatExt
	{
		public static float toFixed(this float n, int decimals)
		{
			return (float)Math.Round( (decimal)n, decimals);
		}
	}
}
