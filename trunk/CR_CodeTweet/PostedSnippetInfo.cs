using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CR_CodeTweet
{
	/// <summary>
	/// Contains information about a snippet posted to CodePaste.NET.
	/// </summary>
	public class PostedSnippetInfo
	{
		private string _id;

		/// <summary>
		/// Gets or sets the snippet ID.
		/// </summary>
		/// <value>
		/// A <see cref="System.String"/> that contains the ID for the posted snippet.
		/// </value>
		/// <remarks>
		/// <para>
		/// Setting this property updates the <see cref="CR_CodeTweet.PostedSnippetInfo.Url"/>
		/// property. Setting it to <see langword="null" /> or empty will result
		/// in a <see langword="null" /> URL.
		/// </para>
		/// </remarks>
		public string Id
		{
			get
			{
				return _id;
			}
			set
			{
				if (String.IsNullOrEmpty(value))
				{
					this._id = null;
					this.Url = null;
				}
				else
				{
					this._id = value;
					this.Url = new Uri("http://codepaste.net/" + value);
				}
			}
		}

		/// <summary>
		/// Gets or sets the URL to the snippet.
		/// </summary>
		/// <value>
		/// A <see cref="System.Uri"/> that contains the URL to view the snippet.
		/// </value>
		public Uri Url { get; private set; }

		/// <summary>
		/// Gets or sets the CodePaste.NET session key.
		/// </summary>
		/// <value>
		/// A <see cref="System.String"/> that has the session key used to post
		/// the snippet.
		/// </value>
		public string SessionKey { get; set; }
	}
}
