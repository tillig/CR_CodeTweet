using System;
using System.Collections.Generic;

namespace CR_CodeTweet
{
	/// <summary>
	/// Extension methods for <see cref="System.Collections.Generic.Dictionary{K,V}"/>.
	/// </summary>
	public static class DictionaryExtensions
	{
		/// <summary>
		/// Converts a <see cref="System.Collections.Generic.Dictionary{K,V}"/> to a querystring.
		/// </summary>
		/// <param name="toEncode">The <see cref="System.Collections.Generic.Dictionary{K,V}"/> of string keys/values.</param>
		/// <returns>
		/// A <see cref="System.String"/> to convert into querystring format. If
		/// the dictionary is empty or <see langword="null" />, this will be
		/// an empty string.
		/// </returns>
		public static string ToQueryString(this IDictionary<string, string> toEncode)
		{
			if (toEncode == null || toEncode.Keys.Count == 0)
			{
				return "";
			}
			List<string> pairs = new List<string>();
			foreach (string key in toEncode.Keys)
			{
				pairs.Add(key.UrlEncodeForOAuth() + "=" + toEncode[key].UrlEncodeForOAuth());
			}
			return String.Join("&", pairs.ToArray());
		}
	}
}
