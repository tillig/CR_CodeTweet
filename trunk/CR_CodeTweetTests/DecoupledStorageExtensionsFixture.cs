using System;
using System.Collections.Generic;
using CR_CodeTweet;
using DevExpress.CodeRush.Common;
using NUnit.Framework;

namespace CR_CodeTweetTests
{
	[TestFixture]
	public class DecoupledStorageExtensionsFixture
	{
		[Test(Description = "Attempts to read an empty key from storage.")]
		public void ReadEncryptedString_KeyEmpty()
		{
			TestStorage storage = new TestStorage();
			Assert.Throws<ArgumentException>(() => storage.ReadEncryptedString("section", "", "value"));
		}

		[Test(Description = "Attempts to read a null key from storage.")]
		public void ReadEncryptedString_KeyNull()
		{
			TestStorage storage = new TestStorage();
			Assert.Throws<ArgumentNullException>(() => storage.ReadEncryptedString("section", null, "value"));
		}

		[Test(Description = "Attempts to read an empty section from storage.")]
		public void ReadEncryptedString_SectionEmpty()
		{
			TestStorage storage = new TestStorage();
			Assert.Throws<ArgumentException>(() => storage.ReadEncryptedString("", "key", "value"));
		}

		[Test(Description = "Attempts to read a null section from storage.")]
		public void ReadEncryptedString_SectionNull()
		{
			TestStorage storage = new TestStorage();
			Assert.Throws<ArgumentNullException>(() => storage.ReadEncryptedString(null, "key", "value"));
		}

		[Test(Description = "Attempts to read from a null storage object.")]
		public void ReadEncryptedString_StorageNull()
		{
			IDecoupledStorage storage = null;
			Assert.Throws<ArgumentNullException>(() => storage.ReadEncryptedString("section", "key", "value"));
		}

		[Test(Description = "Reads a value from storage that isn't there.")]
		public void ReadEncryptedString_ValueDefaultIfNotFound()
		{
			TestStorage storage = new TestStorage();
			string actual = storage.ReadEncryptedString("section", "key", "defaultValue");
			Assert.AreEqual("defaultValue", actual, "The default value should come back if the value isn't found.");
		}

		[Test(Description = "Reads an empty value from storage.")]
		public void ReadEncryptedString_ValueEmpty()
		{
			TestStorage storage = new TestStorage();
			storage.StorageBase.Add("section", new Dictionary<string, string>());
			storage.StorageBase["section"].Add("key", "");
			string actual = storage.ReadEncryptedString("section", "key", "defaultValue");
			Assert.AreEqual("", actual, "An empty string should be retrieved correctly.");
		}

		[Test(Description = "Reads value from storage that isn't correctly encrypted.")]
		public void ReadEncryptedString_ValueNotCorrectlyEncrypted()
		{
			TestStorage storage = new TestStorage();
			storage.StorageBase.Add("section", new Dictionary<string, string>());
			storage.StorageBase["section"].Add("key", "foo");
			string actual = storage.ReadEncryptedString("section", "key", "defaultValue");
			Assert.AreEqual("defaultValue", actual, "If the value can't be decrypted the default value should be returned.");
		}

		[Test(Description = "Round-trips an encrypted, non-empty value through storage.")]
		public void ReadEncryptedString_ValueRoundTrip()
		{
			TestStorage storage = new TestStorage();
			string expected = "non-empty value";
			storage.WriteEncryptedString("section", "key", expected);
			string actual = storage.ReadEncryptedString("section", "key", "defaultValue");
			Assert.AreEqual(expected, actual, "A non-empty string should round-trip through the system correctly.");
		}

		[Test(Description = "Calls Update on an object that isn't a built-in DecoupledStorage object.")]
		public void Update_NotDecoupledStorage()
		{
			TestStorage storage = new TestStorage();
			storage.Update(); // Should not throw an exception.
		}

		[Test(Description = "Calls Update on a null storage object.")]
		public void Update_NullStorage()
		{
			IDecoupledStorage storage = null;
			storage.Update(); // Should not throw an exception.
		}

		[Test(Description = "Writes an empty value to storage.")]
		public void WriteEncryptedString_ValueEmpty()
		{
			TestStorage storage = new TestStorage();
			storage.WriteEncryptedString("section", "key", "");
			Assert.AreEqual("", storage.StorageBase["section"]["key"], "An empty string should be stored as empty.");
		}

		[Test(Description = "Writes a non-empty value to storage.")]
		public void WriteEncryptedString_ValuePopulated()
		{
			TestStorage storage = new TestStorage();
			string original = "non-empty value";
			storage.WriteEncryptedString("section", "key", original);
			string actual = storage.StorageBase["section"]["key"];
			Assert.IsNotNull(actual, "The stored value should not be null.");
			Assert.AreNotEqual(original, actual, "An encrypted value should be stored.");
		}

		[Test(Description = "Attempts to write an empty key to storage.")]
		public void WriteEncryptedString_KeyEmpty()
		{
			TestStorage storage = new TestStorage();
			Assert.Throws<ArgumentException>(() => storage.WriteEncryptedString("section", "", "value"));
		}

		[Test(Description = "Attempts to write a null key to storage.")]
		public void WriteEncryptedString_KeyNull()
		{
			TestStorage storage = new TestStorage();
			Assert.Throws<ArgumentNullException>(() => storage.WriteEncryptedString("section", null, "value"));
		}

