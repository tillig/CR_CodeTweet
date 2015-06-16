using System;
using TweetSharp;

namespace CR_CodeTweet
{
	/// <summary>
	/// Provides information for the CR_CodeTweet application to access Twitter.
	/// </summary>
	internal static class TwitterClientInfoProvider
	{
		/// <summary>
		/// The registered CR_CodeTweet consumer key.
		/// </summary>
		private static readonly string ConsumerKey = "DxsRq1Q42tJlI1GUOYt0Q";

		/// <summary>
		/// The registered CR_CodeTweet consumer secret.
		/// </summary>
		private static readonly string ConsumerSecret = "vmvHdxKK3HHsPDkWwsZ8KLVU3Z2JceGSHs391YAHiwE";

		public static TwitterClientInfo ClientInfo
		{
			get
			{
				return new TwitterClientInfo { ConsumerKey = ConsumerKey, ConsumerSecret = ConsumerSecret };
			}
		}
	}
}
