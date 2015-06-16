using System;
using CR_CodeTweet;
using NUnit.Framework;

namespace CR_CodeTweetTests
{
	[TestFixture]
	public class OptionsFixture
	{
		[Test(Description = "Tests validation of CodePaste.NET password.")]
		public void Options_Validate_CodepastePasswordEmpty()
		{
			Options options = this.CreateValidOptions();
			options.CodePastePassword = "";
			string actual = options.Validate();
			Assert.IsNotNull(actual, "A validation message should have been returned.");
		}

		[Test(Description = "Tests validation of CodePaste.NET password.")]
		public void Options_Validate_CodepastePasswordNull()
		{
			Options options = this.CreateValidOptions();
			options.CodePastePassword = null;
			string actual = options.Validate();
			Assert.IsNotNull(actual, "A validation message should have been returned.");
		}

		[Test(Description = "Tests validation of CodePaste.NET username.")]
		public void Options_Validate_CodepasteUsernameEmpty()
		{
			Options options = this.CreateValidOptions();
			options.CodePasteUsername = "";
			string actual = options.Validate();
			Assert.IsNotNull(actual, "A validation message should have been returned.");
		}

		[Test(Description = "Tests validation of CodePaste.NET username.")]
		public void Options_Validate_CodepasteUsernameNull()
		{
			Options options = this.CreateValidOptions();
			options.CodePasteUsername = null;
			string actual = options.Validate();
			Assert.IsNotNull(actual, "A validation message should have been returned.");
		}

		[Test(Description = "Tests validation of the Twitter OAuth PIN.")]
		public void Options_Validate_TwitterPinEmpty()
		{
			Options options = this.CreateValidOptions();
			options.TwitterUserInfo.Verifier = "";
			string actual = options.Validate();
			Assert.IsNotNull(actual, "A validation message should have been returned.");
		}

		[Test(Description = "Tests validation of the Twitter OAuth PIN.")]
		public void Options_Validate_TwitterPinNull()
		{
			Options options = this.CreateValidOptions();
			options.TwitterUserInfo.Verifier = null;
			string actual = options.Validate();
			Assert.IsNotNull(actual, "A validation message should have been returned.");
		}

		[Test(Description = "Tests validation of the Twitter OAuth token.")]
		public void Options_Validate_TwitterTokenEmpty()
		{
			Options options = this.CreateValidOptions();
			options.TwitterUserInfo.AccessToken = "";
			string actual = options.Validate();
			Assert.IsNotNull(actual, "A validation message should have been returned.");
		}

		[Test(Description = "Tests validation of the Twitter OAuth token.")]
		public void Options_Validate_TwitterTokenNull()
		{
			Options options = this.CreateValidOptions();
			options.TwitterUserInfo.AccessToken = null;
			string actual = options.Validate();
			Assert.IsNotNull(actual, "A validation message should have been returned.");
		}

		[Test(Description = "Tests validation of the Twitter OAuth token secret.")]
		public void Options_Validate_TwitterTokenSecretEmpty()
		{
			Options options = this.CreateValidOptions();
			options.TwitterUserInfo.AccessTokenSecret = "";
			string actual = options.Validate();
			Assert.IsNotNull(actual, "A validation message should have been returned.");
		}

		[Test(Description = "Tests validation of the Twitter OAuth token secret.")]
		public void Options_Validate_TwitterTokenSecretNull()
		{
			Options options = this.CreateValidOptions();
			options.TwitterUserInfo.AccessTokenSecret = null;
			string actual = options.Validate();
			Assert.IsNotNull(actual, "A validation message should have been returned.");
		}

		private Options CreateValidOptions()
		{
			return new Options()
			{
				CodePastePassword = "password",
				CodePasteUsername = "username",
				TwitterUserInfo = new TwitterUserInfo
				{
					AccessToken = "token",
					AccessTokenSecret = "tokensecret",
					Verifier = "pin"
				}
			};
		}
	}
}
