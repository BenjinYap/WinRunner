using Microsoft.Win32;
using Ookii.Dialogs.Wpf;
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
	/// Interaction logic for FolderShortcutWidget.xaml
	/// </summary>
	public partial class FolderShortcutWidget:EditShortcutWidget {
		private FolderShortcut shortcut;
		
		public FolderShortcutWidget () {
			InitializeComponent ();
		}

		private void ChooseFileClicked (object sender, RoutedEventArgs e) {
			this.shortcut = (FolderShortcut) this.Shortcut;

			//use fancy Ookii vista browser dialog
			VistaFolderBrowserDialog dialog = new VistaFolderBrowserDialog ();

			//set the starting path
			dialog.SelectedPath = this.shortcut.Path;
			
			//show the dialog
			bool? result = dialog.ShowDialog ();
			
			//if a folder was selected
			if (result.HasValue && result.Value) {
				//set the shortcut's path to the selected folder path
				this.shortcut.Path = dialog.SelectedPath;
			}
		}
	}
}
