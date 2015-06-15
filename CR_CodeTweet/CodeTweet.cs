using System;
using System.Globalization;
using System.Net;
using System.Web.Services.Protocols;
using System.Windows.Forms;
using CR_CodeTweet.CodePaste;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.Diagnostics.Menus;
using DevExpress.CodeRush.Interop.OLE;
using DevExpress.CodeRush.Menus;
using DevExpress.CodeRush.PlugInCore;

namespace CR_CodeTweet
{
	/// <summary>
	/// DXCore plugin allowing a user to select some code to send to CodePaste.NET
	/// and then send a link to that code via Twitter.
	/// </summary>
	public partial class CodeTweet : StandardPlugIn
	{
		/// <summary>
		/// Gets or sets the current context menu button.
		/// </summary>
		/// <value>
		/// An <see cref="DevExpress.CodeRush.Menus.IMenuButton"/> containing the
		/// current instance of the context menu, or <see langword="null" /> if
		/// there is no context menu instance.
		/// </value>
		public IMenuButton ContextMenuButton { get; private set; }

		/// <summary>
		/// Gets a value indicating if some selected code can be tweeted.
		/// </summary>
		/// <value>
		/// <see langword="true" /> if there is code to be tweeted, otherwise <see langword="false" />.
		/// </value>
		public bool Available
		{
			get
			{
				return CodeRush.Selection.Exists;
			}
		}

		/// <summary>
		/// Adds the context menu entry to the editor context menu.
		/// </summary>
		public void AddContextMenu()
		{
			MenuBar editorContextMenu = DevExpress.CodeRush.VSCore.Manager.Menus.Bars[VsCommonBar.EditorContext];
			this.ContextMenuButton = editorContextMenu.AddButton();
			this.ContextMenuButton.Style = ButtonStyle.IconAndCaption;
			this.ContextMenuButton.Caption = Properties.Resources.ContextMenu_TweetButton;
			this.ContextMenuButton.Enabled = this.Available;
			try
			{
				this.ContextMenuButton.DescriptionText = Properties.Resources.ContextMenu_TweetButtonDescription;
			}
			catch (InvalidOperationException)
			{
				// VS2010 doesn't allow you to set the description text,
				// but VS2008 does.
			}
			this.ContextMenuButton.SetFace(Properties.Resources.ContextButtonIcon.ToBitmap(), Properties.Resources.ContextButtonIconMask.ToBitmap());
			this.ContextMenuButton.Click += new MenuButtonClickEventHandler(ContextMenuButton_Click);
		}

		/// <summary>
		/// Executes the CodeTweet action.
		/// </summary>
		/// <param name="ea">
		/// The <see cref="DevExpress.CodeRush.Core.ExecuteEventArgs"/> instance containing the event data.
		/// </param>
		private void CodeTweetAction_Execute(ExecuteEventArgs ea)
		{
			this.TweetSelectedCode();
		}

		/// <summary>
		/// Handles the click event for the context menu button.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="DevExpress.CodeRush.Menus.MenuButtonClickEventArgs"/> instance containing the event data.</param>
		private void ContextMenuButton_Click(object sender, MenuButtonClickEventArgs e)
		{
			this.TweetSelectedCode();
		}

		/// <summary>
		/// Starts the context-menu rendering process.
		/// </summary>
		/// <param name="ea">The <see cref="DevExpress.CodeRush.Core.EditorMouseEventArgs"/> instance containing the event data.</param>
		private void CodeTweet_EditorMouseDown(EditorMouseEventArgs ea)
		{
			// Check to see if this is a right-mouse-click - don't handle non-right buttons
			if (ea.Button != MouseButtons.Right)
			{
				return;
			}

			// Rebuild the context menu
			this.RemoveContextMenu();
			if (this.Available)
			{
				this.AddContextMenu();
			}
		}

