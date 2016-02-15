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
using WinRunner.Models.Apps;

namespace WinRunner.Views {
	/// <summary>
	/// Interaction logic for EditRegistryAppWindow.xaml
	/// </summary>
	public partial class EditAppWindow:Window {
		public App App { get; set; }

		public EditAppWindow (App app) {
			this.App = app;
			this.App.RememberProperties ();
			
			InitializeComponent ();
		}

		private void SaveClicked (object sender, RoutedEventArgs e) {
			this.DialogResult = true;
		}

		private void WindowClosing (object sender, CancelEventArgs e) {
			if (this.DialogResult == true) {
				this.App.FlushToRegistry ();
			} else {
				this.App.RevertProperties ();
			}
		}
	}
}
