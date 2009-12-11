using System;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.Common;

namespace CR_CodeTweet
{
	/// <summary>
	/// Data storage for the available options.
	/// </summary>
	public class Options
	{
		private const string SectionId = "CR_CodeTweet";
		private const string CodePasteUsernameKey = "CodePasteUsername";
		private const string CodePastePasswordKey = "CodePastePassword";
		private const string TwitterOAuthPinKey = "TwitterOAuthPin";
		private const string TwitterOAuthTokenKey = "TwitterOAuthToken";
		private const string TwitterOAuthTokenSecretKey = "TwitterOAuthTokenSecret";

		/// <summary>
		/// Gets or sets the CodePaste.NET username.
		/// </summary>
		/// <value>
		/// A <see cref="System.String"/> with the username that should be used
		/// to authenticate to CodePaste.NET.
		/// </value>
		public string CodePasteUsername { get; set; }

		/// <summary>
		/// Gets or sets the CodePaste.NET password.
		/// </summary>
		/// <value>
		/// A <see cref="System.String"/> with the password that should be used
		/// to authenticate to CodePaste.NET.
		/// </value>
		public string CodePastePassword { get; set; }

		/// <summary>
		/// Gets or sets the Twitter OAuth PIN.
		/// </summary>
		/// <value>
		/// A <see cref="System.String"/> with the PIN that should be used to
		/// authenticate via OAuth to Twitter.
		/// </value>
		public string TwitterOAuthPin { get; set; }

		/// <summary>
		/// Gets or sets the Twitter OAuth access token.
		/// </summary>
		/// <value>
		/// A <see cref="System.String"/> with the access token that should be used to
		/// authenticate via OAuth to Twitter.
		/// </value>
		public string TwitterOAuthToken { get; set; }

		/// <summary>
		/// Gets or sets the Twitter OAuth access token secret.
		/// </summary>
		/// <value>
		/// A <see cref="System.String"/> with the access token secret that should be used to
		/// authenticate via OAuth to Twitter.
		/// </value>
		public string TwitterOAuthTokenSecret { get; set; }

		/// <summary>
		/// Loads the plugin options.
		/// </summary>
		/// <param name="storage">
		/// The storage location from which options should be loaded.
		/// </param>
		/// <returns>
		/// A populated options object with the current options settings.
		/// </returns>
		/// <exception cref="System.ArgumentNullException">
		/// Thrown if <paramref name="storage" /> is <see langword="null" />.
		/// </exception>
		public static Options Load(IDecoupledStorage storage)
		{
			if (storage == null)
			{
				throw new ArgumentNullException("storage");
			}
			Options options = new Options();
			options.CodePastePassword = storage.ReadEncryptedString(SectionId, CodePastePasswordKey, "");
			options.CodePasteUsername = storage.ReadEncryptedString(SectionId, CodePasteUsernameKey, "");
			options.TwitterOAuthPin = storage.ReadEncryptedString(SectionId, TwitterOAuthPinKey, "");
			options.TwitterOAuthToken = storage.ReadEncryptedString(SectionId, TwitterOAuthTokenKey, "");
			options.TwitterOAuthTokenSecret = storage.ReadEncryptedString(SectionId, TwitterOAuthTokenSecretKey, "");
			return options;
		}

		/// <summary>
		/// Saves the plugin options.
		/// </summary>
		/// <param name="storage">
		/// The storage location to which options should be saved.
		/// </param>
		/// <param name="options">
		/// The populated options set to save.
		/// </param>
		/// <exception cref="System.ArgumentNullException">
		/// Thrown if <paramref name="storage" /> or <paramref name="options" /> is <see langword="null" />.
		/// </exception>
		public static void Save(IDecoupledStorage storage, Options options)
		{
			if (storage == null)
			{
				throw new ArgumentNullException("storage");
			}
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			storage.WriteEncryptedString(SectionId, CodePastePasswordKey, options.CodePastePassword);
			storage.WriteEncryptedString(SectionId, CodePasteUsernameKey, options.CodePasteUsername);
			SaveTwitterInfo(storage, options.TwitterOAuthPin, options.TwitterOAuthToken, options.TwitterOAuthTokenSecret);
			storage.Update();
		}

		/// <summary>
		/// Saves the Twitter authorization info.
		/// </summary>
		/// <param name="storage">
		/// The storage location to which options should be saved.
		/// </param>
		/// <param name="verifier">The Twitter verifier PIN.</param>
		/// <param name="accessToken">The Twitter access token.</param>
		/// <param name="accessTokenSecret">The Twitter access token secret.</param>
		/// <exception cref="System.ArgumentNullException">
		/// Thrown if <paramref name="storage" /> is <see langword="null" />.
		/// </exception>
		public static void SaveTwitterInfo(IDecoupledStorage storage, string verifier, string accessToken, string accessTokenSecret)
		{
			if (storage == null)
			{
				throw new ArgumentNullException("storage");
			}
			storage.WriteEncryptedString(SectionId, TwitterOAuthPinKey, verifier);
			storage.WriteEncryptedString(SectionId, TwitterOAuthTokenKey, accessToken);
			storage.WriteEncryptedString(SectionId, TwitterOAuthTokenSecretKey, accessTokenSecret);
			storage.Update();
		}

		/// <summary>
		/// Validates the selected options.
		/// </summary>
		/// <returns>
		/// A UI-displayable error message if the options aren't valid, or <see langword="null" />
		/// if the options are valid.
		/// </returns>
		public string Validate()
		{
			// Validate the options.
			string message = null;
			if (String.IsNullOrEmpty(this.CodePasteUsername) || String.IsNullOrEmpty(this.CodePastePassword))
			{
				message = Properties.Resources.Options_CodePasteNotConfigured;
			}
			else if (String.IsNullOrEmpty(this.TwitterOAuthPin) || String.IsNullOrEmpty(this.TwitterOAuthToken) || String.IsNullOrEmpty(this.TwitterOAuthTokenSecret))
			{
				message = Properties.Resources.Options_TwitterNotConfigured;
			}
			return message;
		}
	}
}
