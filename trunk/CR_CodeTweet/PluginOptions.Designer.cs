using System;
using DevExpress.CodeRush.Common;
using DevExpress.CodeRush.Core;

namespace CR_CodeTweet
{
	/// <summary>
	/// Options screen for configuring the CR_CodeTweet plugin.
	/// </summary>
	partial class PluginOptions
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="PluginOptions"/> class.
		/// </summary>
		public PluginOptions()
		{
			// Required for Windows.Forms Class Composition Designer support
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PluginOptions));
			this.aboutText = new System.Windows.Forms.Label();
			this.twitterGroupBox = new System.Windows.Forms.GroupBox();
			this.twitterCredentialsLabel = new System.Windows.Forms.Label();
			this.twitterAuthorizeButton = new System.Windows.Forms.Button();
			this.codepasteGroupBox = new System.Windows.Forms.GroupBox();
			this.codepasteCredentialsLabel = new System.Windows.Forms.Label();
			this.codepastePasswordLabel = new System.Windows.Forms.Label();
			this.codepasteUsernameText = new System.Windows.Forms.TextBox();
			this.codepastePasswordText = new System.Windows.Forms.TextBox();
			this.codepasteUsernameLabel = new System.Windows.Forms.Label();
			this.twitterLink = new System.Windows.Forms.LinkLabel();
			this.codepasteLink = new System.Windows.Forms.LinkLabel();
			this.logo = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.versionLabel = new System.Windows.Forms.Label();
			this.twitterGroupBox.SuspendLayout();
			this.codepasteGroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.logo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			this.SuspendLayout();
			// 
			// aboutText
			// 
			this.aboutText.Location = new System.Drawing.Point(302, 296);
			this.aboutText.Name = "aboutText";
			this.aboutText.Size = new System.Drawing.Size(225, 69);
			this.aboutText.TabIndex = 9;
			this.aboutText.Text = "CodeTweet allows you to easily send code from your editor to CodePaste.NET and th" +
				"en tweet a link to it. You will need to have an account on both Twitter and Code" +
				"Paste.NET already.";
			// 
			// twitterGroupBox
			// 
			this.twitterGroupBox.Controls.Add(this.twitterCredentialsLabel);
			this.twitterGroupBox.Controls.Add(this.twitterAuthorizeButton);
			this.twitterGroupBox.Location = new System.Drawing.Point(4, 4);
			this.twitterGroupBox.Name = "twitterGroupBox";
			this.twitterGroupBox.Size = new System.Drawing.Size(275, 123);
			this.twitterGroupBox.TabIndex = 0;
			this.twitterGroupBox.TabStop = false;
			this.twitterGroupBox.Text = "Twitter Authorization";
			// 
			// twitterCredentialsLabel
			// 
			this.twitterCredentialsLabel.Location = new System.Drawing.Point(6, 20);
			this.twitterCredentialsLabel.Name = "twitterCredentialsLabel";
			this.twitterCredentialsLabel.Size = new System.Drawing.Size(262, 67);
			this.twitterCredentialsLabel.TabIndex = 0;
			this.twitterCredentialsLabel.Text = resources.GetString("twitterCredentialsLabel.Text");
			// 
			// twitterAuthorizeButton
			// 
			this.twitterAuthorizeButton.Location = new System.Drawing.Point(7, 90);
			this.twitterAuthorizeButton.Name = "twitterAuthorizeButton";
			this.twitterAuthorizeButton.Size = new System.Drawing.Size(262, 23);
			this.twitterAuthorizeButton.TabIndex = 3;
			this.twitterAuthorizeButton.Text = "Authorize CodeTweet";
			this.twitterAuthorizeButton.UseVisualStyleBackColor = true;
			this.twitterAuthorizeButton.Click += new System.EventHandler(this.TwitterAuthorizeButton_Click);
			// 
			// codepasteGroupBox
			// 
			this.codepasteGroupBox.Controls.Add(this.codepasteCredentialsLabel);
			this.codepasteGroupBox.Controls.Add(this.codepastePasswordLabel);
			this.codepasteGroupBox.Controls.Add(this.codepasteUsernameText);
			this.codepasteGroupBox.Controls.Add(this.codepastePasswordText);
			this.codepasteGroupBox.Controls.Add(this.codepasteUsernameLabel);
			this.codepasteGroupBox.Location = new System.Drawing.Point(4, 135);
			this.codepasteGroupBox.Name = "codepasteGroupBox";
			this.codepasteGroupBox.Size = new System.Drawing.Size(275, 145);
			this.codepasteGroupBox.TabIndex = 1;
			this.codepasteGroupBox.TabStop = false;
			this.codepasteGroupBox.Text = "CodePaste.NET Credentials";
			// 
			// codepasteCredentialsLabel
			// 
			this.codepasteCredentialsLabel.Location = new System.Drawing.Point(6, 16);
			this.codepasteCredentialsLabel.Name = "codepasteCredentialsLabel";
			this.codepasteCredentialsLabel.Size = new System.Drawing.Size(262, 70);
			this.codepasteCredentialsLabel.TabIndex = 0;
			this.codepasteCredentialsLabel.Text = "Enter the credentials you use to authenticate to CodePaste.NET. If you use OpenID" +
				" with CodePaste.NET, you will need to switch to username/password. CodePaste doe" +
				"sn\'t support OpenID auth for programs.";
			// 
			// codepastePasswordLabel
			// 
			this.codepastePasswordLabel.Location = new System.Drawing.Point(9, 116);
			this.codepastePasswordLabel.Name = "codepastePasswordLabel";
			this.codepastePasswordLabel.Size = new System.Drawing.Size(79, 20);
			this.codepastePasswordLabel.TabIndex = 5;
			this.codepastePasswordLabel.Text = "Password:";
			this.codepastePasswordLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// codepasteUsernameText
			// 
			this.codepasteUsernameText.Location = new System.Drawing.Point(94, 89);
			this.codepasteUsernameText.Name = "codepasteUsernameText";
			this.codepasteUsernameText.Size = new System.Drawing.Size(175, 20);
			this.codepasteUsernameText.TabIndex = 4;
			// 
			// codepastePasswordText
			// 
			this.codepastePasswordText.Location = new System.Drawing.Point(94, 116);
			this.codepastePasswordText.Name = "codepastePasswordText";
			this.codepastePasswordText.Size = new System.Drawing.Size(175, 20);
			this.codepastePasswordText.TabIndex = 6;
			this.codepastePasswordText.UseSystemPasswordChar = true;
			// 
			// codepasteUsernameLabel
			// 
			this.codepasteUsernameLabel.Location = new System.Drawing.Point(9, 89);
			this.codepasteUsernameLabel.Name = "codepasteUsernameLabel";
			this.codepasteUsernameLabel.Size = new System.Drawing.Size(79, 20);
			this.codepasteUsernameLabel.TabIndex = 3;
			this.codepasteUsernameLabel.Text = "Email Address:";
			this.codepasteUsernameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// twitterLink
			// 
			this.twitterLink.AutoSize = true;
			this.twitterLink.Location = new System.Drawing.Point(302, 365);
			this.twitterLink.Name = "twitterLink";
			this.twitterLink.Size = new System.Drawing.Size(61, 13);
			this.twitterLink.TabIndex = 4;
			this.twitterLink.TabStop = true;
			this.twitterLink.Tag = "";
			this.twitterLink.Text = "Visit Twitter";
			this.twitterLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Link_LinkClicked);
			// 
			// codepasteLink
			// 
			this.codepasteLink.AutoSize = true;
			this.codepasteLink.Location = new System.Drawing.Point(302, 381);
			this.codepasteLink.Name = "codepasteLink";
			this.codepasteLink.Size = new System.Drawing.Size(106, 13);
			this.codepasteLink.TabIndex = 5;
			this.codepasteLink.TabStop = true;
			this.codepasteLink.Tag = "";
			this.codepasteLink.Text = "Visit CodePaste.NET";
			this.codepasteLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Link_LinkClicked);
			// 
			// logo
			// 
			this.logo.Image = ((System.Drawing.Image)(resources.GetObject("logo.Image")));
			this.logo.Location = new System.Drawing.Point(302, 10);
			this.logo.Name = "logo";
			this.logo.Size = new System.Drawing.Size(225, 237);
			this.logo.TabIndex = 12;
			this.logo.TabStop = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(302, 254);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(95, 13);
			this.label1.TabIndex = 6;
			this.label1.Text = "CR_CodeTweet";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(302, 267);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(45, 13);
			this.label2.TabIndex = 7;
			this.label2.Text = "Version:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// versionLabel
			// 
			this.versionLabel.AutoSize = true;
			this.versionLabel.Location = new System.Drawing.Point(350, 267);
			this.versionLabel.Name = "versionLabel";
			this.versionLabel.Size = new System.Drawing.Size(0, 13);
			this.versionLabel.TabIndex = 8;
			// 
			// PluginOptions
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.Controls.Add(this.versionLabel);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.logo);
			this.Controls.Add(this.codepasteLink);
			this.Controls.Add(this.twitterLink);
			this.Controls.Add(this.codepasteGroupBox);
			this.Controls.Add(this.twitterGroupBox);
			this.Controls.Add(this.aboutText);
			this.Name = "PluginOptions";
			this.RestoreDefaults += new DevExpress.CodeRush.Core.OptionsPage.RestoreDefaultsEventHandler(this.PluginOptions_RestoreDefaults);
			this.CommitChanges += new DevExpress.CodeRush.Core.OptionsPage.CommitChangesEventHandler(this.PluginOptions_CommitChanges);
			this.twitterGroupBox.ResumeLayout(false);
			this.codepasteGroupBox.ResumeLayout(false);
			this.codepasteGroupBox.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.logo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		/// <summary>
		/// Gets a <see cref="DevExpress.CodeRush.Core.DecoupledStorage"/> instance for this options page.
		/// </summary>
		public static IDecoupledStorage Storage
		{
			get
			{
				return DevExpress.CodeRush.Core.CodeRush.Options.GetStorage(GetCategory(), GetPageName());
			}
		}

		/// <summary>
		/// Returns the category of this options page.
		/// </summary>
		public override string Category
		{
			get
			{
				return PluginOptions.GetCategory();
			}
		}

		/// <summary>
		/// Returns the page name of this options page.
		/// </summary>
		public override string PageName
		{
			get
			{
				return PluginOptions.GetPageName();
			}
		}

		/// <summary>
		/// Returns the full path (Category + PageName) of this options page.
		/// </summary>
		public static string FullPath
		{
			get
			{
				return GetCategory() + "\\" + GetPageName();
			}
		}

		/// <summary>
		/// Displays the DXCore options dialog and selects this page.
		/// </summary>
		public new static void Show()
		{
			DevExpress.CodeRush.Core.CodeRush.Command.Execute("Options", FullPath);
		}

		private System.Windows.Forms.Label aboutText;
		private System.Windows.Forms.GroupBox twitterGroupBox;
		private System.Windows.Forms.Label twitterCredentialsLabel;
		private System.Windows.Forms.Button twitterAuthorizeButton;
		private System.Windows.Forms.GroupBox codepasteGroupBox;
		private System.Windows.Forms.Label codepastePasswordLabel;
		private System.Windows.Forms.TextBox codepasteUsernameText;
		private System.Windows.Forms.TextBox codepastePasswordText;
		private System.Windows.Forms.Label codepasteUsernameLabel;
		private System.Windows.Forms.Label codepasteCredentialsLabel;
		private System.Windows.Forms.LinkLabel twitterLink;
		private System.Windows.Forms.LinkLabel codepasteLink;
		private System.Windows.Forms.PictureBox logo;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label versionLabel;
	}
}