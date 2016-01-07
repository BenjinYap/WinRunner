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
using WinRunner.Models;
using WinRunner.Views;

namespace WinRunner {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow:Window {
		public RegistryAppList AppList { get; set; }

		public MainWindow () {
			this.AppList = new RegistryAppList ();

			InitializeComponent ();
			
			this.AppList.Add (new RegistryApp ("awd", @"C:\Users\Benjin\Desktop\awd.bat"));
			this.AppList.Add (new RegistryApp ("goodbye", @"C:\Users\Benjin\Desktop\audacity-win-2.1.1.exe"));

			OpenAppWindow (this.AppList [0]);
		}

		private void NewAppClicked (object sender, RoutedEventArgs e) {
			OpenAppWindow (null);
		}

		private void OpenAppWindow (RegistryApp app) {
			EditRegistryAppWindow window;

			if (app == null) {
				window = new EditRegistryAppWindow (new RegistryApp ());
			} else {
				window = new EditRegistryAppWindow (app);
			}
			
			bool? result = window.ShowDialog ();
		}

		private void EditAppClicked (object sender, RoutedEventArgs e) {
			OpenAppWindow ((RegistryApp) ((Button) sender).DataContext);
		}
	}
}
