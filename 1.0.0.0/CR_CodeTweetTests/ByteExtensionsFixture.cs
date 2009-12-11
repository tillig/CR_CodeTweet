using System;
using System.Text;
using CR_CodeTweet;
using NUnit.Framework;

namespace CR_CodeTweetTests
{
	[TestFixture]
	public class ByteExtensionsFixture
	{
		[Test(Description = "Attempts to decrypt a string with an empty key.")]
		public void Decrypt_EmptyKey()
		{
			byte[] toDecrypt = Encoding.Unicode.GetBytes("Encrypted Content");
			Assert.Throws<ArgumentException>(() => toDecrypt.Decrypt(""));
		}

		[Test(Description = "Attempts to decrypt a string with a null key.")]
		public void Decrypt_NullKey()
		{
			byte[] toDecrypt = Encoding.Unicode.GetBytes("Encrypted Content");
			Assert.Throws<ArgumentNullException>(() => toDecrypt.Decrypt(null));
		}

		[Test(Description = "Attempts to decrypt an empty value.")]
		public void Decrypt_EmptyByteArray()
		{
			byte[] toDecrypt = new byte[0];
			Assert.Throws<ArgumentException>(() => toDecrypt.Decrypt("key"));
		}

		[Test(Description = "Attempts to decrypt a null value.")]
		public void Decrypt_NullByteArray()
		{
			byte[] toDecrypt = null;
			Assert.Throws<ArgumentNullException>(() => toDecrypt.Decrypt("key"));
		}
	}
}
