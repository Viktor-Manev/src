using System.Collections;
using System.Collections.Generic;

public static class DictionaryExt
{
	public static void TryAdd(this Dictionary<string, string> dict, string key, string value)
	{
		if (!dict.ContainsKey(key))
		{
			dict.Add(key, value);
		}
#if DEBUG
		else
		{
			System.Diagnostics.Debug.WriteLine("Dictionary: " + key + " already contains a key for " + value.ToString() );
		}
#endif
	}

	public static void TryAddGeneric<T>(this Dictionary<string, T> dict, string key, T value)
	{
		if (!dict.ContainsKey(key))
		{
			dict.Add(key, value);
		}
#if DEBUG
		else
		{
			System.Diagnostics.Debug.WriteLine("Dictionary: " + key + " already contains a key for " + value.ToString() );
		}
#endif
	}
}