		[Test(Description = "Attempts to write an empty section to storage.")]
		public void WriteEncryptedString_SectionEmpty()
		{
			TestStorage storage = new TestStorage();
			Assert.Throws<ArgumentException>(() => storage.WriteEncryptedString("", "key", "value"));
		}

		[Test(Description = "Attempts to write a null section to storage.")]
		public void WriteEncryptedString_SectionNull()
		{
			TestStorage storage = new TestStorage();
			Assert.Throws<ArgumentNullException>(() => storage.WriteEncryptedString(null, "key", "value"));
		}

		[Test(Description = "Attempts to write to a null storage object.")]
		public void WriteEncryptedString_StorageNull()
		{
			IDecoupledStorage storage = null;
			Assert.Throws<ArgumentNullException>(() => storage.WriteEncryptedString("section", "key", "value"));
		}

		private class TestStorage : IDecoupledStorage
		{
			public Dictionary<string, Dictionary<string, string>> StorageBase = new Dictionary<string, Dictionary<string, string>>();

			public string[] GetKeys(string section)
			{
				throw new NotImplementedException();
			}

			public string[] GetSections()
			{
				throw new NotImplementedException();
			}

			public bool ReadBoolean(string section, string key, bool defaultValue)
			{
				throw new NotImplementedException();
			}

			public bool ReadBoolean(string section, string key)
			{
				throw new NotImplementedException();
			}

			public char ReadChar(string section, string key, char defaultValue)
			{
				throw new NotImplementedException();
			}

			public char ReadChar(string section, string key)
			{
				throw new NotImplementedException();
			}

			public System.Drawing.Color ReadColor(string section, string key, System.Drawing.Color defaultValue)
			{
				throw new NotImplementedException();
			}

			public System.Drawing.Color ReadColor(string section, string key)
			{
				throw new NotImplementedException();
			}

			public DateTime ReadDateTime(string section, string key, DateTime defaultValue)
			{
				throw new NotImplementedException();
			}

			public DateTime ReadDateTime(string section, string key)
			{
				throw new NotImplementedException();
			}

			public double ReadDouble(string section, string key, double defaultValue)
			{
				throw new NotImplementedException();
			}

			public double ReadDouble(string section, string key)
			{
				throw new NotImplementedException();
			}

			public object ReadEnum(string section, string key, Type enumType, object defaultValue)
			{
				throw new NotImplementedException();
			}

			public int ReadInt32(string section, string key, int defaultValue)
			{
				throw new NotImplementedException();
			}

			public int ReadInt32(string section, string key)
			{
				throw new NotImplementedException();
			}

			public float ReadSingle(string section, string key, float defaultValue)
			{
				throw new NotImplementedException();
			}

			public float ReadSingle(string section, string key)
			{
				throw new NotImplementedException();
			}

			public string ReadString(string section, string key, string defaultValue, bool encoded)
			{
				throw new NotImplementedException();
			}

			public string ReadString(string section, string key, string defaultValue)
			{
				try
				{
					return this.StorageBase[section][key];
				}
				catch (KeyNotFoundException)
				{
					return defaultValue;
				}
			}

			public string ReadString(string section, string key, bool encoded)
			{
				throw new NotImplementedException();
			}

			public string ReadString(string section, string key)
			{
				throw new NotImplementedException();
			}

			public string[] ReadStrings(string section, string key, string[] defaultValue, bool encoded)
			{
				throw new NotImplementedException();
			}

			public string[] ReadStrings(string section, string key, string[] defaultValue)
			{
				throw new NotImplementedException();
			}

			public string[] ReadStrings(string section, string key, bool encoded)
			{
				throw new NotImplementedException();
			}

			public string[] ReadStrings(string section, string key)
			{
				throw new NotImplementedException();
			}

			public void WriteBoolean(string section, string key, bool value)
			{
				throw new NotImplementedException();
			}

			public void WriteChar(string section, string key, char value)
			{
				throw new NotImplementedException();
			}

			public void WriteColor(string section, string key, System.Drawing.Color value)
			{
				throw new NotImplementedException();
			}

			public void WriteDateTime(string section, string key, DateTime value)
			{
				throw new NotImplementedException();
			}

			public void WriteDouble(string section, string key, double value)
			{
				throw new NotImplementedException();
			}

			public void WriteEnum(string section, string key, object value)
			{
				throw new NotImplementedException();
			}

			public void WriteInt32(string section, string key, int value)
			{
				throw new NotImplementedException();
			}

			public void WriteSingle(string section, string key, float value)
			{
				throw new NotImplementedException();
			}

			public void WriteString(string section, string key, string value, bool encoded)
			{
				throw new NotImplementedException();
			}

			public void WriteString(string section, string key, string value)
			{
				if (!this.StorageBase.ContainsKey(section))
				{
					this.StorageBase.Add(section, new Dictionary<string, string>());
				}
				this.StorageBase[section].Add(key, value);
			}

			public void WriteStrings(string section, string key, string[] value, bool encoded)
			{
				throw new NotImplementedException();
			}

			public void WriteStrings(string section, string key, string[] value)
			{
				throw new NotImplementedException();
			}

			public void Dispose()
			{
			}
		}
	}
}
