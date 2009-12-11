using System;
using System.Web;
using System.Windows.Forms;
using DevExpress.CodeRush.Common;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.Diagnostics.Menus;

namespace CR_CodeTweet
{
	[UserLevel(UserLevel.NewUser)]
	public partial class PluginOptions : OptionsPage
	{
		/// <summary>
		/// Temporary storage for the Twitter PIN (since it doesn't display on the options form).
		/// </summary>
		private string _twitterPin;

		/// <summary>
		/// Temporary storage for the Twitter access token (since it doesn't display on the options form).
		/// </summary>
		private string _twitterAccessToken;

		/// <summary>
		/// Temporary storage for the Twitter access token secret (since it doesn't display on the options form).
		/// </summary>
		private string _twitterAccessTokenSecret;

		/// <summary>
		/// Gets the options page category.
		/// </summary>
		/// <returns>
		/// Always returns "Editor"
		/// </returns>
		public static string GetCategory()
		{
			return @"Editor";
		}

		/// <summary>
		/// Gets the name of the options page.
		/// </summary>
		/// <returns>
		/// Always returns "CodeTweet"
		/// </returns>
		public static string GetPageName()
		{
			return @"CodeTweet";
		}

		/// <summary>
		/// Initializes this instance.
		/// </summary>
		protected override void Initialize()
		{
			base.Initialize();
			this.versionLabel.Text = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
			this.twitterLink.Links.Add(0, this.twitterLink.Text.Length, "http://twitter.com");
			this.codepasteLink.Links.Add(0, this.codepasteLink.Text.Length, "http://codepaste.net");
			this.PopulateFormFromStorage(PluginOptions.Storage);
		}

		/// <summary>
		/// Handles the click event for a link.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.Windows.Forms.LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
		private void Link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
		}

		/// <summary>
		/// Handles the event when a user elects to commit changes.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="ea">The <see cref="DevExpress.CodeRush.Core.CommitChangesEventArgs"/> instance containing the event data.</param>
		private void PluginOptions_CommitChanges(object sender, CommitChangesEventArgs ea)
		{
			Options options = new Options();
			options.CodePastePassword = this.codepastePasswordText.Text;
			options.CodePasteUsername = this.codepasteUsernameText.Text;
			options.TwitterOAuthPin = this._twitterPin;
			options.TwitterOAuthToken = this._twitterAccessToken;
			options.TwitterOAuthTokenSecret = this._twitterAccessTokenSecret;
			Options.Save(PluginOptions.Storage, options);
		}

		/// <summary>
		/// Handles the event when a user elects to restore default settings.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="ea">The <see cref="DevExpress.CodeRush.Core.OptionsPageEventArgs"/> instance containing the event data.</param>
		private void PluginOptions_RestoreDefaults(object sender, OptionsPageEventArgs ea)
		{
			this.PopulateFormFromStorage(PluginOptions.Storage);
		}

		/// <summary>
		/// Loads options from a given storage location and populates the form.
		/// </summary>
		/// <param name="storage">The storage from which to load the options.</param>
		public void PopulateFormFromStorage(IDecoupledStorage storage)
		{
			Options options = Options.Load(storage);
			this.codepastePasswordText.Text = options.CodePastePassword;
			this.codepasteUsernameText.Text = options.CodePasteUsername;
			this._twitterPin = options.TwitterOAuthPin;
			this._twitterAccessToken = options.TwitterOAuthToken;
			this._twitterAccessTokenSecret = options.TwitterOAuthTokenSecret;
		}

