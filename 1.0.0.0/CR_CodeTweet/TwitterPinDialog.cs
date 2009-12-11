using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CR_CodeTweet
{
	/// <summary>
	/// Dialog box for getting the Twitter PIN from the user before obtaining an access token.
	/// </summary>
	public partial class TwitterPinDialog : Form
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="TwitterPinDialog"/> class.
		/// </summary>
		public TwitterPinDialog()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Enables/disables the OK button based on the input.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private void pinText_TextChanged(object sender, EventArgs e)
		{
			okButton.Enabled =
				this.pinText.Text.Length == this.pinText.MaxLength &&
				Regex.IsMatch(this.pinText.Text, @"^\d+$");
		}

		/// <summary>
		/// Gets the user's Twitter PIN.
		/// </summary>
		/// <value>
		/// A <see cref="System.String"/> with the data the user entered into
		/// the dialog.
		/// </value>
		public string TwitterPin
		{
			get
			{
				return this.pinText.Text;
			}
		}
	}
}
