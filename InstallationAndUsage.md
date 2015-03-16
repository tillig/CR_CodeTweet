# Prerequisites #
To use the latest CR`_`CodeTweet, you must have...
  * Visual Studio 2008 or higher.
  * DXCore 9.2.9.0 (2009.2) or higher.
  * An account on [Twitter](http://twitter.com).
  * An account on [CodePaste.NET](http://codepaste.net)

# Installation #

  * Make sure all instances of Visual Studio are closed.
  * Copy the CR`_`CodeTweet.dll assembly into your DXCore plug-ins folder.  This is typically something like: C:\Documents and Settings\YOURUSERNAME\My Documents\DevExpress\IDE Tools\Community\PlugIns **OR** C:\Program Files\DevExpress 2009.2\IDETools\Community\PlugIns
  * Start Visual Studio.
  * From the DevExpress menu, select "Options."
  * Expand the "Editor" folder in the list of options and select the "FeCodeTweet" option panel.
  * Click the "Authorize CodeTweet" button to run through the process of enabling the plugin to tweet on your behalf.
  * Fill in your username/password credentials for CodePaste.NET. (Unfortunately the CodePaste.NET web service does not support OpenID, so if you currently use OpenID on that site you will need to switch to username/password.)
  * Click "Apply" to save your options.
  * **Optional:** Bind a hotkey to the tweet functionality.
    * Expand the "IDE" folder in the list of options and sleect the "Shortcuts" option panel.
    * Add a new shortcut key and bind it to the "Tweet Selected Code" command. Something easy to remember like "Ctrl+Shift+Alt+T" (for "Tweet") is recommended.
  * Click OK to save your options and close the dialog.

Here's what the options screen looks like (click to embiggen):

![![](http://cr-codetweet.googlecode.com/svn/site/screenshots/options-small.png)](http://cr-codetweet.googlecode.com/svn/site/screenshots/options.png)

# Usage #
While in Visual Studio, select some code in the editor that you'd like to post to Twitter.

Right-click on the selected code and select "Tweet This!" or use the hotkey you bound to the "Tweet This Code" action during installation. This will start the process of posting the code.

![http://cr-codetweet.googlecode.com/svn/site/screenshots/context-menu.png](http://cr-codetweet.googlecode.com/svn/site/screenshots/context-menu.png)

Fill in the optional information about your code snippet and post it.

![http://cr-codetweet.googlecode.com/svn/site/screenshots/snippet-info-dialog.png](http://cr-codetweet.googlecode.com/svn/site/screenshots/snippet-info-dialog.png)

Fill in the body of your tweet and send it off. The URL to the snippet on CodePaste.NET will be automatically appended.

![http://cr-codetweet.googlecode.com/svn/site/screenshots/tweet-info-dialog.png](http://cr-codetweet.googlecode.com/svn/site/screenshots/tweet-info-dialog.png)

Check Twitter and you'll see your snippet!

![http://cr-codetweet.googlecode.com/svn/site/screenshots/tweet-rendered.png](http://cr-codetweet.googlecode.com/svn/site/screenshots/tweet-rendered.png)