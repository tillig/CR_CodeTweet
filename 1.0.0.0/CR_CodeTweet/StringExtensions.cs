using System;
using System.Security.Cryptography;
using System.Text;

namespace CR_CodeTweet
{
	/// <summary>
	/// Extension methods for <see cref="System.String"/>.
	/// </summary>
	public static class StringExtensions
	{
		/// <summary>
		/// The list of characters that should not be encoded. This is defined in the OAuth spec.
		/// </summary>
		public const string SafeUrlCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~";

		/// <summary>
		/// Encrypts a string at the current user's scope using DPAPI.
		/// </summary>
		/// <param name="toEncrypt">The string to encrypt.</param>
		/// <param name="key">The key used to encrypt it.</param>
		/// <returns>
		/// The encrypted bytes that only the current user, with the specified key,
		/// can decrypt again.
		/// </returns>
		/// <seealso cref="CR_CodeTweet.ByteExtensions.Decrypt" />
		public static byte[] Encrypt(this string toEncrypt, string key)
		{
			if (toEncrypt == null)
			{
				throw new ArgumentNullException("toEncrypt");
			}
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (key.Length == 0)
			{
				throw new ArgumentException("Key may not be empty.", "key");
			}
			byte[] entropy = Encoding.Unicode.GetBytes(key);
			byte[] toEncryptBytes = Encoding.Unicode.GetBytes(toEncrypt);
			byte[] encrypted = ProtectedData.Protect(toEncryptBytes, entropy, DataProtectionScope.CurrentUser);
			return encrypted;
		}

		/// <summary>
		/// URL-encodes a string for use with OAuth.
		/// </summary>
		/// <param name="toEncode">
		/// The string to encode.
		/// </param>
		/// <returns>
		/// A URL-encoded string where all of the encoded characters are upper-case.
		/// </returns>
		/// <remarks>
		/// This is a different URL-encoding implementation since the default
		/// .NET one outputs the percent encoding in lower case. While this is
		/// not a problem with the percent encoding spec, it is used in
		/// upper-case throughout OAuth (http://oauth.net/core/1.0a#encoding_parameters).
		/// </remarks>
		/// <exception cref="System.ArgumentNullException">
		/// Thrown if <paramref name="toEncode" /> is <see langword="null" />.
		/// </exception>
		public static string UrlEncodeForOAuth(this string toEncode)
		{
			if (toEncode == null)
			{
				throw new ArgumentNullException("toEncode");
			}
			StringBuilder result = new StringBuilder();
			foreach (char symbol in toEncode)
			{
				if (SafeUrlCharacters.IndexOf(symbol) != -1)
				{
					result.Append(symbol);
				}
				else
				{
					result.AppendFormat("%{0:X2}", (int)symbol);
				}
			}
			return result.ToString();
		}
	}
}
