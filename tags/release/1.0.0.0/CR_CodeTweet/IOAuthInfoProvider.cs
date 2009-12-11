using System;

namespace CR_CodeTweet
{
	/// <summary>
	/// Provides information for OAuth-related requests.
	/// </summary>
	public interface IOAuthInfoProvider
	{
		/// <summary>
		/// Gets the URL to POST to for getting a token that allows you to access protected resources.
		/// </summary>
		/// <value>
		/// A <see cref="System.Uri"/> with a URL like <c>http://twitter.com/oauth/access_token</c>.
		/// </value>
		Uri AccessTokenUrl { get; }

		/// <summary>
		/// Gets the URL the user should visit to authorize the application with the OAuth service provider.
		/// </summary>
		/// <value>
		/// A <see cref="System.Uri"/> with a URL like <c>http://twitter.com/oauth/authorize</c>.
		/// </value>
		Uri AuthorizeUrl { get; }

		/// <summary>
		/// Gets the registered consumer key for the application accessing the OAuth service provider.
		/// </summary>
		/// <value>
		/// A <see cref="System.String"/> provided by the service for accessing the system.
		/// </value>
		string ConsumerKey { get; }

		/// <summary>
		/// Gets the registered consumer secret for the application accessing the OAuth service provider.
		/// </summary>
		/// <value>
		/// A <see cref="System.String"/> provided by the service for accessing the system.
		/// </value>
		string ConsumerSecret { get; }

		/// <summary>
		/// Gets the URL to POST to for getting a token that allows you to start making requests with this app.
		/// </summary>
		/// <value>
		/// A <see cref="System.Uri"/> with a URL like <c>http://twitter.com/oauth/request_token</c>.
		/// </value>
		Uri RequestTokenUrl { get; }

		/// <summary>
		/// Gets or sets the current request/access token.
		/// </summary>
		/// <value>
		/// A <see cref="System.String"/> with the current request or access token
		/// (based on the stage of communication with the service provider).
		/// </value>
		string Token { get; set; }

		/// <summary>
		/// Gets or sets the current request/access token secret.
		/// </summary>
		/// <value>
		/// A <see cref="System.String"/> with the current request or access token
		/// secret (based on the stage of communication with the service provider)
		/// for use in request signing.
		/// </value>
		string TokenSecret { get; set; }

		/// <summary>
		/// Gets or sets the verifier PIN.
		/// </summary>
		/// <value>
		/// A <see cref="System.String"/> provided by the service that indicates
		/// the user has authorized this application to communicate with the service.
		/// </value>
		string Verifier { get; set; }
	}
}
