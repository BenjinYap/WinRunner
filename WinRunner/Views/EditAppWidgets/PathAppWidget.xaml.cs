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
using WinRunner.Models.Apps;

namespace WinRunner.Views.EditAppWidgets {
	/// <summary>
	/// Interaction logic for PathAppWidget.xaml
	/// </summary>
	public partial class PathAppWidget:EditAppWidget {
		private PathApp app;
		
		public PathAppWidget () {
			InitializeComponent ();
		}

		private void ChooseFileClicked (object sender, RoutedEventArgs e) {
			this.app = (PathApp) this.App;

			OpenFileDialog dialog = new OpenFileDialog ();
			
			if (File.Exists (this.app.Path)) {
				dialog.InitialDirectory = System.IO.Path.GetDirectoryName (this.app.Path);
			}

			dialog.FileName = this.app.Path;
			dialog.CheckFileExists = true;
			dialog.CheckPathExists = true;
			bool? result = dialog.ShowDialog ();
			
			if (result.HasValue && result.Value) {
				this.app.Path = dialog.FileName;
			}
		}
	}
}
