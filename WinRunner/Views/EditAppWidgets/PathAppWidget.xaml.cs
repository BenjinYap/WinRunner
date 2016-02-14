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

namespace WinRunner.Views.EditAppWidgets {
	/// <summary>
	/// Interaction logic for PathAppWidget.xaml
	/// </summary>
	public partial class PathAppWidget:EditAppWidget {
		public PathAppWidget () {
			InitializeComponent ();

			
		}

		private void ChooseFileClicked (object sender, RoutedEventArgs e) {
			OpenFileDialog dialog = new OpenFileDialog ();
			
			if (File.Exists (this.App.Path)) {
				dialog.InitialDirectory = System.IO.Path.GetDirectoryName (this.App.Path);
			}

			dialog.FileName = this.App.Path;
			dialog.CheckFileExists = true;
			dialog.CheckPathExists = true;
			bool? result = dialog.ShowDialog ();
			
			if (result.HasValue && result.Value) {
				this.App.Path = dialog.FileName;
			}
		}
	}
}
