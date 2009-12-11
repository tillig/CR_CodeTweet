namespace CR_CodeTweet
{
	partial class SnippetDialog
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
			this.label2 = new System.Windows.Forms.Label();
			this.commentText = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.tagText = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.titleText = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.cancelButton = new System.Windows.Forms.Button();
			this.okButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(13, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(255, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Tell me about your snippet. (All of this is OPTIONAL.)";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(13, 55);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(54, 43);
			this.label2.TabIndex = 3;
			this.label2.Text = "Comment:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// commentText
			// 
			this.commentText.Location = new System.Drawing.Point(73, 55);
			this.commentText.Multiline = true;
			this.commentText.Name = "commentText";
			this.commentText.Size = new System.Drawing.Size(207, 43);
			this.commentText.TabIndex = 4;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(13, 104);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(54, 20);
			this.label3.TabIndex = 5;
			this.label3.Text = "Tags:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// tagText
			// 
			this.tagText.Location = new System.Drawing.Point(73, 104);
			this.tagText.Name = "tagText";
			this.tagText.Size = new System.Drawing.Size(207, 20);
			this.tagText.TabIndex = 6;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(73, 127);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(140, 13);
			this.label4.TabIndex = 7;
			this.label4.Text = "Separate tags with commas.";
			// 
			// titleText
			// 
			this.titleText.Location = new System.Drawing.Point(73, 29);
			this.titleText.Name = "titleText";
			this.titleText.Size = new System.Drawing.Size(207, 20);
			this.titleText.TabIndex = 2;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(13, 29);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(54, 20);
			this.label5.TabIndex = 1;
			this.label5.Text = "Title:";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(205, 143);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 9;
			this.cancelButton.Text = "Cancel";
			this.cancelButton.UseVisualStyleBackColor = true;
			// 
			// okButton
			// 
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okButton.Location = new System.Drawing.Point(124, 143);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 8;
			this.okButton.Text = "Post It!";
			this.okButton.UseVisualStyleBackColor = true;
			// 
			// SnippetDialog
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(292, 175);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.titleText);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.tagText);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.commentText);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SnippetDialog";
			this.Text = "Additional Snippet Info";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox commentText;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox tagText;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox titleText;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button okButton;
	}
}