		/// <summary>
		/// Determines if the CodeTweet action is available.
		/// </summary>
		/// <param name="ea">
		/// The <see cref="DevExpress.CodeRush.Core.QueryStatusEventArgs"/> instance containing the event data.
		/// </param>
		/// <remarks>
		/// <para>
		/// If <see cref="CR_CodeTweet.CodeTweet.Available"/> is <see langword="true" />,
		/// this command is enabled and supported.  If <see cref="CR_CodeTweet.CodeTweet.Available"/>
		/// is <see langword="false" />, this command is unsupported.
		/// </para>
		/// </remarks>
		private void CodeTweetAction_QueryStatus(QueryStatusEventArgs ea)
		{
			if (!this.Available)
			{
				ea.CommandFlags = OLECMDF.OLECMDF_DEFHIDEONCTXTMENU;
			}
			else
			{
				ea.CommandFlags = OLECMDF.OLECMDF_ENABLED | OLECMDF.OLECMDF_SUPPORTED;
			}
		}

		/// <summary>
		/// Creates a complete code snippet to post given all of the options gathered.
		/// </summary>
		/// <param name="code">The code to put in the snippet. Generally this is the current selection.</param>
		/// <param name="language">The language the snippet is in. This should be the CodeRush language and will be mapped to the CodePaste.NET equivalent here.</param>
		/// <param name="dialogInfo">Additional information for the snippet retrieved from interaction.</param>
		/// <returns>
		/// A fully populated code snippet with all of the data combined from the various
		/// provided parameters.
		/// </returns>
		/// <exception cref="System.ArgumentNullException">
		/// Thrown if <paramref name="code" /> or <paramref name="dialogInfo" /> is <see langword="null" />.
		/// </exception>
		/// <exception cref="System.ArgumentException">
		/// Thrown if <paramref name="code" /> is empty.
		/// </exception>
		public CodeSnippet CreateCodeSnippet(string code, string language, SnippetDialogResult dialogInfo)
		{
			if (code == null)
			{
				throw new ArgumentNullException("code");
			}
			if (code.Length == 0)
			{
				throw new ArgumentException("You must post at least one character in the snippet.", "code");
			}
			if (dialogInfo == null)
			{
				throw new ArgumentNullException("dialogInfo");
			}
			string snippetLanguage = Map.CodePasteSyntaxHighlighterNone;
			Map.LanguageIdToSyntaxHighlighter.TryGetValue(language, out snippetLanguage);

			CodeSnippet snippet = new CodeSnippet();
			snippet.Code = code;
			snippet.Comment = dialogInfo.Comment;
			snippet.Entered = DateTime.Now;
			snippet.Language = snippetLanguage;
			snippet.ShowLineNumbers = true;
			snippet.Tags = dialogInfo.Tags;
			snippet.Title = dialogInfo.Title;
			return snippet;
		}

		/// <summary>
		/// Displays a dialog that gathers additional information about the code snippet to post.
		/// </summary>
		/// <returns>
		/// A <see cref="SnippetDialogResult"/> with additional data about the snippet
		/// to post, or <see langword="null" /> if the user chooses to cancel posting.
		/// </returns>
		public SnippetDialogResult GetAdditionalSnippetInfo()
		{
			SnippetDialogResult snippetDialogResult = null;
			using (SnippetDialog snippetDialog = new SnippetDialog())
			{
				if (snippetDialog.ShowDialog() == DialogResult.OK)
				{
					snippetDialogResult = snippetDialog.GetDialogValues();
				}
			}
			return snippetDialogResult;
		}

		/// <summary>
		/// Initializes the plug in.
		/// </summary>
		public override void InitializePlugIn()
		{
			base.InitializePlugIn();
			this.EditorMouseDown += new EditorMouseEventHandler(CodeTweet_EditorMouseDown);
		}

