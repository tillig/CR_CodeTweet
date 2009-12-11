using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Web;

namespace CR_CodeTweet
{
	/// <summary>
	/// Client for interacting with the Twitter service.
	/// </summary>
	/// <remarks>
	/// <para>
	/// Code here is based on
	/// <see href="http://oauth.googlecode.com/svn/code/csharp/">the sample OAuth
	/// client on oauth.net by Eran Sandler</see> and the
	/// <see href="http://www.codingthewheel.com/archives/codingthetweet">starter
	/// Twitter application on Coding the Wheel by James Devlin</see>.
	/// </para>
	/// <para>
	/// The <see href="http://oauth.net/core/1.0a">OAuth spec</see> outlines
	/// how OAuth is supposed to work. This client uses
	/// OAuth for accessing Twitter.
	/// </para>
	/// </remarks>
	public class TwitterClient
	{
		/// <summary>
		/// The POST parameter that Twitter takes with the status message.
		/// </summary>
		public const string StatusUpdateParameter = "status";

		/// <summary>
		/// The URL to which status updates should be posted.
		/// </summary>
		public static readonly Uri StatusUpdateUrl = new Uri("http://twitter.com/statuses/update.xml");

		/// <summary>
		/// The provider of Twitter contact information.
		/// </summary>
		/// <value>
		/// The <see cref="IOAuthInfoProvider"/> used to get info about contacting Twitter.
		/// </value>
		public IOAuthInfoProvider InfoProvider { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="TwitterClient"/> class.
		/// </summary>
		/// <param name="twitterInfo">An <see cref="IOAuthInfoProvider"/> with information about how to contact Twitter.</param>
		/// <exception cref="System.ArgumentNullException">
		/// Thrown if <paramref name="twitterInfo" /> is <see langword="null" />.
		/// </exception>
		public TwitterClient(IOAuthInfoProvider twitterInfo)
		{
			if (twitterInfo == null)
			{
				throw new ArgumentNullException("twitterInfo");
			}
			this.InfoProvider = twitterInfo;
		}

		/// <summary>
		/// Gets an access token from Twitter using the data in the
		/// info provider. Updates the info provider's token and token secret
		/// to be the access information on success.
		/// </summary>
		/// <remarks>
		/// <para>
		/// The <see cref="CR_CodeTweet.TwitterClient.InfoProvider"/> will get its
		/// <see cref="CR_CodeTweet.IOAuthInfoProvider.Token"/> value updated to
		/// be the returned access token and the <see cref="CR_CodeTweet.IOAuthInfoProvider.TokenSecret"/>
		/// to be the access token secret when this gets run.
		/// </para>
		/// </remarks>
		public void GetAccessToken()
		{
			string response = this.ExecuteWebRequest(WebRequestMethod.GET, OAuthRequestType.GetAccessToken, this.InfoProvider.AccessTokenUrl, null);
			if (String.IsNullOrEmpty(response))
			{
				throw new System.Net.WebException("Did not receive a valid response while requesting a token from Twitter.");
			}
			// Get the "oauth_token" and "oauth_token_secret" from the response.
			// Response looks like this:
			// oauth_token=1234567-abcdefghijklmnopqrstuvwxyz1234567890abcdef&oauth_token_secret=abcdefghijklmnopqrstuvwxyz1234567890abcde&user_id=1234567&screen_name=someuser
			// The number at the front of the oauth_token is the same as the user_id.
			// Not sure if that's always true, though. Doesn't matter - we don't
			// need anything but the token and token secret.
			NameValueCollection parsed = HttpUtility.ParseQueryString(response);
			string oauthToken = parsed[OAuthSignatureBuilder.OAuthTokenKey];
			string oauthTokenSecret = parsed[OAuthSignatureBuilder.OAuthTokenSecretKey];
			if (String.IsNullOrEmpty(oauthToken))
			{
				throw new System.Net.WebException("Did not receive an oauth token in the response from Twitter.");
			}
			if (String.IsNullOrEmpty(oauthTokenSecret))
			{
				throw new System.Net.WebException("Did not receive an oauth token secret in the response from Twitter.");
			}

			this.InfoProvider.Token = oauthToken;
			this.InfoProvider.TokenSecret = oauthTokenSecret;
		}

		/// <summary>
		/// Creates the URL the user should visit to authorize CR_CodeTweet. Updates
		/// the info provider's token to be the request token on successful communication.
		/// </summary>
		/// <returns>
		/// A <see cref="System.Uri"/> that a user could open in a browser and
		/// authorize CR_CodeTweet for access.
		/// </returns>
		/// <remarks>
		/// <para>
		/// The <see cref="CR_CodeTweet.TwitterClient.InfoProvider"/> will get its
		/// <see cref="CR_CodeTweet.IOAuthInfoProvider.Token"/> value updated to
		/// be the returned request token when this gets run. Generally the authorization
		/// process should be done with a new/clean info provider rather than one
		/// built from stored settings. (Auth really only happens once - long enough
		/// to get a permanent access token.)
		/// </para>
		/// </remarks>
		/// <exception cref="System.Net.WebException">
		/// Thrown if the response from Twitter is empty or does not contain the
		/// request token as dictated by the OAuth spec.
		/// </exception>
		public Uri GetAuthorizationUrl()
		{
			// Get a request token. GET to RequestTokenUrl.
			string response = this.ExecuteWebRequest(WebRequestMethod.GET, OAuthRequestType.GetRequestToken, this.InfoProvider.RequestTokenUrl, null);
			if (String.IsNullOrEmpty(response))
			{
				throw new System.Net.WebException("Did not receive a valid response while requesting a token from Twitter.");
			}

			// Get the "oauth_token" from the response.
			NameValueCollection parsed = HttpUtility.ParseQueryString(response);
			string oauthToken = parsed[OAuthSignatureBuilder.OAuthTokenKey];
			if (String.IsNullOrEmpty(oauthToken))
			{
				throw new System.Net.WebException("Did not receive an oauth token in the response from Twitter.");
			}
			this.InfoProvider.Token = oauthToken;

			// Build the URL with the returned token.
			UriBuilder authorizationUrl = new UriBuilder(this.InfoProvider.AuthorizeUrl);
			authorizationUrl.Query = OAuthSignatureBuilder.OAuthTokenKey + "=" + this.InfoProvider.Token.UrlEncodeForOAuth();
			return authorizationUrl.Uri;
		}


		/// <summary>
		/// Executes a web request and returns the response content.
		/// </summary>
		/// <param name="webRequestMethod">The HTTP request method.</param>
		/// <param name="requestType">The type of request being made.</param>
		/// <param name="requestUrl">URL to which the request should be made.</param>
		/// <param name="requestParameters">Data to provide in the request (querystring or post data). Pass <see langword="null" /> if there is no data.</param>
		/// <returns>A <see cref="System.String"/> containing the response content.</returns>
		/// <exception cref="System.ArgumentNullException">
		/// Thrown if <paramref name="webRequestMethod" /> or <paramref name="requestUrl" /> is <see langword="null" />.
		/// </exception>
		/// <exception cref="System.ArgumentException">
		/// Thrown if <paramref name="webRequestMethod" /> is <see cref="System.String.Empty" />.
		/// </exception>
		public string ExecuteWebRequest(WebRequestMethod webRequestMethod, OAuthRequestType requestType, Uri requestUrl, Dictionary<string, string> requestParameters)
		{
			// We are using the HttpWebRequest/HttpWebResponse here rather than WebClient
			// for two reasons. First, we need to make sure the URL encoding goes
			// through our upper-case encoding rather than the built-in one, which
			// is required per http://oauth.net/core/1.0a#encoding_parameters.
			// Second, we need to control the "Expect100Continue" property on the
			// outgoing request.

			if (requestUrl == null)
			{
				throw new ArgumentNullException("requestUrl");
			}

			// Get the request parameters normalized and signed.
			OAuthSignatureBuilder sigBuilder = new OAuthSignatureBuilder(this.InfoProvider);
			OAuthSignature signature = sigBuilder.BuildSignature(requestUrl, requestParameters, webRequestMethod, requestType);
			string query = signature.NormalizedParameters;

			HttpWebRequest webRequest = null;
			WebResponse response = null;

			try
			{
				if (webRequestMethod == WebRequestMethod.GET)
				{
					UriBuilder getUrl = new UriBuilder(requestUrl);
					getUrl.Query = query;
					requestUrl = getUrl.Uri;
				}

				webRequest = (HttpWebRequest)WebRequest.Create(requestUrl);
				webRequest.Method = webRequestMethod.ToString();
				webRequest.ServicePoint.Expect100Continue = false;

				if (webRequestMethod == WebRequestMethod.POST)
				{
					webRequest.ContentType = "application/x-www-form-urlencoded";
					using (StreamWriter requestWriter = new StreamWriter(webRequest.GetRequestStream()))
					{
						requestWriter.Write(query);
					}
				}

				response = webRequest.GetResponse();
				using (StreamReader responseReader = new StreamReader(response.GetResponseStream()))
				{
					return responseReader.ReadToEnd();
				}
			}
			finally
			{
				if (response != null)
				{
					response.Close();
					response = null;
				}
				webRequest = null;
			}
		}

		/// <summary>
		/// Sends content to Twitter.
		/// </summary>
		/// <param name="tweetContent">The body of the tweet to send.</param>
		/// <exception cref="System.ArgumentNullException">
		/// Thrown if <paramref name="tweetContent" /> is <see langword="null" />.
		/// </exception>
		/// <exception cref="System.ArgumentException">
		/// Thrown if <paramref name="tweetContent" /> is less than one character long.
		/// </exception>
		public void Tweet(string tweetContent)
		{
			if (tweetContent == null)
			{
				throw new ArgumentNullException("tweetContent");
			}
			if (tweetContent.Length < 1)
			{
				throw new ArgumentException("Tweet must be at least one character long.", "tweetContent");
			}
			Dictionary<string, string> requestParameters = new Dictionary<string, string>();
			requestParameters.Add(StatusUpdateParameter, tweetContent);
			this.ExecuteWebRequest(WebRequestMethod.POST, OAuthRequestType.GetProtectedResources, StatusUpdateUrl, requestParameters);
		}
	}
}
