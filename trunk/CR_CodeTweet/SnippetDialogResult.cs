using System;

namespace CR_CodeTweet
{
	/// <summary>
	/// Information about a snippet gathered from the <see cref="SnippetDialog"/>.
	/// </summary>
	public class SnippetDialogResult
	{
		private string _tags = null;
		private string _title = null;
		private string _comment = null;

		/// <summary>
		/// Gets or sets the snippet comment.
		/// </summary>
		/// <value>
		/// A <see cref="System.String "/> with the comment that should be associated
		/// with a snippet, or <see langword="null" /> for no comment.
		/// </value>
		/// <remarks>
		/// Whitespace will be trimmed from the beginning/end of the comment.
		/// Setting the comment to empty string or all whitespace will result in a
		/// <see langword="null" /> title.
		/// </remarks>
		public string Comment
		{
			get
			{
				return _comment;
			}
			set
			{
				_comment = this.CanonicalizeStringValue(value);
			}
		}

		/// <summary>
		/// Gets or sets the snippet tags.
		/// </summary>
		/// <value>
		/// A <see cref="System.String "/> with the comma-delimited tags that should be associated
		/// with a snippet, or <see langword="null" /> for no tags.
		/// </value>
		/// <remarks>
		/// Whitespace will be trimmed from the beginning/end of the tags.
		/// Setting the tags to empty string or all whitespace will result in a
		/// <see langword="null" /> title.
		/// </remarks>
		public string Tags
		{
			get
			{
				return _tags;
			}
			set
			{
				_tags = this.CanonicalizeStringValue(value);
			}
		}

		/// <summary>
		/// Gets or sets the snippet title.
		/// </summary>
		/// <value>
		/// A <see cref="System.String "/> with the title that should be associated
		/// with a snippet, or <see langword="null" /> for no title.
		/// </value>
		/// <remarks>
		/// Whitespace will be trimmed from the beginning/end of the title.
		/// Setting the title to empty string or all whitespace will result in a
		/// <see langword="null" /> title.
		/// </remarks>
		public string Title
		{
			get
			{
				return _title;
			}
			set
			{
				_title = this.CanonicalizeStringValue(value);
			}
		}

		private string CanonicalizeStringValue(string value)
		{
			string result = null;
			if (value != null)
			{
				result = value.Trim();
				if (result.Length == 0)
				{
					result = null;
				}
			}
			return result;
		}
	}
}
