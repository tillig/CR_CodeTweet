using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using CR_CodeTweet.Properties;
using DevExpress.CodeRush.Common;

[assembly: DXCoreAssembly(DXCoreAssemblyType.PlugIn, ProductConstants.PlugInName, PlugInLoadType.Demand, LoadAbilityType.LoadEnabled)]

[assembly: ComVisible(false)]

[assembly: AssemblyTitle(ProductConstants.PlugInName)]
[assembly: AssemblyDescription("DXCore plugin that provides the ability to send a code snippet to CodePaste.NET and post a link to the snippet on Twitter.")]
