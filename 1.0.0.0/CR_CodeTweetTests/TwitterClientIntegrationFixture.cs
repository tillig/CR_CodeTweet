using System;
using CR_CodeTweet;
using NUnit.Framework;

namespace CR_CodeTweetTests
{
	[TestFixture]
	[Explicit("These tests actually make calls to the Twitter service.")]
	public class TwitterClientIntegrationFixture
	{
		[Test(Description = "Requests an authorization URL from Twitter.")]
		public void GetAuthorizationUrl()
		{
			// A correct authorization URL looks like:
			// http://twitter.com/oauth/authorize?oauth_token=c7F9nNDQnxr8hmgdfdLXxPc2hkkpWnd6UluWJls0vM

			TwitterClient client = new TwitterClient(new TwitterInfoProvider());
			Uri authUrl = client.GetAuthorizationUrl();
			Assert.IsTrue(authUrl.AbsoluteUri.StartsWith("http://twitter.com/oauth/authorize?oauth_token="), "The authorization URL was incorrect.");
		}
	}
}
