using System;

namespace CR_CodeTweet
{
	/// <summary>
	/// Data storage class for Twitter user data.
	/// </summary>
	public class TwitterUserInfo
	{
		/// <summary>
		/// Gets or sets the access token.
		/// </summary>
		/// <value>
		/// The OAuth access token to use for communicating with Twitter.
		/// </value>
		public string AccessToken { get; set; }

		/// <summary>
		/// Gets or sets the access token secret.
		/// </summary>
		/// <value>
		/// The OAuth access token secret to use for communicating with Twitter.
		/// </value>
		public string AccessTokenSecret { get; set; }

		/// <summary>
		/// Gets or sets the OAuth verification PIN.
		/// </summary>
		/// <value>
		/// The PIN given to the user to verify OAuth access.
		/// </value>
		public string Verifier { get; set; }
	}
}
