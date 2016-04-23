using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using WinRunner.Models.Shortcuts;

namespace WinRunner.Views.EditShortcutWidgets {
	/// <summary>
	/// Interaction logic for PathAppWidget.xaml
	/// </summary>
	public partial class MSEdgeShortcutWidget:EditShortcutWidget {
		private MSEdgeShortcut shortcut;
		
		public MSEdgeShortcutWidget () {
			InitializeComponent ();
		}

		private void TestUrlClicked (object sender, RoutedEventArgs e) {
			this.shortcut = (MSEdgeShortcut) this.Shortcut;

			Process.Start (new ProcessStartInfo (this.shortcut.Url));
			e.Handled = true;
		}
	}
}
