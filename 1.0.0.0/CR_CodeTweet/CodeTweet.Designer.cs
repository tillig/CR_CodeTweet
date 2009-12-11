namespace CR_CodeTweet
{
	partial class CodeTweet
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Initializes a new instance of the <see cref="CodeTweet"/> class.
		/// </summary>
		public CodeTweet()
		{
			// Required for Windows.Forms Class Composition Designer support
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">
		/// <see langword="true" /> if managed resources should be disposed; otherwise, <see langword="false" />.
		/// </param>
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CodeTweet));
			this.codeTweetAction = new DevExpress.CodeRush.Core.Action(this.components);
			((System.ComponentModel.ISupportInitialize)(this.codeTweetAction)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			// 
			// codeTweetAction
			// 
			this.codeTweetAction.ActionName = "Tweet Selected Code";
			this.codeTweetAction.ButtonText = "Tweet Selected Code";
			this.codeTweetAction.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None;
			this.codeTweetAction.Description = "Sends the selected code to CodePaste.NET and then tweets a link to that code.";
			this.codeTweetAction.Image = ((System.Drawing.Bitmap)(resources.GetObject("codeTweetAction.Image")));
			this.codeTweetAction.ImageBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(254)))), ((int)(((byte)(0)))));
			this.codeTweetAction.Execute += new DevExpress.CodeRush.Core.CommandExecuteEventHandler(this.CodeTweetAction_Execute);
			this.codeTweetAction.QueryStatus += new DevExpress.CodeRush.Core.QueryStatusEventHandler(this.CodeTweetAction_QueryStatus);
			((System.ComponentModel.ISupportInitialize)(this.codeTweetAction)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();

		}

		#endregion

		private DevExpress.CodeRush.Core.Action codeTweetAction;
	}
}