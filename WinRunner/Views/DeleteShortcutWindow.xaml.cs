using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using WinRunner.Models;
using WinRunner.Models.Shortcuts;

namespace WinRunner.Views {
	/// <summary>
	/// Interaction logic for DeleteRegistryAppWindow.xaml
	/// </summary>
	public partial class DeleteShortcutWindow:Window {
		public Shortcut Shortcut { get; set; }

		public DeleteShortcutWindow (Shortcut shortcut) {
			this.Shortcut = shortcut;

			InitializeComponent ();
		}

		private void DeleteClicked (object sender, RoutedEventArgs e) {
			this.DialogResult = true;
		}
	}
}
