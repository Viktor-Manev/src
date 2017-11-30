﻿using System;

namespace PTSPensjon
{
    public static class OperatorLocalizationWidget
    {
		public static string GetOperatorTimeLocalization(DateTime dateTime)
        {
			//System.Diagnostics.Debug.WriteLine("DateTime: " + dateTime.ToString() + " | dateTime.DayOfYear: " + dateTime.DayOfYear + " | Day of year #1: " + new DateTime(dateTime.Year, 5, 15).DayOfYear + " | Day of year #2: " + new DateTime(dateTime.Year, 9, 14).DayOfYear);

            if (dateTime.DayOfYear >= new DateTime(dateTime.Year, 5, 15).DayOfYear &&
                dateTime.DayOfYear <= new DateTime(dateTime.Year, 9, 14).DayOfYear)
            {
				return Localization.Get("Help_calltimes_summer");
            }
			return Localization.Get("Help_calltimes_winter");
        }
    }
}
