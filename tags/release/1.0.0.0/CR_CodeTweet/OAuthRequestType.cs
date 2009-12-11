using System;

namespace CR_CodeTweet
{
	/// <summary>
	/// Enumerates the type of request that can be made via OAuth.
	/// </summary>
	/// <remarks>
	/// <para>
	/// Different signature elements are required/allowed per OAuth spec given
	/// the type of request being made.
	/// </para>
	/// </remarks>
	public enum OAuthRequestType
	{
		/// <summary>
		/// A request is being made to get a "request token."
		/// </summary>
		GetRequestToken,

		/// <summary>
		/// A request is being made to get an "access token."
		/// </summary>
		GetAccessToken,

		/// <summary>
		/// A request is being made to access some protected resources.
		/// </summary>
		GetProtectedResources
	}
}
