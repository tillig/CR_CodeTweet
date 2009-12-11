using System;
using CR_CodeTweet;
using NUnit.Framework;

namespace CR_CodeTweetTests
{
	[TestFixture]
	public class PostedSnippetInfoFixture
	{
		[Test(Description = "Tests the default property values for a new info object.")]
		public void Ctor_DefaultValues()
		{
			PostedSnippetInfo info = new PostedSnippetInfo();
			Assert.IsNull(info.Id, "The ID should start null.");
			Assert.IsNull(info.Url, "The Url should start null.");
			Assert.IsNull(info.SessionKey, "The session key should start null.");
		}

		[Test(Description = "Setting the ID property to empty string is the same as setting it to null.")]
		public void Id_SetConvertsEmptyStringToNull()
		{
			PostedSnippetInfo info = new PostedSnippetInfo();
			info.Id = "abcde";
			info.Id = "";
			Assert.IsNull(info.Id, "The ID should be null if it is set to empty.");
			Assert.IsNull(info.Url, "The URL should be null if the ID is set to empty.");
		}

		[Test(Description = "Setting the ID property to null empties the Url property.")]
		public void Id_SetUpdatesUrlWithNull()
		{
			PostedSnippetInfo info = new PostedSnippetInfo();
			info.Id = "abcde";
			info.Id = null;
			Assert.IsNull(info.Url, "The URL should be null if the ID is set to null.");
		}

		[Test(Description = "Setting the ID property updates the Url property with the correct value.")]
		public void Id_SetUpdatesUrlWithValue()
		{
			PostedSnippetInfo info = new PostedSnippetInfo();
			info.Id = "abcde";
			Assert.AreEqual("http://codepaste.net/abcde", info.Url.AbsoluteUri, "The URL was not updated correctly when the ID was set.");
		}
	}
}
