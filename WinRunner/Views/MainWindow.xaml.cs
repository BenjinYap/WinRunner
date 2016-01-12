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
using System.Windows.Threading;
using WinRunner.Models;
using WinRunner.Views;

namespace WinRunner {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow:Window {
		public RegistryAppList AppList { get; set; }

		private UserPreferences preferences = new UserPreferences ();
		private DispatcherTimer timer = new DispatcherTimer { Interval = new TimeSpan (0, 0, 0, 0, 500) };

		public MainWindow () {
			this.AppList = new RegistryAppList ();
			
			InitializeComponent ();
			
			this.AppList.LoadAppsFromRegistry ();

			this.timer.Tick += TimerTicked;
			this.timer.IsEnabled = false;

			this.LoadPreferences ();
		}

		private void NewAppClicked (object sender, RoutedEventArgs e) {
			OpenAppWindow (null);
		}

		private bool? OpenAppWindow (RegistryApp app) {
			bool isNew = false;

			if (app == null) {
				isNew = true;
				app = new RegistryApp ();
			}

			bool? result = new EditRegistryAppWindow (app).ShowDialog ();

			if (result.HasValue && result.Value) {
				if (isNew) {
					this.AppList.Add (app);
				}
			}

			return result;
		}

		private void EditAppClicked (object sender, RoutedEventArgs e) {
			Button button = sender as Button;
			bool? result = OpenAppWindow ((RegistryApp) button.DataContext);

			if (result.HasValue && result.Value) {
				((Image) (button.Parent as Grid).Children [0]).GetBindingExpression (Image.SourceProperty).UpdateTarget ();
				((TextBlock) (button.Parent as Grid).Children [1]).GetBindingExpression (TextBlock.TextProperty).UpdateTarget ();
			}
		}

		private void DeleteAppClicked (object sender, RoutedEventArgs e) {
			Button button = sender as Button;
			RegistryApp app = (RegistryApp) button.DataContext;
			
			bool? result = new DeleteRegistryAppWindow (app).ShowDialog ();

			if (result.HasValue && result.Value) {
				app.DeleteFromRegistry ();
				this.AppList.Remove (app);
			}
		}

		private void ViewHelpExecuted (object sender, ExecutedRoutedEventArgs e) {
			new HelpWindow ().ShowDialog ();
		}

		private void AboutWinRunnerExecuted (object sender, ExecutedRoutedEventArgs e) {
			new AboutWindow ().ShowDialog ();
		}

		private void WindowLocationChanged (object sender, EventArgs e) {
			this.timer.Stop ();
			this.timer.Start ();
		}

		private void WindowSizeChanged (object sender, SizeChangedEventArgs e) {
			this.timer.Stop ();
			this.timer.Start ();
		}

		private void TimerTicked (object sender, EventArgs e) {
			this.timer.Stop ();

			this.preferences.SetConfig (UserPreferences.WindowLeft, this.Left);
			this.preferences.SetConfig (UserPreferences.WindowTop, this.Top);
			this.preferences.SetConfig (UserPreferences.WindowWidth, this.Width);
			this.preferences.SetConfig (UserPreferences.WindowHeight, this.Height);
			this.preferences.Save ();
		}

		private void LoadPreferences () {
			this.preferences.Load ();
			this.Left = double.Parse (this.preferences.GetConfig (UserPreferences.WindowLeft));
			this.Top = double.Parse (this.preferences.GetConfig (UserPreferences.WindowTop));
			this.Width = double.Parse (this.preferences.GetConfig (UserPreferences.WindowWidth));
			this.Height = double.Parse (this.preferences.GetConfig (UserPreferences.WindowHeight));
		}
	}
}
