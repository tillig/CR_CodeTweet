using System;
using System.Collections.Generic;
using System.Text;
using CR_CodeTweet;
using NUnit.Framework;

namespace CR_CodeTweetTests
{
	[TestFixture]
	public class OAuthSignatureBuilderFixture
	{
		[Test(Description = "OAuth 1.0a 9.2: Key is consumer secret and token secret, encoded and concatenated by &.")]
		public void BuildHmacSha1Key_CompleteKey()
		{
			OAuthSignatureBuilder client = new OAuthSignatureBuilder(new TestInfoProvider());
			byte[] key = client.BuildHmacSha1Key("tokensecret[]");
			string decoded = Encoding.ASCII.GetString(key);
			Assert.AreEqual("consumersecret%7B%7D&tokensecret%5B%5D", decoded, "The HMAC-SHA1 key with both consumer and token secret was not correctly generated.");
		}

		[Test(Description = "OAuth 1.0a 9.2: Consumer secret may be empty.")]
		public void BuildHmacSha1Key_ConsumerSecretEmpty()
		{
			TestInfoProvider provider = new TestInfoProvider();
			provider.ConsumerSecret = "";
			OAuthSignatureBuilder client = new OAuthSignatureBuilder(provider);
			byte[] key = client.BuildHmacSha1Key("tokensecret[]");
			string decoded = Encoding.ASCII.GetString(key);
			Assert.AreEqual("&tokensecret%5B%5D", decoded, "The HMAC-SHA1 key with an empty consumer secret and valid token secret was not correctly generated.");
		}

		[Test(Description = "OAuth 1.0a 9.2: Consumer secret may be null.")]
		public void BuildHmacSha1Key_ConsumerSecretNull()
		{
			TestInfoProvider provider = new TestInfoProvider();
			provider.ConsumerSecret = null;
			OAuthSignatureBuilder client = new OAuthSignatureBuilder(provider);
			byte[] key = client.BuildHmacSha1Key("tokensecret[]");
			string decoded = Encoding.ASCII.GetString(key);
			Assert.AreEqual("&tokensecret%5B%5D", decoded, "The HMAC-SHA1 key with a null consumer secret and valid token secret was not correctly generated.");
		}

		[Test(Description = "OAuth 1.0a 9.2: Token secret may be empty.")]
		public void BuildHmacSha1Key_TokenSecretEmpty()
		{
			OAuthSignatureBuilder client = new OAuthSignatureBuilder(new TestInfoProvider());
			byte[] key = client.BuildHmacSha1Key("");
			string decoded = Encoding.ASCII.GetString(key);
			Assert.AreEqual("consumersecret%7B%7D&", decoded, "The HMAC-SHA1 key with a valid consumer secret and empty token secret was not correctly generated.");
		}

		[Test(Description = "OAuth 1.0a 9.2: Token secret may be null.")]
		public void BuildHmacSha1Key_TokenSecretNull()
		{
			OAuthSignatureBuilder client = new OAuthSignatureBuilder(new TestInfoProvider());
			byte[] key = client.BuildHmacSha1Key(null);
			string decoded = Encoding.ASCII.GetString(key);
			Assert.AreEqual("consumersecret%7B%7D&", decoded, "The HMAC-SHA1 key with a valid consumer secret and null token secret was not correctly generated.");
		}

		[Test(Description = "Verifies that the normalized parameters get the signature appended.")]
		public void BuildSignature_SignatureAppendedToNormalizedParameters()
		{
			TestInfoProvider provider = new TestInfoProvider();
			OAuthSignatureBuilder client = new OAuthSignatureBuilder(provider);
			OAuthSignature signature = client.BuildSignature(provider.AccessTokenUrl, null, WebRequestMethod.GET, OAuthRequestType.GetProtectedResources);
			string signatureEnd = "&oauth_signature=" + signature.Signature.UrlEncodeForOAuth();
			Assert.IsTrue(signature.NormalizedParameters.EndsWith(signatureEnd), "The normalized parameters should end with the signature.");
		}

		[Test(Description = "Verifies that the signature returned is a hash and not plaintext.")]
		public void BuildSignature_SignatureEncrypted()
		{
			TestInfoProvider provider = new TestInfoProvider();
			OAuthSignatureBuilder client = new OAuthSignatureBuilder(provider);
			OAuthSignature signature = client.BuildSignature(provider.AccessTokenUrl, null, WebRequestMethod.GET, OAuthRequestType.GetProtectedResources);
			Assert.IsFalse(signature.Signature.EndsWith(signature.NormalizedParameters), "The signature should be a hash of the signature base.");
		}

