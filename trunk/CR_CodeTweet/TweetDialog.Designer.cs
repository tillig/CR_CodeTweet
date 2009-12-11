namespace CR_CodeTweet
{
	partial class TweetDialog
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

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
			this.label1 = new System.Windows.Forms.Label();
			this.snippetLink = new System.Windows.Forms.LinkLabel();
			this.label2 = new System.Windows.Forms.Label();
			this.tweetText = new System.Windows.Forms.TextBox();
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.charsRemaining = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(71, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Snippet URL:";
			// 
			// snippetLink
			// 
			this.snippetLink.AutoSize = true;
			this.snippetLink.Location = new System.Drawing.Point(12, 22);
			this.snippetLink.Name = "snippetLink";
			this.snippetLink.Size = new System.Drawing.Size(106, 13);
			this.snippetLink.TabIndex = 1;
			this.snippetLink.TabStop = true;
			this.snippetLink.Text = "http://codepaste.net";
			this.snippetLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.snippetLink_LinkClicked);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 46);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(255, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Tweet (snippet URL will be automatically appended):";
			// 
			// tweetText
			// 
			this.tweetText.Location = new System.Drawing.Point(15, 63);
			this.tweetText.MaxLength = 140;
			this.tweetText.Multiline = true;
			this.tweetText.Name = "tweetText";
			this.tweetText.Size = new System.Drawing.Size(265, 60);
			this.tweetText.TabIndex = 3;
			this.tweetText.TextChanged += new System.EventHandler(this.tweetText_TextChanged);
			// 
			// okButton
			// 
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okButton.Enabled = false;
			this.okButton.Location = new System.Drawing.Point(124, 142);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 6;
			this.okButton.Text = "Tweet It!";
			this.okButton.UseVisualStyleBackColor = true;
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(205, 142);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 7;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(12, 126);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(109, 13);
			this.label4.TabIndex = 4;
			this.label4.Text = "Characters remaining:";
			// 
			// charsRemaining
			// 
			this.charsRemaining.AutoSize = true;
			this.charsRemaining.Location = new System.Drawing.Point(127, 126);
			this.charsRemaining.Name = "charsRemaining";
			this.charsRemaining.Size = new System.Drawing.Size(25, 13);
			this.charsRemaining.TabIndex = 5;
			this.charsRemaining.Text = "140";
			// 
			// TweetDialog
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(292, 173);
			this.Controls.Add(this.charsRemaining);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.tweetText);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.snippetLink);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "TweetDialog";
			this.Text = "And Now For The Tweet!";
			this.Shown += new System.EventHandler(this.TweetDialog_Shown);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.LinkLabel snippetLink;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox tweetText;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label charsRemaining;
	}
}