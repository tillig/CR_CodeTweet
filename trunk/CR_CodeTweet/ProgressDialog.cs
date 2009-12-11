using System;
using System.Windows.Forms;

namespace CR_CodeTweet
{
	/// <summary>
	/// Dialog that shows progress for operations.
	/// </summary>
	public partial class ProgressDialog : Form
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ProgressDialog"/> class.
		/// </summary>
		public ProgressDialog()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Gets or sets the status message.
		/// </summary>
		/// <value>
		/// A <see cref="System.String"/> containing the status message that the
		/// dialog is currently showing.
		/// </value>
		public string StatusMessage
		{
			get
			{
				return this.statusMessage.Text;
			}
			set
			{
				this.statusMessage.Text = value;
			}
		}
	}
}