		[Test(Description = "OAuth 1.0a 6.3.1: Additional request parameters are not present for getting an access token.")]
		[TestCase(OAuthRequestType.GetAccessToken)]
		public void BuildSignatureBase_AdditionalRequestParametersNotPresentForCertainRequestTypes(OAuthRequestType requestType)
		{
			TestInfoProvider provider = new TestInfoProvider();
			OAuthSignatureBuilder client = new OAuthSignatureBuilder(provider);
			Dictionary<string, string> parameters = new Dictionary<string, string>()
			{
				{"a", "1"},
				{"z&", "2%"}
			};
			OAuthSignature signatureBase = client.BuildSignatureBase(provider.AccessTokenUrl, parameters, WebRequestMethod.GET, requestType);
			Assert.IsFalse(signatureBase.Signature.Contains("a=1"), "The first additional parameter should not be there.");
			Assert.IsFalse(signatureBase.Signature.Contains("z%26=2%25"), "The second additional parameter should not be there.");
		}

		[Test(Description = "OAuth 1.0a 6.1.1, 7: Additional request parameters are present for getting a request token and accessing resources.")]
		[TestCase(OAuthRequestType.GetProtectedResources)]
		[TestCase(OAuthRequestType.GetRequestToken)]
		public void BuildSignatureBase_AdditionalRequestParametersPresentForCertainRequestTypes(OAuthRequestType requestType)
		{
			TestInfoProvider provider = new TestInfoProvider();
			OAuthSignatureBuilder client = new OAuthSignatureBuilder(provider);
			Dictionary<string, string> parameters = new Dictionary<string, string>()
			{
				{"a", "1"},
				{"z&", "2%"}
			};
			OAuthSignature signatureBase = client.BuildSignatureBase(provider.AccessTokenUrl, parameters, WebRequestMethod.GET, requestType);
			Assert.IsTrue(signatureBase.Signature.Contains("a%3D1"), "The first additional parameter was not there.");
			Assert.IsTrue(signatureBase.Signature.Contains("z%2526%3D2%2525"), "The second additional parameter was not there.");
		}

		[Test(Description = "OAuth 1.0a 6.1.1: A request token contains a callback value of 'oob' when using out-of-band configuration.")]
		[TestCase(OAuthRequestType.GetAccessToken, null)]
		[TestCase(OAuthRequestType.GetProtectedResources, null)]
		[TestCase(OAuthRequestType.GetRequestToken, "oob")]
		public void BuildSignatureBase_CallbackValue(OAuthRequestType requestType, string callbackValue)
		{
			TestInfoProvider provider = new TestInfoProvider();
			OAuthSignatureBuilder client = new OAuthSignatureBuilder(provider);
			OAuthSignature signatureBase = client.BuildSignatureBase(provider.AccessTokenUrl, null, WebRequestMethod.GET, requestType);
			if (callbackValue != null)
			{
				Assert.IsTrue(signatureBase.Signature.Contains("oauth_callback%3D" + callbackValue), "The correct callback value was not present.");
			}
			else
			{
				Assert.IsFalse(signatureBase.Signature.Contains("oauth_callback%3D"), "The callback value should not be present.");
			}
		}

		[Test(Description = "OAuth 1.0a 6.1.1, 6.3.1, 7: A subset of oauth_* request parameters are common to all consumer request types.")]
		[TestCase(WebRequestMethod.GET, OAuthRequestType.GetAccessToken)]
		[TestCase(WebRequestMethod.GET, OAuthRequestType.GetProtectedResources)]
		[TestCase(WebRequestMethod.GET, OAuthRequestType.GetRequestToken)]
		[TestCase(WebRequestMethod.POST, OAuthRequestType.GetAccessToken)]
		[TestCase(WebRequestMethod.POST, OAuthRequestType.GetProtectedResources)]
		[TestCase(WebRequestMethod.POST, OAuthRequestType.GetRequestToken)]
		public void BuildSignatureBase_CommonRequestParameters(WebRequestMethod requestMethod, OAuthRequestType requestType)
		{
			TestInfoProvider provider = new TestInfoProvider();
			OAuthSignatureBuilder client = new OAuthSignatureBuilder(provider);
			OAuthSignature signatureBase = client.BuildSignatureBase(provider.AccessTokenUrl, null, requestMethod, requestType);
			Assert.IsTrue(signatureBase.Signature.Contains("oauth_consumer_key%3D"), "The consumer key was not present.");
			Assert.IsTrue(signatureBase.Signature.Contains("oauth_signature_method%3DHMAC-SHA1"), "The signature method was not present.");
			Assert.IsTrue(signatureBase.Signature.Contains("oauth_timestamp%3D"), "The timestamp was not present.");
			Assert.IsTrue(signatureBase.Signature.Contains("oauth_nonce%3D"), "The nonce was not present.");
			Assert.IsTrue(signatureBase.Signature.Contains("oauth_version%3D1.0"), "The version was not present.");
		}

