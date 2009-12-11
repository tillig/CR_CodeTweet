using System;
using CR_CodeTweet;

namespace CR_CodeTweetTests
{
	public class TestInfoProvider : IOAuthInfoProvider
	{
		public static Uri TestListenerUri = new Uri("http://localhost:817/CR_CodeTweetTests/");

		public TestInfoProvider()
		{
			this.AccessTokenUrl = new Uri(TestListenerUri, "access_token");
			this.AuthorizeUrl = new Uri(TestListenerUri, "authorize");
			this.ConsumerKey = "consumerkey()";
			this.ConsumerSecret = "consumersecret{}";
			this.RequestTokenUrl = new Uri(TestListenerUri, "request_token");
			this.Token = "token";
			this.TokenSecret = "tokensecret";
			this.Verifier = "pin";
		}

		public Uri AccessTokenUrl { get; set; }

		public Uri AuthorizeUrl { get; set; }

		public string ConsumerKey { get; set; }

		public string ConsumerSecret { get; set; }

		public Uri RequestTokenUrl { get; set; }

		public string Token { get; set; }

		public string TokenSecret { get; set; }

		public string Verifier { get; set; }
	}
}
