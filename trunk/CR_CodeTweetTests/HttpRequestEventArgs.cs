using System;
using System.Net;

namespace CR_CodeTweetTests
{
	/// <summary>
	/// Event arguments used for incoming web requests.
	/// </summary>
	public class HttpRequestEventArgs : EventArgs
	{
		public HttpListenerContext RequestContext { get; private set; }

		public HttpRequestEventArgs(HttpListenerContext requestContext)
		{
			this.RequestContext = requestContext;
		}
	}
}