		[Test(Description = "OAuth 1.0a 9.1.1: Normalized parameters are in alpha order by name.")]
		public void BuildSignatureBase_NormalizedParameterOrder()
		{
			TestInfoProvider provider = new TestInfoProvider();
			OAuthSignatureBuilder client = new OAuthSignatureBuilder(provider);
			Dictionary<string, string> parameters = new Dictionary<string, string>()
			{
				{"a", "1"},
				{"z&", "2%"}
			};
			OAuthSignature signatureBase = client.BuildSignatureBase(provider.AccessTokenUrl, parameters, WebRequestMethod.GET, OAuthRequestType.GetProtectedResources);
			string[] originalPairs = signatureBase.NormalizedParameters.Split('&');
			List<string> sortedPairs = new List<string>(originalPairs);
			sortedPairs.Sort();
			for (int i = 0; i < originalPairs.Length; i++)
			{
				Assert.AreEqual(originalPairs[i], sortedPairs[i], "The sorted list did not match the parsed list. Parameters were not in the right order.");
			}
		}

		[Test(Description = "OAuth 1.0a 9.1.3: Signature is request method, simplified request URL, and normalized request parameters.")]
		public void BuildSignatureBase_SignatureComponentsPresent()
		{
			TestInfoProvider provider = new TestInfoProvider();
			OAuthSignatureBuilder client = new OAuthSignatureBuilder(provider);
			Dictionary<string, string> parameters = new Dictionary<string, string>()
			{
				{"a", "1"},
				{"z&", "2%"}
			};
			OAuthSignature signatureBase = client.BuildSignatureBase(provider.AccessTokenUrl, parameters, WebRequestMethod.GET, OAuthRequestType.GetProtectedResources);
			string signatureStart = "GET&" + provider.AccessTokenUrl.AbsoluteUri.UrlEncodeForOAuth() + "&";
			Assert.IsTrue(signatureBase.Signature.StartsWith(signatureStart), "The signature did not begin with the request method and URL.");
			Assert.IsTrue(signatureBase.Signature.EndsWith(signatureBase.NormalizedParameters.UrlEncodeForOAuth()), "The signature did not end with the normalized parameters.");
			Assert.AreEqual(signatureStart + signatureBase.NormalizedParameters.UrlEncodeForOAuth(), signatureBase.Signature, "The signature had additional incorrect components.");
		}

		[Test(Description = "OAuth 1.0a 9.1.3: URL in signature should be simplified (no query string).")]
		public void BuildSignatureBase_SignatureUrlIsSimplified()
		{
			TestInfoProvider provider = new TestInfoProvider();
			OAuthSignatureBuilder client = new OAuthSignatureBuilder(provider);
			Uri complexUrl = new Uri(provider.AccessTokenUrl.AbsoluteUri + "?query1=1");
			OAuthSignature signatureBase = client.BuildSignatureBase(complexUrl, null, WebRequestMethod.GET, OAuthRequestType.GetProtectedResources);
			string[] parameters = signatureBase.Signature.Split('&');
			Assert.AreEqual(provider.AccessTokenUrl.AbsoluteUri.UrlEncodeForOAuth(), parameters[1], "The second item in the signature should be the simplified URL.");
		}

