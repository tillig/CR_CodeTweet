using System;

namespace CR_CodeTweet
{
	/// <summary>
	/// Provides information for the CR_CodeTweet application to access Twitter.
	/// </summary>
	public class TwitterInfoProvider : IOAuthInfoProvider
	{
		/// <summary>
		/// URL to POST to for getting a token that allows us to post to Twitter.
		/// </summary>
		private static readonly Uri _accessTokenUrl = new Uri("http://twitter.com/oauth/access_token");

		/// <summary>
		/// URL the user should visit to authorize the application with Twitter.
		/// </summary>
		private static readonly Uri _authorizeUrl = new Uri("http://twitter.com/oauth/authorize");

		/// <summary>
		/// The registered CR_CodeTweet consumer key.
		/// </summary>
		private const string _consumerKey = "DxsRq1Q42tJlI1GUOYt0Q";

		/// <summary>
		/// The registered CR_CodeTweet consumer secret.
		/// </summary>
		private const string _consumerSecret = "vmvHdxKK3HHsPDkWwsZ8KLVU3Z2JceGSHs391YAHiwE";

		/// <summary>
		/// URL to POST to for getting a token that allows us to authorize Twitter use for this app.
		/// </summary>
		private static readonly Uri _requestTokenUrl = new Uri("http://twitter.com/oauth/request_token");

		/// <summary>
		/// URL to POST to for getting a token that allows you to post to Twitter.
		/// </summary>
		/// <value>
		/// Always returns <c>http://twitter.com/oauth/access_token</c>.
		/// </value>
		public Uri AccessTokenUrl
		{
			get { return _accessTokenUrl; }
		}

		/// <summary>
		/// URL the user should visit to authorize the application with Twitter.
		/// </summary>
		/// <value>
		/// Always returns <c>http://twitter.com/oauth/authorize</c>.
		/// </value>
		public Uri AuthorizeUrl
		{
			get { return _authorizeUrl; }
		}

		/// <summary>
		/// The registered consumer key for the application accessing Twitter.
		/// </summary>
		/// <value>
		/// A <see cref="System.String"/> provided by Twitter for accessing the system.
		/// </value>
		public string ConsumerKey
		{
			get { return _consumerKey; }
		}

		/// <summary>
		/// The registered consumer secret for the application accessing Twitter.
		/// </summary>
		/// <value>
		/// A <see cref="System.String"/> provided by Twitter for accessing the system.
		/// </value>
		public string ConsumerSecret
		{
			get { return _consumerSecret; }
		}

		/// <summary>
		/// URL to POST to for getting a token that allows you to authorize Twitter use for this app.
		/// </summary>
		/// <value>
		/// Always returns <c>http://twitter.com/oauth/request_token</c>.
		/// </value>
		public Uri RequestTokenUrl
		{
			get { return _requestTokenUrl; }
		}

		/// <summary>
		/// Gets or sets the current request/access token.
		/// </summary>
		/// <value>
		/// A <see cref="System.String"/> with the current request or access token
		/// (based on the stage of communication with the service provider).
		/// </value>
		public string Token { get; set; }

		/// <summary>
		/// Gets or sets the current request/access token secret.
		/// </summary>
		/// <value>
		/// A <see cref="System.String"/> with the current request or access token
		/// secret (based on the stage of communication with the service provider)
		/// for use in request signing.
		/// </value>
		public string TokenSecret { get; set; }

		/// <summary>
		/// Gets or sets the verifier PIN.
		/// </summary>
		/// <value>
		/// A <see cref="System.String"/> provided by the service that indicates
		/// the user has authorized this application to communicate with the service.
		/// </value>
		public string Verifier { get; set; }
	}
}
