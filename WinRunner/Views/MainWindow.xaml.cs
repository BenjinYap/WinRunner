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
using WinRunner.Models.Apps;
using WinRunner.Views;

namespace WinRunner {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow:Window {
		public AppList AppList { get; set; }

		private UserPreferences preferences = new UserPreferences ();
		private DispatcherTimer timer = new DispatcherTimer { Interval = new TimeSpan (0, 0, 0, 0, 500) };

		public MainWindow () {
			this.AppList = new AppList ();
			
			InitializeComponent ();
			
			this.AppList.LoadAppsFromRegistry ();

			this.timer.Tick += TimerTicked;
			this.timer.IsEnabled = false;

			this.LoadPreferences ();

			DispatcherTimer awd = new DispatcherTimer { Interval = new TimeSpan (0, 0, 0, 0, 200) };
			awd.Start ();
			awd.Tick += (a, b) => {
				//new DeleteAppWindow (this.AppList [1]).ShowDialog ();
				//OpenAppWindow (this.AppList [0], true);
				awd.Stop ();
			};
		}

		private void NewBatchAppClicked (object sender, RoutedEventArgs e) {
			OpenAppWindow (new BatchApp (), true);
		}

		private void NewAppClicked (object sender, RoutedEventArgs e) {
			OpenAppWindow (new PathApp (), true);
		}

		private bool? OpenAppWindow (App app, bool isNew) {
			EditAppWindow window = new EditAppWindow (app);
			window.Owner = this;
			bool? result = window.ShowDialog ();

			if (result.HasValue && result.Value) {
				if (isNew) {
					this.AppList.Add (app);
				}
			}

			return result;
		}

		private void EditAppClicked (object sender, RoutedEventArgs e) {
			Button button = sender as Button;
			bool? result = OpenAppWindow ((App) button.DataContext, false);
			
			if (result.HasValue && result.Value) {
				((Image) (button.Parent as Grid).Children [0]).GetBindingExpression (Image.SourceProperty).UpdateTarget ();
				((TextBlock) ((Grid) (button.Parent as Grid).Children [1]).Children [0]).GetBindingExpression (TextBlock.TextProperty).UpdateTarget ();
			}
		}

		private void DeleteAppClicked (object sender, RoutedEventArgs e) {
			Button button = sender as Button;
			App app = (App) button.DataContext;
			
			DeleteAppWindow window = new DeleteAppWindow (app);
			window.Owner = this;
			bool? result = window.ShowDialog ();

			if (result.HasValue && result.Value) {
				app.DeleteFromRegistry ();
				this.AppList.Remove (app);
			}
		}

		private void ViewHelpExecuted (object sender, ExecutedRoutedEventArgs e) {
			HelpWindow window = new HelpWindow ();
			window.Owner = this;
			window.ShowDialog ();
		}

		private void AboutWinRunnerExecuted (object sender, ExecutedRoutedEventArgs e) {
			AboutWindow window = new AboutWindow ();
			window.Owner = this;
			window.ShowDialog ();
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

			double left, top, width, height;

			if (double.TryParse (this.preferences.GetConfig (UserPreferences.WindowLeft), out left)) {
				this.Left = left;
			}

			if (double.TryParse (this.preferences.GetConfig (UserPreferences.WindowTop), out top)) {
				this.Top = top;
			}

			if (double.TryParse (this.preferences.GetConfig (UserPreferences.WindowWidth), out width)) {
				this.Width = width;
			}

			if (double.TryParse (this.preferences.GetConfig (UserPreferences.WindowHeight), out height)) {
				this.Height = height;
			}
		}
	}
}
