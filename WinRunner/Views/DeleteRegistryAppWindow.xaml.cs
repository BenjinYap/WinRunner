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

namespace WinRunner.Views {
	/// <summary>
	/// Interaction logic for DeleteRegistryAppWindow.xaml
	/// </summary>
	public partial class DeleteRegistryAppWindow:Window {
		public App App { get; set; }

		public DeleteRegistryAppWindow (App app) {
			this.App = app;

			InitializeComponent ();
		}

		private void DeleteClicked (object sender, RoutedEventArgs e) {
			this.DialogResult = true;
		}
	}
}
