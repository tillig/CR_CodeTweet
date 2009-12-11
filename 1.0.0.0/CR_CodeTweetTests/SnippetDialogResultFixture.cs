using System;
using CR_CodeTweet;
using NUnit.Framework;

namespace CR_CodeTweetTests
{
	[TestFixture]
	public class SnippetDialogResultFixture
	{
		[Test(Description = "Checks the default value of the comment.")]
		public void Comment_DefaultValue()
		{
			SnippetDialogResult result = new SnippetDialogResult();
			Assert.IsNull(result.Comment, "The comment should default to null.");
		}

		[Test(Description = "Sets the comment to an empty value.")]
		public void Comment_Empty()
		{
			SnippetDialogResult result = new SnippetDialogResult();
			result.Comment = "";
			Assert.IsNull(result.Comment, "The comment should switch to null if set to empty.");
		}

		[Test(Description = "Sets the comment to a null value.")]
		public void Comment_Null()
		{
			SnippetDialogResult result = new SnippetDialogResult();
			result.Comment = null;
			Assert.IsNull(result.Comment, "The comment should be able to be set to null.");
		}

		[Test(Description = "Sets the comment to a value surrounded by whitespace.")]
		public void Comment_Trimmed()
		{
			SnippetDialogResult result = new SnippetDialogResult();
			result.Comment = "  \t\t  \n\r  ";
			Assert.IsNull(result.Comment, "The comment should switch to null if set to all whitespace.");
		}

		[Test(Description = "Sets the comment to an all-whitespace value.")]
		public void Comment_Whitespace()
		{
			SnippetDialogResult result = new SnippetDialogResult();
			result.Comment = "  \t\t  \n\r  ";
			Assert.IsNull(result.Comment, "The comment should switch to null if set to all whitespace.");
		}

		[Test(Description = "Checks the default value of the tags.")]
		public void Tags_DefaultValue()
		{
			SnippetDialogResult result = new SnippetDialogResult();
			Assert.IsNull(result.Tags, "The tags should default to null.");
		}

		[Test(Description = "Sets the tags to an empty value.")]
		public void Tags_Empty()
		{
			SnippetDialogResult result = new SnippetDialogResult();
			result.Tags = "";
			Assert.IsNull(result.Tags, "The tags should switch to null if set to empty.");
		}

		[Test(Description = "Sets the tags to a null value.")]
		public void Tags_Null()
		{
			SnippetDialogResult result = new SnippetDialogResult();
			result.Tags = null;
			Assert.IsNull(result.Tags, "The tags should be able to be set to null.");
		}

		[Test(Description = "Sets the tags to a value surrounded by whitespace.")]
		public void Tags_Trimmed()
		{
			SnippetDialogResult result = new SnippetDialogResult();
			result.Tags = "  \t\t  \n\r  ";
			Assert.IsNull(result.Tags, "The tags should switch to null if set to all whitespace.");
		}

		[Test(Description = "Sets the tags to an all-whitespace value.")]
		public void Tags_Whitespace()
		{
			SnippetDialogResult result = new SnippetDialogResult();
			result.Tags = "  \t\t  \n\r  ";
			Assert.IsNull(result.Tags, "The tags should switch to null if set to all whitespace.");
		}

		[Test(Description = "Checks the default value of the title.")]
		public void Title_DefaultValue()
		{
			SnippetDialogResult result = new SnippetDialogResult();
			Assert.IsNull(result.Title, "The title should default to null.");
		}

		[Test(Description = "Sets the title to an empty value.")]
		public void Title_Empty()
		{
			SnippetDialogResult result = new SnippetDialogResult();
			result.Title = "";
			Assert.IsNull(result.Title, "The title should switch to null if set to empty.");
		}

		[Test(Description = "Sets the title to a null value.")]
		public void Title_Null()
		{
			SnippetDialogResult result = new SnippetDialogResult();
			result.Title = null;
			Assert.IsNull(result.Title, "The title should be able to be set to null.");
		}

		[Test(Description = "Sets the title to a value surrounded by whitespace.")]
		public void Title_Trimmed()
		{
			SnippetDialogResult result = new SnippetDialogResult();
			result.Title = "  \t\t  \n\r  ";
			Assert.IsNull(result.Title, "The title should switch to null if set to all whitespace.");
		}

		[Test(Description = "Sets the title to an all-whitespace value.")]
		public void Title_Whitespace()
		{
			SnippetDialogResult result = new SnippetDialogResult();
			result.Title = "  \t\t  \n\r  ";
			Assert.IsNull(result.Title, "The title should switch to null if set to all whitespace.");
		}
	}
}
