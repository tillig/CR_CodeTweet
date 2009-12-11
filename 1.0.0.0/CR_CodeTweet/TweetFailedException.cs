using System;
using System.Runtime.Serialization;

namespace CR_CodeTweet
{
	/// <summary>
	/// Exception that indicates the overall operation of posting a code snippet
	/// and tweeting about it failed.
	/// </summary>
	[Serializable]
	public class TweetFailedException : Exception
	{
		private const string SerializationKeyMessage = "TFE_Message";
		private string _message = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="TweetFailedException"/> class.
		/// </summary>
		public TweetFailedException()
		{
			this._message = null;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TweetFailedException"/> class with a specified message.
		/// </summary>
		/// <param name="message">The exception message.</param>
		public TweetFailedException(string message)
			: base(message)
		{
			this._message = message;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TweetFailedException"/> class with a specified message and nested exception.
		/// </summary>
		/// <param name="message">The exception message.</param>
		/// <param name="innerException">The inner exception.</param>
		public TweetFailedException(string message, Exception innerException)
			: base(message, innerException)
		{
			this._message = message;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TweetFailedException"/> class via serialization.
		/// </summary>
		/// <param name="info">The info to deserialize.</param>
		/// <param name="context">The context in which to deserialize.</param>
		protected TweetFailedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this._message = info.GetString(SerializationKeyMessage);
		}


		/// <summary>
		/// Gets a message that describes the current exception.
		/// </summary>
		/// <value>
		/// The error message that explains the reason for the exception, or an empty string ("").
		/// </value>
		public override string Message
		{
			get
			{
				if (this._message == null)
				{
					return Properties.Resources.Exception_TweetFailedMessage;
				}
				return this._message;
			}
		}

		/// <summary>
		/// When overridden in a derived class, sets the <see cref="T:System.Runtime.Serialization.SerializationInfo"/> with information about the exception.
		/// </summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
		/// <exception cref="T:System.ArgumentNullException">
		/// The <paramref name="info"/> parameter is a null reference (Nothing in Visual Basic).
		/// </exception>
		/// <PermissionSet>
		/// <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*"/>
		/// <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter"/>
		/// </PermissionSet>
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue(SerializationKeyMessage, this._message, typeof(string));
		}
	}
}
