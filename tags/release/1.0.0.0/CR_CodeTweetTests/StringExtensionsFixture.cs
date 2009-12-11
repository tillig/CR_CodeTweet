using System;
using CR_CodeTweet;
using NUnit.Framework;

namespace CR_CodeTweetTests
{
	[TestFixture]
	public class StringExtensionsFixture
	{
		[Test(Description = "Attempts to encrypt a string with an empty key.")]
		public void Encrypt_EmptyKey()
		{
			string toEncrypt = "Original Content";
			Assert.Throws<ArgumentException>(() => toEncrypt.Encrypt(""));
		}

		[Test(Description = "Attempts to encrypt a string with a null key.")]
		public void Encrypt_NullKey()
		{
			string toEncrypt = "Original Content";
			Assert.Throws<ArgumentNullException>(() => toEncrypt.Encrypt(null));
		}

		[Test(Description = "Attempts to encrypt a null string.")]
		public void Encrypt_NullString()
		{
			string toEncrypt = null;
			Assert.Throws<ArgumentNullException>(() => toEncrypt.Encrypt("key"));
		}

		[Test(Description = "Encrypts and subsequently decrypts a string.")]
		public void Encrypt_RoundTrip()
		{
			string expected = "Original Content";
			string key = "Secret Key";
			byte[] encrypted = expected.Encrypt(key);
			string decrypted = encrypted.Decrypt(key);
			Assert.AreEqual(expected, decrypted, "The string did not correctly round-trip through encryption.");
		}

		[Test(Description = "Encodes a value where all of the characters are URL-safe.")]
		public void UrlEncodeForOAuth_AllCharactersSafe()
		{
			string toEncode = "ThisIsATest.html";
			string actual = toEncode.UrlEncodeForOAuth();
			Assert.AreEqual(toEncode, actual, "If all of the characters are safe, no encoding should occur.");
		}

		[Test(Description = "Encodes a string where all of the characters get encoded.")]
		public void UrlEncodeForOAuth_AllCharactersUnsafe()
		{
			string toEncode = "!@#$%^&*():\"'{}[]";
			string expected = "%21%40%23%24%25%5E%26%2A%28%29%3A%22%27%7B%7D%5B%5D";
			string actual = toEncode.UrlEncodeForOAuth();
			Assert.AreEqual(expected, actual, "The characters were not properly encoded.");
		}

		[Test(Description = "Encodes an empty string.")]
		public void UrlEncodeForOAuth_EmptyString()
		{
			string toEncode = "";
			string actual = toEncode.UrlEncodeForOAuth();
			Assert.AreEqual("", actual, "Empty string encoded should be empty.");
		}

		[Test(Description = "Encodes a string where some characters get encoded and some don't.")]
		public void UrlEncodeForOAuth_MixedCharacterSet()
		{
			string toEncode = "This is a test & it should % be # encoded.!@#$%^&*():\"'{}[]";
			string expected = "This%20is%20a%20test%20%26%20it%20should%20%25%20be%20%23%20encoded.%21%40%23%24%25%5E%26%2A%28%29%3A%22%27%7B%7D%5B%5D";
			string actual = toEncode.UrlEncodeForOAuth();
			Assert.AreEqual(expected, actual, "The characters were not properly encoded.");
		}

		[Test(Description = "Attempts to encode a null string.")]
		public void UrlEncodeForOAuth_NullString()
		{
			string toEncode = null;
			Assert.Throws<ArgumentNullException>(() => toEncode.UrlEncodeForOAuth());
		}
	}
}
