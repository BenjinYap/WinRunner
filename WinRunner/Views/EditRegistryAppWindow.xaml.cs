using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WinRunner.Models;

namespace WinRunner.Views {
	/// <summary>
	/// Interaction logic for EditRegistryAppWindow.xaml
	/// </summary>
	public partial class EditRegistryAppWindow:Window {
		public RegistryApp App { get; set; }

		private string oldName;
		private string oldPath;

		public EditRegistryAppWindow (RegistryApp app) {
			this.App = app;

			this.oldName = app.Name;
			this.oldPath = app.Path;

			InitializeComponent ();
		}

		private void ChooseFileClicked (object sender, RoutedEventArgs e) {
			OpenFileDialog dialog = new OpenFileDialog ();
			dialog.InitialDirectory = System.IO.Path.GetDirectoryName (this.App.Path);
			dialog.FileName = this.App.Path;
			dialog.CheckFileExists = true;
			dialog.CheckPathExists = true;
			bool? result = dialog.ShowDialog ();
			
			if (result.HasValue && result.Value) {
				this.App.Path = dialog.FileName;
			}
		}

		private void SaveClicked (object sender, RoutedEventArgs e) {
			this.DialogResult = true;
		}

		private void WindowClosing (object sender, CancelEventArgs e) {
			if (this.DialogResult == true) {

			} else {
				this.App.Name = this.oldName;
				this.App.Path = this.oldPath;
			}
		}
	}
}
