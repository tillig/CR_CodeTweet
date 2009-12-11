using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.CodeRush.Common;
using DevExpress.CodeRush.Core;

namespace CR_CodeTweet
{
	/// <summary>
	/// Extension methods for <see cref="DevExpress.CodeRush.Core.DecoupledStorage"/>.
	/// </summary>
	public static class DecoupledStorageExtensions
	{
		/// <summary>
		/// Flushes all changes being buffered by the decoupled storage object.
		/// </summary>
		/// <param name="storage">
		/// The storage to which changes are being buffered.
		/// </param>
		/// <remarks>
		/// <para>
		/// This method is needed because the <see cref="DevExpress.CodeRush.Common.IDecoupledStorage"/>
		/// interface does not have the <see cref="DevExpress.CodeRush.Core.DecoupledStorage.UpdateStorage"/>
		/// method but you need to call that method to ensure changes get updated
		/// real-time.
		/// </para>
		/// <para>
		/// Calling this method on a <see langword="null" /> object or on something
		/// not a <see cref="DevExpress.CodeRush.Core.DecoupledStorage"/> will
		/// have no effect.
		/// </para>
		/// </remarks>
		public static void Update(this IDecoupledStorage storage)
		{
			DecoupledStorage cast = storage as DecoupledStorage;
			if (cast != null)
			{
				cast.UpdateStorage();
			}
		}

		/// <summary>
		/// Encrypts a string for the given user and writes it to storage.
		/// </summary>
		/// <param name="storage">The storage to which the string should be written.</param>
		/// <param name="section">The section to which the string should be written.</param>
		/// <param name="key">The key to both encrypt the string with and store the string under.</param>
		/// <param name="value">The value to encrypt and store.</param>
		public static void WriteEncryptedString(this IDecoupledStorage storage, string section, string key, string value)
		{
			if (storage == null)
			{
				throw new ArgumentNullException("storage");
			}
			if (section == null)
			{
				throw new ArgumentNullException("section");
			}
			if (section.Length == 0)
			{
				throw new ArgumentException("Section may not be empty.", "section");
			}
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (key.Length == 0)
			{
				throw new ArgumentException("Key may not be empty.", "key");
			}
			string encrypted = "";
			if (!String.IsNullOrEmpty(value))
			{
				encrypted = Convert.ToBase64String(value.Encrypt(key));
			}
			storage.WriteString(section, key, encrypted);
		}

		/// <summary>
		/// Reads a string for the given user from storage and decrypts it.
		/// </summary>
		/// <param name="storage">The storage from which the string should be read.</param>
		/// <param name="section">The section from which the string should be read.</param>
		/// <param name="key">The key to both decrypt the string with and read the string from.</param>
		/// <param name="defaultValue">The value to return if the string isn't found or otherwise can't be decrypted.</param>
		public static string ReadEncryptedString(this IDecoupledStorage storage, string section, string key, string defaultValue)
		{
			if (storage == null)
			{
				throw new ArgumentNullException("storage");
			}
			if (section == null)
			{
				throw new ArgumentNullException("section");
			}
			if (section.Length == 0)
			{
				throw new ArgumentException("Section may not be empty.", "section");
			}
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			if (key.Length == 0)
			{
				throw new ArgumentException("Key may not be empty.", "key");
			}

			string stored = storage.ReadString(section, key, defaultValue);
			if (stored == defaultValue)
			{
				// The value wasn't found so just return the default value.
				return defaultValue;
			}
			else if (stored.Length == 0)
			{
				// Empty strings don't get decrypted.
				return stored;
			}
			try
			{
				// The value was found; decrypt it.
				byte[] toDecrypt = Convert.FromBase64String(stored);
				string decrypted = toDecrypt.Decrypt(key);
				return decrypted;
			}
			catch
			{
				// There was a problem decrypting so just return the default value.
				return defaultValue;
			}
		}
	}
}
