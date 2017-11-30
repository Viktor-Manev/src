using System;
using System.Collections;
using Newtonsoft.Json;

using System.Diagnostics;

public static class JsonDotNetExt
{
	readonly static JsonSerializerSettings SerializerSettings = new JsonSerializerSettings();

	public static string ToJson(this object obj)
	{
		if (obj == null) return string.Empty;

		try
		{
			return JsonConvert.SerializeObject(obj, Formatting.Indented);
		}
		catch (Exception e)
		{
			Debug.WriteLine("ToJson. InvalidJSON e: " + e + ": " + obj);
			return string.Empty;
		}
	}

	public static T JsonToObject<T>(this string json)
	{
		try
		{
			return JsonConvert.DeserializeObject<T>(json, SerializerSettings);
		}
		catch (Exception e)
		{
			Debug.WriteLine("JsonToObject. InvalidJSON. e: " + e + ": " + json);
			return default(T);
		}
	}
}
