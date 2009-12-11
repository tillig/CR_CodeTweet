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
			options.TwitterOAuthPin = "";
			string actual = options.Validate();
			Assert.IsNotNull(actual, "A validation message should have been returned.");
		}

		[Test(Description = "Tests validation of the Twitter OAuth PIN.")]
		public void Options_Validate_TwitterPinNull()
		{
			Options options = this.CreateValidOptions();
			options.TwitterOAuthPin = null;
			string actual = options.Validate();
			Assert.IsNotNull(actual, "A validation message should have been returned.");
		}

		[Test(Description = "Tests validation of the Twitter OAuth token.")]
		public void Options_Validate_TwitterTokenEmpty()
		{
			Options options = this.CreateValidOptions();
			options.TwitterOAuthToken = "";
			string actual = options.Validate();
			Assert.IsNotNull(actual, "A validation message should have been returned.");
		}

		[Test(Description = "Tests validation of the Twitter OAuth token.")]
		public void Options_Validate_TwitterTokenNull()
		{
			Options options = this.CreateValidOptions();
			options.TwitterOAuthToken = null;
			string actual = options.Validate();
			Assert.IsNotNull(actual, "A validation message should have been returned.");
		}

		[Test(Description = "Tests validation of the Twitter OAuth token secret.")]
		public void Options_Validate_TwitterTokenSecretEmpty()
		{
			Options options = this.CreateValidOptions();
			options.TwitterOAuthTokenSecret = "";
			string actual = options.Validate();
			Assert.IsNotNull(actual, "A validation message should have been returned.");
		}

		[Test(Description = "Tests validation of the Twitter OAuth token secret.")]
		public void Options_Validate_TwitterTokenSecretNull()
		{
			Options options = this.CreateValidOptions();
			options.TwitterOAuthTokenSecret = null;
			string actual = options.Validate();
			Assert.IsNotNull(actual, "A validation message should have been returned.");
		}

		private Options CreateValidOptions()
		{
			Options options = new Options()
			{
				CodePastePassword = "password",
				CodePasteUsername = "username",
				TwitterOAuthPin = "pin",
				TwitterOAuthToken = "token",
				TwitterOAuthTokenSecret = "tokensecret"
			};
			return options;
		}
	}
}
