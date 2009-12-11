using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using CR_CodeTweet;
using NUnit.Framework;

namespace CR_CodeTweetTests
{
	[TestFixture]
	public class TweetFailedExceptionFixture
	{
		[Test(Description = "Ensures the message can be set to null when there's no inner exception.")]
		public void Message_CanBeNullWithoutInnerException()
		{
			TweetFailedException ex = new TweetFailedException(null);
			Assert.IsTrue(ex.Message.Length > 1, "The default message should not be empty.");
		}

		[Test(Description = "Ensures the message can be set to empty string when there's no inner exception.")]
		public void Message_CanBeEmptyStringWithoutInnerException()
		{
			TweetFailedException ex = new TweetFailedException("");
			Assert.AreEqual("", ex.Message, "The message should be able to be empty string.");
		}

		[Test(Description = "Ensures the message can be set to null when there's an inner exception.")]
		public void Message_CanBeNullWithInnerException()
		{
			TweetFailedException ex = new TweetFailedException(null, new Exception());
			Assert.IsTrue(ex.Message.Length > 1, "The default message should not be empty.");
		}

		[Test(Description = "Ensures the message can be set to empty string when there's an inner exception.")]
		public void Message_CanBeEmptyStringWithInnerException()
		{
			TweetFailedException ex = new TweetFailedException("", new Exception());
			Assert.AreEqual("", ex.Message, "The message should be able to be empty string.");
		}

		[Test(Description = "Verifies the default value of the message.")]
		public void Message_DefaultValue()
		{
			TweetFailedException ex = new TweetFailedException();
			Assert.IsTrue(ex.Message.Length > 1, "The default message should not be empty.");
		}

		[Test(Description = "Serializes and deserializes the exception to ensure the message is retained.")]
		public void Serializable_ReconsititutesMessage()
		{
			TweetFailedException ex = new TweetFailedException("Custom message.");
			TweetFailedException actual = null;
			using (MemoryStream stream = new MemoryStream())
			{
				BinaryFormatter formatter = new BinaryFormatter();
				formatter.Serialize(stream, ex);
				stream.Position = 0;
				actual = (TweetFailedException)formatter.Deserialize(stream);
			}
			Assert.AreEqual(ex.Message, actual.Message, "The message was not retained across serialization.");
		}
	}
}