		/// <summary>
		/// Runs the Twitter application authorization process.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void TwitterAuthorizeButton_Click(object sender, EventArgs e)
		{
			if (DialogResult.No == MessageBox.Show(
				Properties.Resources.Dialog_GetTwitterPinMessage,
				Properties.Resources.Dialog_GetTwitterPinTitle,
				MessageBoxButtons.YesNo, MessageBoxIcon.Question)
				)
			{
				return;
			}

			// In the auth process we want to start with a "blank slate"
			// provider - none of the existing PIN or token info - so
			// create a new one and use that.
			TwitterInfoProvider infoProvider = new TwitterInfoProvider();

			try
			{
				this.SendUserToGetTwitterPin(infoProvider);
			}
			catch
			{
				// Nothing to do - the error has already been logged and
				// we can't otherwise proceed.
				return;
			}

			// Info provider has been updated to store the request token now.
			// It can be used with the PIN to get an access token. Get the PIN.
			TwitterPinDialog pinDialog = new TwitterPinDialog();
			if (DialogResult.OK != pinDialog.ShowDialog())
			{
				return;
			}
			infoProvider.Verifier = pinDialog.TwitterPin;

			try
			{
				this.SendUserToGetTwitterAccessToken(infoProvider);
			}
			catch
			{
				// Nothing to do - the error has already been logged and
				// we can't otherwise proceed.
				return;
			}

			// On successful authorization, save the verifier, access token, and access token secret to storage.
			// We don't wait for an "OK" and we don't decline saving the settings
			// on "Cancel" because, as far as Twitter's concerned, it's already done.
			Options.SaveTwitterInfo(PluginOptions.Storage, infoProvider.Verifier, infoProvider.Token, infoProvider.TokenSecret);

			// Update instance fields with the values so if the user clicks OK they won't be lost.
			// These fields get loaded/saved with the rest of the options.
			this._twitterPin = infoProvider.Verifier;
			this._twitterAccessToken = infoProvider.Token;
			this._twitterAccessTokenSecret = infoProvider.TokenSecret;

			Log.Send("Successfully authorized CR_CodeTweet with Twitter.");
			MessageBox.Show(Properties.Resources.Dialog_GetTwitterPinSuccess,
				Properties.Resources.Dialog_GetTwitterPinTitle,
				MessageBoxButtons.OK,
				MessageBoxIcon.Information);
		}

		/// <summary>
		/// Takes an info provider with a request token and verifier and swaps them
		/// for an access token.
		/// </summary>
		/// <exception cref="System.ArgumentNullException">
		/// Thrown if <paramref name="infoProvider" /> is <see langword="null" />.
		/// </exception>
		public void SendUserToGetTwitterAccessToken(IOAuthInfoProvider infoProvider)
		{
			if (infoProvider == null)
			{
				throw new ArgumentNullException("infoProvider");
			}
			try
			{
				TwitterClient client = new TwitterClient(infoProvider);
				client.GetAccessToken();
			}
			catch (HttpException ex)
			{
				Log.SendException("Communication error while trying to get a Twitter access token.", ex);
				MessageBox.Show(Properties.Resources.Dialog_GetTwitterPinFailed,
					Properties.Resources.Dialog_GetTwitterPinTitle,
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
			}
			catch (Exception ex)
			{
				Log.SendException("Unexpected error while trying to get a Twitter access token.", ex);
				MessageBox.Show(Properties.Resources.Dialog_GetTwitterPinFailed,
					Properties.Resources.Dialog_GetTwitterPinTitle,
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// Opens the user's browser and sends them to the Twitter authorization page.
		/// </summary>
		/// <exception cref="System.ArgumentNullException">
		/// Thrown if <paramref name="infoProvider" /> is <see langword="null" />.
		/// </exception>
		public void SendUserToGetTwitterPin(IOAuthInfoProvider infoProvider)
		{
			if (infoProvider == null)
			{
				throw new ArgumentNullException("infoProvider");
			}
			try
			{
				TwitterClient client = new TwitterClient(infoProvider);
				Uri authorizeUrl = client.GetAuthorizationUrl();
				System.Diagnostics.Process.Start(authorizeUrl.AbsoluteUri);
			}
			catch (HttpException ex)
			{
				Log.SendException("Communication error while trying to get a Twitter PIN.", ex);
				MessageBox.Show(Properties.Resources.Dialog_GetTwitterPinFailed,
					Properties.Resources.Dialog_GetTwitterPinTitle,
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
			}
			catch (Exception ex)
			{
				Log.SendException("Unexpected error while trying to get a Twitter PIN.", ex);
				MessageBox.Show(Properties.Resources.Dialog_GetTwitterPinFailed,
					Properties.Resources.Dialog_GetTwitterPinTitle,
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
			}
		}
	}
}