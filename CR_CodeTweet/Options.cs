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
		/// Gets or sets the Twitter user information.
		/// </summary>
		/// <value>
		/// A <see cref="TwitterUserInfo"/> with the user's
		/// access token information.
		/// </value>
		public TwitterUserInfo TwitterUserInfo { get; set; }

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
			options.TwitterUserInfo = new TwitterUserInfo
			{
				AccessToken = storage.ReadEncryptedString(SectionId, TwitterOAuthTokenKey, ""),
				AccessTokenSecret = storage.ReadEncryptedString(SectionId, TwitterOAuthTokenSecretKey, ""),
				Verifier = storage.ReadEncryptedString(SectionId, TwitterOAuthPinKey, "")
			};

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
			SaveTwitterInfo(storage, options.TwitterUserInfo);
			storage.Update();
		}

		/// <summary>
		/// Saves the Twitter authorization info.
		/// </summary>
		/// <param name="storage">
		/// The storage location to which options should be saved.
		/// </param>
		/// <param name="userInfo">
		/// The Twitter user info with the verifier PIN, access token, and access token secret.
		/// </param>
		/// <exception cref="System.ArgumentNullException">
		/// Thrown if <paramref name="storage" /> or <paramref name="userInfo" /> is <see langword="null" />.
		/// </exception>
		public static void SaveTwitterInfo(IDecoupledStorage storage, TwitterUserInfo userInfo)
		{
			if (storage == null)
			{
				throw new ArgumentNullException("storage");
			}

			if (userInfo == null)
			{
				throw new ArgumentNullException("userInfo");
			}

			storage.WriteEncryptedString(SectionId, TwitterOAuthPinKey, userInfo.Verifier);
			storage.WriteEncryptedString(SectionId, TwitterOAuthTokenKey, userInfo.AccessToken);
			storage.WriteEncryptedString(SectionId, TwitterOAuthTokenSecretKey, userInfo.AccessTokenSecret);
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
			else if (String.IsNullOrEmpty(this.TwitterUserInfo.Verifier) || String.IsNullOrEmpty(this.TwitterUserInfo.AccessToken) || String.IsNullOrEmpty(this.TwitterUserInfo.AccessTokenSecret))
			{
				message = Properties.Resources.Options_TwitterNotConfigured;
			}
			return message;
		}
	}
}
