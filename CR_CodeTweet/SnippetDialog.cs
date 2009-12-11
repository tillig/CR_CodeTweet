using System;
using System.Windows.Forms;

namespace CR_CodeTweet
{
	/// <summary>
	/// Dialog box that gathers information from the user about the content of the
	/// code snippet to post.
	/// </summary>
	public partial class SnippetDialog : Form
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SnippetDialog"/> class.
		/// </summary>
		public SnippetDialog()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Loads the dialog values into an object for use in creating a complete snippet.
		/// </summary>
		/// <returns>
		/// A <see cref="SnippetDialogResult"/> containing the values from this dialog instance.
		/// </returns>
		public SnippetDialogResult GetDialogValues()
		{
			SnippetDialogResult result = new SnippetDialogResult()
			{
				Comment = this.commentText.Text,
				Tags = this.tagText.Text,
				Title = this.titleText.Text
			};
			return result;
		}
	}
}
