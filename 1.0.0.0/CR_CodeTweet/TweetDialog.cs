using System;
using System.Windows.Forms;

namespace CR_CodeTweet
{
	/// <summary>
	/// Dialog box that gathers information from the user about the content of the
	/// Twitter message to send.
	/// </summary>
	public partial class TweetDialog : Form
	{
		/// <summary>
		/// Gets the maximum tweet length allowed.
		/// </summary>
		/// <value>
		/// A <see cref="System.Int32"/> containing the total number of characters
		/// available for the user to tweet.
		/// </value>
		public int MaxTweetLength { get; private set; }

		/// <summary>
		/// Gets the URL text that will be appended to the tweet.
		/// </summary>
		/// <value>
		/// A <see cref="System.String"/> that includes a space followed
		/// by the absolute URL to the code snippet.
		/// </value>
		public string SnippetUrlToAppend { get; private set; }

		/// <summary>
		/// Gets the built tweet content.
		/// </summary>
		/// <value>
		/// A <see cref="System.String"/> with the tweet content including the URL.
		/// </value>
		public string TweetContent
		{
			get
			{
				return this.tweetText.Text + this.SnippetUrlToAppend;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TweetDialog"/> class.
		/// </summary>
		/// <param name="snippetUrl">
		/// The URL to the code snippet on CodePaste.NET.
		/// </param>
		/// <exception cref="System.ArgumentNullException">
		/// Thrown if <paramref name="snippetUrl" /> is <see langword="null" />.
		/// </exception>
		public TweetDialog(Uri snippetUrl)
		{
			if (snippetUrl == null)
			{
				throw new ArgumentNullException("snippetUrl");
			}
			this.SnippetUrlToAppend = " " + snippetUrl.AbsoluteUri;
			this.MaxTweetLength = 140 - this.SnippetUrlToAppend.Length;
			InitializeComponent();
			this.SetInitialContent(snippetUrl);
		}

		/// <summary>
		/// Initializes the contents of the tweet box based on the URL to the
		/// posted code snippet.
		/// </summary>
		/// <param name="snippetUrl">
		/// The URL to the code snippet on CodePaste.NET.
		/// </param>
		/// <exception cref="System.ArgumentNullException">
		/// Thrown if <paramref name="snippetUrl" /> is <see langword="null" />.
		/// </exception>
		private void SetInitialContent(Uri snippetUrl)
		{
			if (snippetUrl == null)
			{
				throw new ArgumentNullException("snippetUrl");
			}
			this.snippetLink.Text = snippetUrl.AbsoluteUri;
			this.snippetLink.Links.Add(0, this.snippetLink.Text.Length, this.snippetLink.Text);
			this.tweetText.MaxLength = this.MaxTweetLength;
			this.charsRemaining.Text = this.tweetText.MaxLength.ToString();
		}

		/// <summary>
		/// Enables/disables the OK button based on the content of the tweet text.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void tweetText_TextChanged(object sender, EventArgs e)
		{
			this.okButton.Enabled = this.tweetText.Text.Length > 0;
			this.charsRemaining.Text = (this.tweetText.MaxLength - this.tweetText.Text.Length).ToString();
		}

		/// <summary>
		/// Handles the click event for a link.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
		private void snippetLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
		}

		/// <summary>
		/// Sets up first focus.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void TweetDialog_Shown(object sender, EventArgs e)
		{
			this.tweetText.Focus();
		}
	}
}
