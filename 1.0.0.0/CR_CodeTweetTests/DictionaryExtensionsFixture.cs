using System;
using System.Collections.Generic;
using CR_CodeTweet;
using NUnit.Framework;

namespace CR_CodeTweetTests
{
	[TestFixture]
	public class DictionaryExtensionsFixture
	{
		[Test(Description = "Converts an empty dictionary to a querystring.")]
		public void ToQueryString_EmptyDictionary()
		{
			Dictionary<string, string> dict = new Dictionary<string, string>();
			string actual = dict.ToQueryString();
			Assert.AreEqual("", actual, "An empty dictionary should return an empty querystring.");
		}

		[Test(Description = "Converts a dictionary with several items to a querystring and verifies key/value encoding.")]
		public void ToQueryString_MultipleItems()
		{
			Dictionary<string, string> dict = new Dictionary<string, string>();
			dict.Add("key1[]", "value1{}");
			dict.Add("&key2", "&value2");
			dict.Add("k3", "v3");
			string actual = dict.ToQueryString();
			Assert.AreEqual("key1%5B%5D=value1%7B%7D&%26key2=%26value2&k3=v3", actual, "The querystring was not correctly generated.");
		}

		[Test(Description = "Converts a null dictionary to a querystring.")]
		public void ToQueryString_NullDictionary()
		{
			Dictionary<string, string> dict = null;
			string actual = dict.ToQueryString();
			Assert.AreEqual("", actual, "A null dictionary should return an empty querystring.");
		}

		[Test(Description = "Converts a dictionary with one item to a querystring and verifies key/value encoding.")]
		public void ToQueryString_SingleItem()
		{
			Dictionary<string, string> dict = new Dictionary<string, string>();
			dict.Add("key[]", "value{}");
			string actual = dict.ToQueryString();
			Assert.AreEqual("key%5B%5D=value%7B%7D", actual, "The querystring was not correctly generated.");
		}
	}
}
