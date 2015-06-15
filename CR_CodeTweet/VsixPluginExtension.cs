using System.ComponentModel.Composition;
using DevExpress.CodeRush.Common;

namespace CR_CodeTweet
{
	/// <summary>
	/// VSIX plugin extension class for CR_CodeTweet.
	/// </summary>
	[Export(typeof(IVsixPluginExtension))]
	public class VsixPluginExtension : IVsixPluginExtension
	{
	}
}