		/// <summary>
		/// Loads and validates options. If there is anything misconfigured, the user is prompted to configure.
		/// </summary>
		/// <returns>
		/// A valid set of <see cref="CR_CodeTweet.Options"/> if things are configured
		/// correctly or if the user elects to configure the misconfigured bits;
		/// <see langword="null" /> if things are misconfigured and the user chooses
		/// not to set things up.
		/// </returns>
		public Options LoadValidOptions()
		{
			string message = null;
			do
			{
				Options options = Options.Load(PluginOptions.Storage);
				message = options.Validate();
				if (message == null)
				{
					return options;
				}

				message = String.Format(CultureInfo.CurrentUICulture, "{0}{2}{2}{1}", message, Properties.Resources.Options_WantToConfigure, Environment.NewLine);
				DialogResult result = MessageBox.Show(message, Properties.Resources.Options_NotConfiguredTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
				if (result == DialogResult.Yes)
				{
					PluginOptions.Show();
				}
				else
				{
					return null;
				}
			} while (message != null);

			return null;
		}

		/// <summary>
		/// Removes the current context menu instance from the editor context menu.
		/// </summary>
		public void RemoveContextMenu()
		{
			if (this.ContextMenuButton != null)
			{
				this.ContextMenuButton.Delete();
				this.ContextMenuButton = null;
			}
		}

		/// <summary>
		/// Sends the currently selected code to CodePaste.NET, gets a link to the
		/// paste, and sets up a Twitter message with that link.
		/// </summary>
		public void TweetSelectedCode()
		{
			if (!this.Available)
			{
				return;
			}

			// Get the set of valid options and bail if we can't.
			Options options = this.LoadValidOptions();
			if (options == null)
			{
				return;
			}

			// Get the user input portion of the snippet. Result will be null if user cancels.
			SnippetDialogResult snippetDialogResult = GetAdditionalSnippetInfo();
			if (snippetDialogResult == null)
			{
				return;
			}

			// Combine everything to create the snippet to post.
			CodeSnippet snippet = this.CreateCodeSnippet(CodeRush.Selection.Text, CodeRush.Documents.Active.Language, snippetDialogResult);
			PostedSnippetInfo snippetInfo = null;

			try
			{
				Log.SendMsg("CR_CodeTweet: posting and tweeting.");

				snippetInfo = PostSnippetToCodePaste(options, snippet);

				TweetDialog tweetDialog = new TweetDialog(snippetInfo.Url);
				if (DialogResult.OK == tweetDialog.ShowDialog())
				{
					this.PostTweetContent(options, tweetDialog.TweetContent);
				}
				else if (
					DialogResult.Yes == MessageBox.Show(
						Properties.Resources.Dialog_TweetCanceledMessage,
						Properties.Resources.Dialog_TweetCanceledTitle,
						MessageBoxButtons.YesNo,
						MessageBoxIcon.Question)
					)
				{
					// The user canceled the tweet - ask if they want to delete
					// the snippet and, if so, delete it.
					try
					{
						Log.Send("User elected to delete snippet after canceling tweet.");
						this.DeleteSnippetFromCodePaste(snippetInfo);
					}
					catch (TweetFailedException)
					{
						// If deleting the snippet fails, we don't want to ask
						// the user to try to delete AGAIN later in the error
						// handling block.
						snippetInfo = null;
						throw;
					}
				}
			}
			catch (TweetFailedException tfe)
			{
				Log.SendException("Failed to tweet. Alerting user and prompting to update settings.", tfe);

				// Tell the user what happened...
				MessageBox.Show(tfe.Message, Properties.Resources.Dialog_TweetFailedTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

				// ...if they posted a snippet but didn't finish tweeting, see if they want to delete the snippet...
				if (
					snippetInfo != null &&
					DialogResult.Yes == MessageBox.Show(
						Properties.Resources.Dialog_TweetFailedDeleteSnippetMessage,
						Properties.Resources.Dialog_TweetFailedTitle,
						MessageBoxButtons.YesNo,
						MessageBoxIcon.Question)
					)
				{
					this.DeleteSnippetFromCodePaste(snippetInfo);
				}

				// ...and, finally, ask if they'd like to check configuration settings.
				string message = String.Format(CultureInfo.CurrentUICulture, "{0}{2}{2}{1}", Properties.Resources.Dialog_TweetFailedMessage, Properties.Resources.Options_WantToConfigure, Environment.NewLine);
				DialogResult result = MessageBox.Show(message, Properties.Resources.Dialog_TweetFailedTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
				if (result == DialogResult.Yes)
				{
					PluginOptions.Show();
				}
			}
			catch (Exception ex)
			{
				Log.SendException("Unexpected exception while posting snippet/tweeting.", ex);

				// Tell the user we basically have to throw up our hands for now.
				string message = String.Format(CultureInfo.CurrentUICulture, "{0}{2}{2}{1}", Properties.Resources.Dialog_UnexpectedException, ex.Message, Environment.NewLine);
				MessageBox.Show(message, Properties.Resources.Dialog_TweetFailedTitle, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
			}
		}

		/// <summary>
		/// Logs in and posts a snippet to CodePaste.NET.
		/// </summary>
		/// <param name="options">The options (credentials, etc.) with which authentication should be performed.</param>
		/// <param name="snippet">The populated snippet to post.</param>
		/// <returns>
		/// Information about the posted snippet that can be used to compose a tweet.
		/// </returns>
		/// <exception cref="TweetFailedException">
		/// Thrown if the snippet can't be posted.
		/// </exception>
		/// <exception cref="System.ArgumentNullException">
		/// Thrown if <paramref name="options" /> or <paramref name="snippet" /> is <see langword="null" />.
		/// </exception>
		public PostedSnippetInfo PostSnippetToCodePaste(Options options, CodeSnippet snippet)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			if (snippet == null)
			{
				throw new ArgumentNullException("snippet");
			}

			// After this block, we should have a valid CodePaste.NET session key
			// and a URL to the posted snippet.
			PostedSnippetInfo postedSnippetInfo = new PostedSnippetInfo();
			using (ProgressDialog progress = new ProgressDialog())
			using (CodePasteSoapService codePaste = new CodePasteSoapService())
			{
				try
				{
					progress.Show();

					try
					{
						Log.Send("Getting CodePaste session...");
						progress.StatusMessage = Properties.Resources.Progress_ConnectingToCodePaste;
						postedSnippetInfo.SessionKey = codePaste.GetSessionKey(options.CodePasteUsername, options.CodePastePassword);
						progress.StatusMessage = Properties.Resources.Progress_ConnectedToCodePaste;
						Log.Send("Got CodePaste session.");
					}
					catch (SoapException ex)
					{
						Log.SendException("Failed to get CodePaste session.", ex);
						progress.StatusMessage = Properties.Resources.Progress_UnableToGetCodePasteSession;
						throw new TweetFailedException(Properties.Resources.CodePaste_UnableToLoginMessage, ex);
					}

					try
					{
						Log.Send("Posting snippet to CodePaste.NET...");
						CodeSnippet postedSnippet = codePaste.PostNewCodeSnippet(snippet, postedSnippetInfo.SessionKey);
						Log.Send("Snippet posted.");
						progress.StatusMessage = Properties.Resources.Progress_PostedSnippet;
						postedSnippetInfo.Id = postedSnippet.Id;
					}
					catch (SoapException ex)
					{
						Log.SendException("Failed to get CodePaste session.", ex);
						progress.StatusMessage = Properties.Resources.Progress_UnableToPostSnippet;
						throw new TweetFailedException(Properties.Resources.CodePaste_UnableToPostSnippet, ex);
					}
				}
				finally
				{
					progress.Hide();
				}
			}
			return postedSnippetInfo;
		}

		/// <summary>
		/// Deletes a snippet from CodePaste.NET.
		/// </summary>
		/// <param name="snippetInfo">The information about the posted snippet.</param>
		/// <exception cref="TweetFailedException">
		/// Thrown if the snippet can't be deleted.
		/// </exception>
		/// <exception cref="System.ArgumentNullException">
		/// Thrown if <paramref name="snippetInfo" /> is <see langword="null" />.
		/// </exception>
		public void DeleteSnippetFromCodePaste(PostedSnippetInfo snippetInfo)
		{
			if (snippetInfo == null)
			{
				throw new ArgumentNullException("snippetInfo");
			}
			using (ProgressDialog progress = new ProgressDialog())
			using (CodePasteSoapService codePaste = new CodePasteSoapService())
			{
				try
				{
					progress.Show();
					Log.Send("Deleting snippet...");
					progress.StatusMessage = Properties.Resources.Progress_DeletingSnippet;
					codePaste.DeleteSnippet(snippetInfo.Id, snippetInfo.SessionKey);
					Log.Send("Snippet deleted.");
					MessageBox.Show(Properties.Resources.Dialog_SnippetDeletedMessage, Properties.Resources.Dialog_SnippetDeletedTitle);
				}
				catch (SoapException ex)
				{
					Log.SendException("Failed to delete snippet.", ex);
					progress.StatusMessage = Properties.Resources.Progress_DeleteSnippetFailed;
					throw new TweetFailedException(Properties.Resources.CodePaste_UnableToDeleteSnippet, ex);
				}
				finally
				{
					progress.Hide();
				}
			}
		}

		/// <summary>
		/// Sends tweet content to Twitter.
		/// </summary>
		/// <param name="options">The options (credentials, etc.) with which authentication should be performed.</param>
		/// <param name="tweetContent">The content of the message to send to Twitter.</param>
		/// <exception cref="System.ArgumentNullException">
		/// Thrown if <paramref name="options" /> or <paramref name="tweetContent" /> is <see langword="null" />.
		/// </exception>
		/// <exception cref="System.ArgumentException">
		/// Thrown if <paramref name="tweetContent" /> is less than one character long.
		/// </exception>
		/// <exception cref="TweetFailedException">
		/// Thrown if there's any problem communicating with Twitter.
		/// </exception>
		public void PostTweetContent(Options options, string tweetContent)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			if (tweetContent == null)
			{
				throw new ArgumentNullException("tweetContent");
			}
			if (tweetContent.Length < 1)
			{
				throw new ArgumentException("Tweet must be at least one character long.", "tweetContent");
			}
			using (ProgressDialog progress = new ProgressDialog())
			{
				try
				{
					progress.Show();
					Log.Send("Tweeting the snippet.");
					progress.StatusMessage = Properties.Resources.Progress_PostingTweet;
					TwitterInfoProvider infoProvider = new TwitterInfoProvider()
					{
						Token = options.TwitterOAuthToken,
						TokenSecret = options.TwitterOAuthTokenSecret,
						Verifier = options.TwitterOAuthPin
					};
					TwitterClient twitterClient = new TwitterClient(infoProvider);
					twitterClient.Tweet(tweetContent);
					Log.Send("Tweet sent!");
					progress.StatusMessage = Properties.Resources.Progress_PostedTweet;
					MessageBox.Show(Properties.Resources.Dialog_TweetSentMessage, Properties.Resources.Dialog_TweetSentTitle);
				}
				catch (WebException we)
				{
					// Something went wrong while talking to Twitter.
					progress.StatusMessage = Properties.Resources.Progress_TweetFailed;
					HttpWebResponse response = we.Response as HttpWebResponse;
					HttpStatusCode status = response == null ? HttpStatusCode.InternalServerError : response.StatusCode;
					Log.SendEnum("Twitter communication failed.", status);
					if (status == HttpStatusCode.Unauthorized)
					{
						throw new TweetFailedException(Properties.Resources.Twitter_AuthorizationFailed);
					}
					throw new TweetFailedException(Properties.Resources.Twitter_ServerError);
				}
				finally
				{
					progress.Hide();
				}
			}
		}
	}
}