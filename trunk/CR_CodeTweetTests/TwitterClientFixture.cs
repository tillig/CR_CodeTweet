using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using CR_CodeTweet;
using NUnit.Framework;

namespace CR_CodeTweetTests
{
	[TestFixture]
	public class TwitterClientFixture
	{
		private WebServer _server = null;

		[SetUp]
		public void SetUp()
		{
			this._server = new WebServer(TestInfoProvider.TestListenerUri);
			this._server.Start();
		}

		[TearDown]
		public void TearDown()
		{
			this._server.Stop();
			this._server.Dispose();
		}

		[Test(Description = "Attempts to construct a TwitterClient with a null info provider.")]
		public void Ctor_NullProvider()
		{
			Assert.Throws<ArgumentNullException>(() => new TwitterClient(null));
		}

		[Test(Description = "Ensures that a GET works with no query string contents.")]
		public void ExecuteWebRequest_GetNoQueryString()
		{
			this._server.IncomingRequest += this.CreateRequestHandler(200, (args) => args.RequestContext.Request.Url.Query);
			TwitterClient client = new TwitterClient(new TestInfoProvider());
			string actual = client.ExecuteWebRequest(WebRequestMethod.GET, OAuthRequestType.GetRequestToken, TestInfoProvider.TestListenerUri, null);
			string[] pairs = actual.Substring(1).Split('&');
			foreach (string pair in pairs)
			{
				Assert.IsTrue(pair.StartsWith("oauth_"), "There should only be oauth-related parameters present.");
			}
		}

		[Test(Description = "Ensures that the query string contents come through in a GET request.")]
		public void ExecuteWebRequest_GetQueryString()
		{
			Dictionary<string, string> data = new Dictionary<string, string>()
			{
				{"a", "1"},
				{"b", "{}[]"}
			};
			this._server.IncomingRequest += this.CreateRequestHandler(200, (args) => args.RequestContext.Request.Url.Query.Substring(1));
			TwitterClient client = new TwitterClient(new TestInfoProvider());
			string actual = client.ExecuteWebRequest(WebRequestMethod.GET, OAuthRequestType.GetRequestToken, TestInfoProvider.TestListenerUri, data);
			string expected = data.ToQueryString();
			Assert.AreEqual(expected + "&", actual.Substring(0, expected.Length + 1), "The query string did not correctly come through.");
		}

		[Test(Description = "Attempts to make a request to a null URL.")]
		public void ExecuteWebRequest_NullUrl()
		{
			TwitterClient client = new TwitterClient(new TestInfoProvider());
			Assert.Throws<ArgumentNullException>(() => client.ExecuteWebRequest(WebRequestMethod.GET, OAuthRequestType.GetRequestToken, null, null));
		}

		[Test(Description = "Ensures that the data contents come through in a POST request.")]
		public void ExecuteWebRequest_PostData()
		{
			Dictionary<string, string> data = new Dictionary<string, string>()
			{
				{"a", "1"},
				{"b", "{}[]"}
			};
			this._server.IncomingRequest += this.CreateRequestHandler(200,
				(args) =>
				{
					using (StreamReader reader = new StreamReader(args.RequestContext.Request.InputStream))
					{
						return reader.ReadToEnd();
					}
				});
			TwitterClient client = new TwitterClient(new TestInfoProvider());
			string actual = client.ExecuteWebRequest(WebRequestMethod.POST, OAuthRequestType.GetRequestToken, TestInfoProvider.TestListenerUri, data);
			string expected = data.ToQueryString();
			Assert.AreEqual(expected + "&", actual.Substring(0, expected.Length + 1), "The posted data did not correctly come through.");
		}

		[Test(Description = "Ensures that a POST works with no data content.")]
		public void ExecuteWebRequest_PostNoData()
		{
			this._server.IncomingRequest += this.CreateRequestHandler(200,
				(args) =>
				{
					using (StreamReader reader = new StreamReader(args.RequestContext.Request.InputStream))
					{
						return reader.ReadToEnd();
					}
				});
			TwitterClient client = new TwitterClient(new TestInfoProvider());
			string actual = client.ExecuteWebRequest(WebRequestMethod.POST, OAuthRequestType.GetRequestToken, TestInfoProvider.TestListenerUri, null);
			string[] pairs = actual.Split('&');
			foreach (string pair in pairs)
			{
				Assert.IsTrue(pair.StartsWith("oauth_"), "There should only be oauth-related parameters present.");
			}
		}

		[Test(Description = "Verifies that the token and token secret are not updated on the info provider for a failed call.")]
		public void GetAccessToken_TokenNotUpdatedOnInfoProviderOnFail()
		{
			this._server.IncomingRequest += this.CreateRequestHandler(500, (args) => "Server Error");
			TestInfoProvider provider = new TestInfoProvider();
			provider.Token = "token";
			provider.TokenSecret = "tokensecret";
			TwitterClient client = new TwitterClient(provider);
			Assert.Throws<WebException>(() => client.GetAccessToken());
			Assert.AreEqual("token", provider.Token, "The access token should not be updated in the provider.");
			Assert.AreEqual("tokensecret", provider.TokenSecret, "The access token secret should not be updated in the provider.");
		}

