using System;
using System.Security.Cryptography;
using System.Text;

namespace CR_CodeTweet
{
	/// <summary>
	/// Extension methods for <see cref="System.Byte"/>.
	/// </summary>
	public static class ByteExtensions
	{
		/// <summary>
		/// Decrypts a string at the current user's scope using DPAPI.
		/// </summary>
		/// <param name="toDecrypt">The encrypted string.</param>
		/// <param name="key">The key used to encrypt it.</param>
		/// <returns>
		/// The string resulting from decrypting the bytes at the current
		/// user's scope using the given key.
		/// </returns>
		/// <seealso cref="CR_CodeTweet.StringExtensions.Encrypt" />
		public static string Decrypt(this byte[] toDecrypt, string key)
		{
			if (toDecrypt == null)
			{
				throw new ArgumentNullException("toDecrypt");
			}
			if (toDecrypt.Length == 0)
			{
				throw new ArgumentException("Encrypted content may not be empty.", "toDecrypt");
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
			byte[] decryptedBytes = ProtectedData.Unprotect(toDecrypt, entropy, DataProtectionScope.CurrentUser);
			string decrypted = Encoding.Unicode.GetString(decryptedBytes);
			return decrypted;
		}
	}
}
