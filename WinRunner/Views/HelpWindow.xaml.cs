using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WinRunner.Views {
	/// <summary>
	/// Interaction logic for HelpWindow.xaml
	/// </summary>
	public partial class HelpWindow:Window {
		private bool throwawayKeyUp = false;

		public HelpWindow () {
			InitializeComponent ();
		}

		private void NavigateRequested (object sender, RequestNavigateEventArgs e) {
			Process.Start (new ProcessStartInfo (e.Uri.AbsoluteUri));
			e.Handled = true;
		}

		private void KeyUpped (object sender, KeyEventArgs e) {
			if (throwawayKeyUp && e.Key == Key.F1 || e.Key == Key.Escape) {
				this.Close ();
			}

			throwawayKeyUp = true;
		}
	}
}
