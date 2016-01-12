

using System;
using System.Windows;
namespace WinRunner.Views {
	public class CenteredDialogWindow:Window {

		protected override void OnContentRendered (EventArgs e) {
			base.OnContentRendered (e);

			this.Left = this.Owner.Left + this.Owner.Width / 2 - this.Width / 2;
			this.Top = this.Owner.Top + this.Owner.Height / 2 - this.Height / 2;
		}
	}
}
