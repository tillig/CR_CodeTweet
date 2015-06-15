#CR_CodeTweet

Sometimes you have a snippet of code in Visual Studio that you'd like to share over [Twitter](http://twitter.com). You used to have to post the code snippet somewhere (or take a screen shot), then manually tweet the link to that snippet.

Not anymore!

CR_CodeTweet is a plugin for [DXCore](http://www.devexpress.com/Products/Visual_Studio_Add-in/DXCore/) that adds a context menu and hotkey that allows you to select code in Visual Studio and automatically post it to [CodePaste.NET](http://codepaste.net), then tweet a link to that posted snippet - all without leaving Visual Studio.

##License

Copyright 2009 CR_CodeTweet Contributors

Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with the License. You may obtain a copy of the License at

[http://www.apache.org/licenses/LICENSE-2.0](http://www.apache.org/licenses/LICENSE-2.0)

Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the specific language governing permissions and limitations under the License.

##Requirements

This product requires **CodeRush for Visual Studio .NET 14.2 or later.**

CodeRush is a Visual Studio extension from Developer Express, Inc.: https://www.devexpress.com/Products/CodeRush/

##Installation

1. [Install the add-in via the Visual Studio extension gallery.](https://visualstudiogallery.msdn.microsoft.com/668a65b5-2468-4afa-b78d-8c369850e2b2)
1. Start Visual Studio.
1. From the DevExpress menu, select "Options."
1. Expand the "Editor" folder in the list of options and select the "CodeTweet" option panel.
1. Click the "Authorize CodeTweet" button to run through the process of enabling the plugin to tweet on your behalf.
1. Fill in your username/password credentials for CodePaste.NET. (Unfortunately the CodePaste.NET web service does not support OpenID, so if you currently use OpenID on that site you will need to switch to username/password.)
1. Click "Apply" to save your options.
1. <strong>Optional:</strong> Bind a hotkey to the tweet functionality.
	1. Expand the "IDE" folder in the list of options and sleect the "Shortcuts" option panel.
	1. Add a new shortcut key and bind it to the "Tweet Selected Code" command. Something easy to remember like "Ctrl+Shift+Alt+T" (for "Tweet") is recommended.
1. Click OK to save your options and close the dialog.

##Usage

1. Open an instance of Visual Studio.
1. When working in a source file, select one or more characters and tweet the snippet by either:
	* Right-clicking and selecting "Tweet This!" from the context menu, OR
	* Using the hotkey you set up to tweet the selected code.
