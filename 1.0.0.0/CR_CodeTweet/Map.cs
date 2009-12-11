using System;
using System.Collections.Generic;

namespace CR_CodeTweet
{
	/// <summary>
	/// Contains mappings from DXCore values to corresponding values in other services.
	/// </summary>
	public static class Map
	{
		/// <summary>
		/// Indicates the option for no syntax highlighting.
		/// </summary>
		public const string CodePasteSyntaxHighlighterNone = "NoFormat";

		/// <summary>
		/// Map of DXCore language IDs to CodePaste.NET syntax highlighters.
		/// </summary>
		public static Dictionary<String, String> LanguageIdToSyntaxHighlighter = new Dictionary<string, string>()
		{
			// Maps DXCore LanguageID to CodePaste.NET syntax highlighter name.
			{"Basic", "VB.NET"},
			{ "C/C++", "C++"},
			{"CSharp", "C#"},
			{"CSS", "CSS"},
			{"F#", CodePasteSyntaxHighlighterNone},
			{"HTML", "HTML"},
			{"HTML/XML", "HTML"},
			{"JavaScript", "JavaScript"},
			{"JScript", "JavaScript"},
			{"Plain Text", CodePasteSyntaxHighlighterNone},
			{"T-SQL90", "SQL"},
			{"Visual JSharp", "Java"},
			{"XAML", "XML"},
			{"XML", "XML"}
		};
		// CodePaste.NET Synatax Highlighter selections:
		//<option value="NoFormat">No code formatting</option>
		//<option value="C#">C#</option>
		//<option value="VB.NET">Visual Basic .NET</option>
		//<option value="HTML">HTML, ASP.NET, JavaScript</option>
		//<option value="JavaScript">JavaScript</option>
		//<option value="CSS">CSS</option>
		//<option value="XML">XML</option>
		//<option value="SQL">SQL and TSQL</option>
		//<option value="PowerShell">Power Shell (Monad)</option>
		//<option value="FoxPro">FoxPro</option>
		//<option value="Java">Java</option>
		//<option value="C++">C++</option> //C/C++
	}
}