		[Test(Description = "Verifies that the token and token secret are not updated on the info provider for an invalid response from Twitter.")]
		public void GetAccessToken_TokenNotUpdatedOnInfoProviderOnEmptyResponse()
		{
			this._server.IncomingRequest += this.CreateRequestHandler(200, (args) => "");
			TestInfoProvider provider = new TestInfoProvider();
			provider.Token = "token";
			provider.TokenSecret = "tokensecret";
			TwitterClient client = new TwitterClient(provider);
			Assert.Throws<WebException>(() => client.GetAccessToken());
			Assert.AreEqual("token", provider.Token, "The access token should not be updated in the provider.");
			Assert.AreEqual("tokensecret", provider.TokenSecret, "The access token secret should not be updated in the provider.");
		}

		[Test(Description = "Verifies that the token and token secret are not updated on the info provider if the token was not returned.")]
		public void GetAccessToken_TokenNotUpdatedOnInfoProviderOnMissingToken()
		{
			this._server.IncomingRequest += this.CreateRequestHandler(200, (args) => "oauth_token_secret=access_token_secret");
			TestInfoProvider provider = new TestInfoProvider();
			provider.Token = "token";
			provider.TokenSecret = "tokensecret";
			TwitterClient client = new TwitterClient(provider);
			Assert.Throws<WebException>(() => client.GetAccessToken());
			Assert.AreEqual("token", provider.Token, "The access token should not be updated in the provider.");
			Assert.AreEqual("tokensecret", provider.TokenSecret, "The access token secret should not be updated in the provider.");
		}

		[Test(Description = "Verifies that the token and token secret are not updated on the info provider if the token secret was not returned.")]
		public void GetAccessToken_TokenNotUpdatedOnInfoProviderOnMissingTokenSecret()
		{
			this._server.IncomingRequest += this.CreateRequestHandler(200, (args) => "oauth_token=access_token");
			TestInfoProvider provider = new TestInfoProvider();
			provider.Token = "token";
			provider.TokenSecret = "tokensecret";
			TwitterClient client = new TwitterClient(provider);
			Assert.Throws<WebException>(() => client.GetAccessToken());
			Assert.AreEqual("token", provider.Token, "The access token should not be updated in the provider.");
			Assert.AreEqual("tokensecret", provider.TokenSecret, "The access token secret should not be updated in the provider.");
		}

		[Test(Description = "Verifies that the token and token secret are updated on the info provider for a successful call.")]
		public void GetAccessToken_TokenUpdatedOnInfoProviderOnSuccess()
		{
			this._server.IncomingRequest += this.CreateRequestHandler(200, (args) => "oauth_token=access_token&oauth_token_secret=access_token_secret");
			TestInfoProvider provider = new TestInfoProvider();
			provider.Token = "token";
			provider.TokenSecret = "tokensecret";
			TwitterClient client = new TwitterClient(provider);
			client.GetAccessToken();
			Assert.AreEqual("access_token", provider.Token, "The access token was not updated in the provider.");
			Assert.AreEqual("access_token_secret", provider.TokenSecret, "The access token secret was not updated in the provider.");
		}

		[Test(Description = "OAuth 6.3.1: A request token is required to get an access token.")]
		public void GetAccessToken_RequestTokenRequired()
		{
			TestInfoProvider provider = new TestInfoProvider();
			provider.Token = null;
			TwitterClient client = new TwitterClient(provider);
			Assert.Throws<ArgumentException>(() => client.GetAccessToken());
		}

		[Test(Description = "OAuth 6.3.1: A verifier (PIN) is required to get an access token.")]
		public void GetAccessToken_VerifierRequired()
		{
			TestInfoProvider provider = new TestInfoProvider();
			provider.Verifier = null;
			TwitterClient client = new TwitterClient(provider);
			Assert.Throws<ArgumentException>(() => client.GetAccessToken());
		}

		[Test(Description = "OAuth 1.0a 6.2.1: To get to the authorization URL, the GET request has a request token obtained from the provider.")]
		public void GetAuthorizationUrl_RequestTokenAddedToAuthorizationUrl()
		{
			this._server.IncomingRequest += this.CreateRequestHandler(200, (args) => "oauth_token=request_token&oauth_token_secret=request_token_secret&oauth_callback_confirmed=true");
			TestInfoProvider provider = new TestInfoProvider();
			TwitterClient client = new TwitterClient(provider);
			Uri authUrl = client.GetAuthorizationUrl();
			UriBuilder expected = new UriBuilder(provider.AuthorizeUrl);
			expected.Query = "oauth_token=request_token";
			Assert.AreEqual(expected.Uri, authUrl, "The authorization URL was not properly constructed.");
		}

		[Test(Description = "After getting the request token for the authorization URL, further auth (getting an access token) will need it.")]
		public void GetAuthorizationUrl_RequestTokenUpdatedOnInfoProvider()
		{
			this._server.IncomingRequest += this.CreateRequestHandler(200, (args) => "oauth_token=request_token&oauth_token_secret=request_token_secret&oauth_callback_confirmed=true");
			TestInfoProvider provider = new TestInfoProvider();
			provider.Token = null;
			TwitterClient client = new TwitterClient(provider);
			Uri authUrl = client.GetAuthorizationUrl();
			Assert.AreEqual("request_token", provider.Token, "The token should be populated with the request token.");
		}

		private EventHandler<HttpRequestEventArgs> CreateRequestHandler(int statusCode, Func<HttpRequestEventArgs, string> responseContent)
		{
			return (object sender, HttpRequestEventArgs args) =>
				{
					args.RequestContext.Response.StatusCode = statusCode;
					using (StreamWriter writer = new StreamWriter(args.RequestContext.Response.OutputStream))
					{
						writer.Write(responseContent(args));
						writer.Flush();
					}
				};
		}
	}
}
