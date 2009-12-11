using System;

namespace CR_CodeTweet
{
	/// <summary>
	/// Contains the components that make up a formatted/normalized OAuth signature.
	/// </summary>
	public class OAuthSignature
	{
		/// <summary>
		/// Gets or sets the signature.
		/// </summary>
		/// <value>
		/// A base-64 encoded string containing the signature after normalizing
		/// the request parameters.
		/// </value>
		public string Signature { get; set; }

		/// <summary>
		/// Gets or sets the normalized parameters.
		/// </summary>
		/// <value>
		/// The set of request parameters normalized and reordered for an
		/// OAuth request.
		/// </value>
		public string NormalizedParameters { get; set; }
	}
}
