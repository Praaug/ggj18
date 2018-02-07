/* taken from https://stackoverflow.com/a/36244111/3711593 */

using UnityEngine;

public static class JsonHelper
{
	public static T[] FromJson<T>(string json)
	{
		Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
		return wrapper.Items;
	}

	public static string ToJson<T>(T[] array)
	{
		Wrapper<T> wrapper = new Wrapper<T>();
		wrapper.Items = array;
		return JsonUtility.ToJson(wrapper);
	}

	public static string ToJson<T>(T[] array, bool prettyPrint)
	{
		Wrapper<T> wrapper = new Wrapper<T>();
		wrapper.Items = array;
		return JsonUtility.ToJson(wrapper, prettyPrint);
	}

	public static string FixJson(string value)
	{
		value = value.Replace('\'', '\"');
		value = "{\"Items\":" + value + "}";
		return value;
	}

	[System.Serializable]
	private class Wrapper<T>
	{
		public T[] Items;
	}
}
