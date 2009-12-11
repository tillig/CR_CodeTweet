using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace CR_CodeTweet
{
	/// <summary>
	/// Builds OAuth signatures for use in communicating with OAuth-enabled services.
	/// </summary>
	/// <remarks>
	/// <para>
	/// The primary interface to this class is the <see cref="OAuthSignatureBuilder.BuildSignature"/>
	/// method. Other methods available help in the generation of the signature.
	/// </para>
	/// </remarks>
	/// <seealso href="http://oauth.net/core/1.0a#signing_process"/>
	public class OAuthSignatureBuilder
	{
		/// <summary>
		/// Query string parameter for the OAuth callback key.
		/// </summary>
		public const string OAuthCallbackKey = "oauth_callback";

		/// <summary>
		/// Query string parameter for the OAuth consumer key (<see cref="IOAuthInfoProvider.ConsumerKey"/>).
		/// </summary>
		public const string OAuthConsumerKeyKey = "oauth_consumer_key";

		/// <summary>
		/// Query string parameter for the OAuth nonce.
		/// </summary>
		public const string OAuthNonceKey = "oauth_nonce";

		/// <summary>
		/// Query string parameter for the signature on the OAuth message.
		/// </summary>
		public const string OAuthSignatureKey = "oauth_signature";

		/// <summary>
		/// Query string parameter for the way we sign OAuth messages.
		/// </summary>
		public const string OAuthSignatureMethodKey = "oauth_signature_method";

		/// <summary>
		/// Query string parameter for the OAuth timestamp.
		/// </summary>
		public const string OAuthTimestampKey = "oauth_timestamp";

		/// <summary>
		/// Query string parameter for OAuth token.
		/// </summary>
		public const string OAuthTokenKey = "oauth_token";

		/// <summary>
		/// Query string parameter for OAuth token secret.
		/// </summary>
		public const string OAuthTokenSecretKey = "oauth_token_secret";

		/// <summary>
		/// Query string parameter for the OAuth verifier, used in PIN-based auth support.
		/// </summary>
		public const string OAuthVerifierKey = "oauth_verifier";

		/// <summary>
		/// Indicates the OAuth version this class supports.
		/// </summary>
		public const string OAuthVersion = "1.0";

		/// <summary>
		/// Query string parameter for the OAuth version being used.
		/// </summary>
		public const string OAuthVersionKey = "oauth_version";

		/// <summary>
		/// The provider of OAuth contact information.
		/// </summary>
		/// <value>
		/// The <see cref="IOAuthInfoProvider"/> used to get info about secure OAuth communications.
		/// </value>
		public IOAuthInfoProvider InfoProvider { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="OAuthSignatureBuilder"/> class.
		/// </summary>
		/// <param name="infoProvider">An <see cref="IOAuthInfoProvider"/> with information about how to contact the OAuth service.</param>
		/// <exception cref="System.ArgumentNullException">
		/// Thrown if <paramref name="infoProvider" /> is <see langword="null" />.
		/// </exception>
		public OAuthSignatureBuilder(IOAuthInfoProvider infoProvider)
		{
			if (infoProvider == null)
			{
				throw new ArgumentNullException("infoProvider");
			}
			this.InfoProvider = infoProvider;
		}

		/// <summary>
		/// Builds the HMAC-SHA1 signature key.
		/// </summary>
		/// <param name="tokenSecret">
		/// The token secret to include in the key.
		/// </param>
		/// <returns>
		/// The byte array used as the signature key in creating the OAuth signature.
		/// </returns>
		/// <remarks>
		/// The HMAC-SHA1 signature method uses the HMAC-SHA1 signature algorithm
		/// as defined in [RFC2104] where the Signature Base String is the text
		/// and the key is the concatenated values (each first encoded per Parameter Encoding)
		/// of the Consumer Secret and Token Secret, separated by an '&amp;' character
		/// (ASCII code 38) even if empty.
		/// </remarks>
		public byte[] BuildHmacSha1Key(string tokenSecret)
		{
			return Encoding.ASCII.GetBytes(
				(String.IsNullOrEmpty(this.InfoProvider.ConsumerSecret) ? "" : this.InfoProvider.ConsumerSecret.UrlEncodeForOAuth()) +
				"&" +
				(String.IsNullOrEmpty(tokenSecret) ? "" : tokenSecret.UrlEncodeForOAuth())
				);
		}

		/// <summary>
		/// Builds an encrypted OAuth signature.
		/// </summary>
		/// <param name="requestUrl">The <see cref="System.Uri"/> to which the request is being made. Querystring is ignored - pass request parameters in <paramref name="requestParameters"/>.</param>
		/// <param name="requestParameters">The parameters being passed to the service request. Ignored in <see cref="OAuthRequestType.GetAccessToken"/> requests.</param>
		/// <param name="requestMethod">The web request method being used for the request.</param>
		/// <param name="requestType">The type of OAuth request being made.</param>
		/// <returns>
		/// An <see cref="OAuthSignature"/> that contains the encrypted signature
		/// to be used in the "oauth_signature" request parameter as well as the
		/// normalized request parameters - including the signature - to use in
		/// the request.
		/// </returns>
		/// <remarks>
		/// <para>
		/// The normalized request parameters that come back in the <see cref="OAuthSignature"/>
		/// will already contain the signature. Pass them directly to the request
		/// unaltered.
		/// </para>
		/// <para>
		/// The signature type used here is HMAC-SHA1.
		/// </para>
		/// <para>
		/// The verifier, token, and token secret information used in the signature
		/// will be retrieved from <see cref="OAuthSignatureBuilder.InfoProvider"/>.
		/// </para>
		/// </remarks>
		public OAuthSignature BuildSignature(Uri requestUrl, Dictionary<string, string> requestParameters, WebRequestMethod requestMethod, OAuthRequestType requestType)
		{
			OAuthSignature signatureBase = BuildSignatureBase(requestUrl, requestParameters, requestMethod, requestType);

			// Hash the unencoded signature information
			HMACSHA1 hmacsha1 = new HMACSHA1();
			hmacsha1.Key = this.BuildHmacSha1Key(this.InfoProvider.TokenSecret);
			byte[] dataBuffer = Encoding.ASCII.GetBytes(signatureBase.Signature);
			byte[] hashBytes = hmacsha1.ComputeHash(dataBuffer);

			// Update the signature data with the actual hash and send back the fully-built signature.
			signatureBase.Signature = Convert.ToBase64String(hashBytes);
			signatureBase.NormalizedParameters += "&" + OAuthSignatureKey + "=" + signatureBase.Signature.UrlEncodeForOAuth();
			return signatureBase;
		}

		/// <summary>
		/// Builds the unencrypted base for an OAuth signature.
		/// </summary>
		/// <param name="requestUrl">The <see cref="System.Uri"/> to which the request is being made. Querystring is ignored - pass request parameters in <paramref name="requestParameters"/>.</param>
		/// <param name="requestParameters">The parameters being passed to the service request. Ignored in <see cref="OAuthRequestType.GetAccessToken"/> requests.</param>
		/// <param name="requestMethod">The web request method being used for the request.</param>
		/// <param name="requestType">The type of OAuth request being made.</param>
		/// <returns>
		/// An <see cref="OAuthSignature"/> that contains the unencrypted basis
		/// for the OAuth signature and a normalized set of request parameters that
		/// are formatted and ready to pass to the request.
		/// </returns>
		/// <exception cref="System.ArgumentException">
		/// <para>
		/// Thrown if...
		/// </para>
		/// <list type="bullet">
		/// <item>
		/// <term>
		/// The <see cref="IOAuthInfoProvider.Token"/> on <see cref="OAuthSignatureBuilder.InfoProvider"/>
		/// is <see langword="null" /> or <see cref="System.String.Empty"/>
		/// and this request is a <see cref="OAuthRequestType.GetAccessToken"/> or
		/// <see cref="OAuthRequestType.GetProtectedResources"/> request.
		/// </term>
		/// </item>
		/// <item>
		/// <term>
		/// The <see cref="IOAuthInfoProvider.Verifier"/> on <see cref="OAuthSignatureBuilder.InfoProvider"/>
		/// is <see langword="null" /> or <see cref="System.String.Empty"/>
		/// and this request is a <see cref="OAuthRequestType.GetAccessToken"/> request.
		/// </term>
		/// </item>
		/// </list>
		/// </exception>
		public OAuthSignature BuildSignatureBase(Uri requestUrl, Dictionary<string, string> requestParameters, WebRequestMethod requestMethod, OAuthRequestType requestType)
		{
			// All requests to an OAuth service need...
			// * oauth_consumer_key
			// * oauth_signature_method
			// * oauth_signature (except right now we're making that...)
			// * oauth_timestamp
			// * oauth_nonce
			// * oauth_version

			// In addition:

			// To obtain a request token...
			// * oauth_callback (only when getting a request token)
			// and any additional parameters needed by the provider.

			// To obtain an access token...
			// * oauth_token (the request token)
			// * oauth_verifier (the PIN from the provider)
			// and nothing else is allowed.

			// To access protected resources...
			// * oauth_token (the access token)
			// and any additional parameters needed by the provider.


			// Common to all requests:
			// * oauth_consumer_key
			// * oauth_signature_method
			// * oauth_timestamp
			// * oauth_nonce
			// * oauth_version
			SortedDictionary<string, string> parameters = new SortedDictionary<string, string>();
			parameters[OAuthConsumerKeyKey] = this.InfoProvider.ConsumerKey;
			parameters[OAuthSignatureMethodKey] = "HMAC-SHA1";
			parameters[OAuthTimestampKey] = this.GenerateTimestamp().ToString(CultureInfo.InvariantCulture);
			parameters[OAuthNonceKey] = this.GenerateNonce();
			parameters[OAuthVersionKey] = OAuthVersion;

			// * oauth_callback (only when getting a request token)
			// Value will be "oob" for out-of-band callback (means "issue a PIN").
			if (requestType == OAuthRequestType.GetRequestToken)
			{
				parameters[OAuthCallbackKey] = "oob";
			}

			// * oauth_token
			// The request token when asking for an access token or the access
			// token when asking for protected resources.
			if (requestType == OAuthRequestType.GetAccessToken || requestType == OAuthRequestType.GetProtectedResources)
			{
				if (String.IsNullOrEmpty(this.InfoProvider.Token))
				{
					throw new ArgumentException("You must provide a non-empty, non-null request token for getting an access token, or access token for accessing protected resources. Be sure to set the InfoProvider Token property.");
				}
				parameters[OAuthTokenKey] = this.InfoProvider.Token;
			}

			// * oauth_verifier (the PIN from the provider)
			if (requestType == OAuthRequestType.GetAccessToken)
			{
				if (String.IsNullOrEmpty(this.InfoProvider.Verifier))
				{
					throw new ArgumentException("You must provide a non-empty, non-null PIN (verifier) for getting an access token. Be sure to set the InfoProvider Verifier property.");
				}
				parameters[OAuthVerifierKey] = this.InfoProvider.Verifier;
			}

			// Add provider-specific request parameters for request token requests
			// and protected resource requests. Don't add existing OAuth parameters.
			if (
				(requestParameters != null && requestParameters.Count > 0) &&
				(requestType == OAuthRequestType.GetRequestToken || requestType == OAuthRequestType.GetProtectedResources)
				)
			{
				foreach (var key in requestParameters.Keys)
				{
					if (!key.StartsWith("oauth_"))
					{
						parameters[key] = requestParameters[key];
					}
				}
			}

			// The SortedDictionary will automatically sort everything by key for us.
			string normalizedParameters = parameters.ToQueryString();

			// Get the request URL with no querystring.
			UriBuilder uriWithoutQuery = new UriBuilder(requestUrl);
			uriWithoutQuery.Query = "";

			// Signature base string must be, in this order:
			// * HTTP method in upper case.
			// * The request URL, encoded, no querystring.
			// * The rest of the normalized request parameters.
			OAuthSignature signature = new OAuthSignature()
			{
				Signature = String.Format(CultureInfo.InvariantCulture, "{0}&{1}&{2}", requestMethod, uriWithoutQuery.Uri.AbsoluteUri.UrlEncodeForOAuth(), normalizedParameters.UrlEncodeForOAuth()),
				NormalizedParameters = normalizedParameters
			};
			return signature;
		}

		/// <summary>
		/// Generates a cryptographically random one-time value.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String"/> that can be used as a nonce in OAuth requests.
		/// </returns>
		public string GenerateNonce()
		{
			byte[] data = new byte[8];
			RNGCryptoServiceProvider.Create().GetBytes(data);
			Int64 nonce = BitConverter.ToInt64(data, 0);
			if (nonce < 0)
			{
				nonce *= -1;
			}
			return nonce.ToString(CultureInfo.InvariantCulture);
		}

		/// <summary>
		/// Generates a timestamp with the number of seconds since Jan 1, 1970 00:00:00 GMT.
		/// </summary>
		/// <returns>
		/// An <see cref="Int64"/> with the number of seconds since the Unix epoch.
		/// </returns>
		public Int64 GenerateTimestamp()
		{
			TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1);
			return (Int64)ts.TotalSeconds;
		}
	}
}
