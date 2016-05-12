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
	public partial class WebPageShortcutWidget:EditShortcutWidget {
		private WebPageShortcut shortcut;
		
		public WebPageShortcutWidget () {
			InitializeComponent ();
		}

		private void ChooseFileClicked (object sender, RoutedEventArgs e) {
			this.shortcut = (WebPageShortcut) this.Shortcut;

			OpenFileDialog dialog = new OpenFileDialog ();
			
			if (File.Exists (this.shortcut.Browser)) {
				dialog.InitialDirectory = System.IO.Path.GetDirectoryName (this.shortcut.Browser);
			}

			dialog.FileName = this.shortcut.Browser;
			dialog.CheckFileExists = true;
			dialog.CheckPathExists = true;
			bool? result = dialog.ShowDialog ();
			
			if (result.HasValue && result.Value) {
				this.shortcut.Browser = dialog.FileName;
			}
		}

		private void TestUrlClicked (object sender, RoutedEventArgs e) {
			this.shortcut = (WebPageShortcut) this.Shortcut;

			Process.Start (new ProcessStartInfo (this.shortcut.Url));
			e.Handled = true;
		}
	}
}