		[Test(Description = "OAuth 1.0a 6.3.1, 7: A request for an access token requires a request token; a request for protected resources requires an access token.")]
		[TestCase(OAuthRequestType.GetAccessToken, null)]
		[TestCase(OAuthRequestType.GetAccessToken, "")]
		[TestCase(OAuthRequestType.GetProtectedResources, null)]
		[TestCase(OAuthRequestType.GetProtectedResources, "")]
		public void BuildSignatureBase_TokenRequiredForCertainRequestTypes(OAuthRequestType requestType, string token)
		{
			TestInfoProvider provider = new TestInfoProvider();
			provider.Token = token;
			OAuthSignatureBuilder client = new OAuthSignatureBuilder(provider);
			Assert.Throws<ArgumentException>(() => client.BuildSignatureBase(provider.AccessTokenUrl, null, WebRequestMethod.GET, requestType));
		}

		[Test(Description = "OAuth 1.0a 6.3.1: The only request type that uses a verifier (PIN) code is getting an access token.")]
		[TestCase(OAuthRequestType.GetProtectedResources, null)]
		[TestCase(OAuthRequestType.GetProtectedResources, "")]
		[TestCase(OAuthRequestType.GetRequestToken, null)]
		[TestCase(OAuthRequestType.GetRequestToken, "")]
		public void BuildSignatureBase_VerifierNotRequiredForCertainRequestTypes(OAuthRequestType requestType, string pin)
		{
			TestInfoProvider provider = new TestInfoProvider();
			provider.Verifier = pin;
			OAuthSignatureBuilder client = new OAuthSignatureBuilder(provider);
			Assert.IsNotNull(client.BuildSignatureBase(provider.AccessTokenUrl, null, WebRequestMethod.GET, requestType), "A signature base should have been constructed.");
		}

		[Test(Description = "OAuth 1.0a 6.3.1: A request for an access token requires a verifier (PIN) code.")]
		[TestCase(OAuthRequestType.GetAccessToken, null)]
		[TestCase(OAuthRequestType.GetAccessToken, "")]
		public void BuildSignatureBase_VerifierRequiredForCertainRequestTypes(OAuthRequestType requestType, string pin)
		{
			TestInfoProvider provider = new TestInfoProvider();
			provider.Verifier = pin;
			OAuthSignatureBuilder client = new OAuthSignatureBuilder(provider);
			Assert.Throws<ArgumentException>(() => client.BuildSignatureBase(provider.AccessTokenUrl, null, WebRequestMethod.GET, requestType));
		}

		[Test(Description = "Attempts to construct a signature builder with a null info provider.")]
		public void Ctor_NullInfoProvider()
		{
			Assert.Throws<ArgumentNullException>(() => new OAuthSignatureBuilder(null));
		}

		[Test(Description = "OAuth 1.0a 8: Each generated nonce is unique.")]
		public void GenerateNonce_DifferentEachTime()
		{
			OAuthSignatureBuilder client = new OAuthSignatureBuilder(new TestInfoProvider());
			List<string> generated = new List<string>();
			for (int i = 0; i < 1000; i++)
			{
				string nonce = client.GenerateNonce();
				Assert.False(generated.Contains(nonce), "The nonce generated was already found in the list.");
				generated.Add(nonce);
			}
		}

		[Test(Description = "OAuth 1.0a 8: A nonce is non-empty.")]
		public void GenerateNonce_NonEmpty()
		{
			OAuthSignatureBuilder client = new OAuthSignatureBuilder(new TestInfoProvider());
			string nonce = client.GenerateNonce();
			Assert.IsNotNull(nonce, "The nonce should not be null.");
			Assert.IsTrue(nonce.Length > 16, "The nonce should be at least 16 characters long.");
		}

		[Test(Description = "Ensure that the nonces we use are numeric only to avoid dealing with multi-encoding problems.")]
		public void GenerateNonce_NumericOnly()
		{
			OAuthSignatureBuilder client = new OAuthSignatureBuilder(new TestInfoProvider());
			for (int i = 0; i < 1000; i++)
			{
				string nonce = client.GenerateNonce();
				Int64 parsed = Convert.ToInt64(nonce);
				Assert.IsTrue(parsed > 0, "The nonce should always be positive.");
			}
		}

		[Test(Description = "OAuth 1.0a 8: A timestamp should be the number of seconds since 1/1/1970 GMT.")]
		public void GenerateTimestamp_Value()
		{
			OAuthSignatureBuilder client = new OAuthSignatureBuilder(new TestInfoProvider());
			Int64 now = (Int64)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
			Int64 stamp = client.GenerateTimestamp();
			Assert.IsTrue(stamp >= now, "The timestamp generated should be after the timestamp in test setup.");
		}
	}
}
