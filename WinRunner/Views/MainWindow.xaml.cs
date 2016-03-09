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
using WinRunner.Models.Shortcuts;
using WinRunner.Views;

namespace WinRunner {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow:Window {
		public ShortcutList ShortcutList { get; set; }

		private UserPreferences preferences = new UserPreferences ();
		private DispatcherTimer timer = new DispatcherTimer { Interval = new TimeSpan (0, 0, 0, 0, 500) };

		public MainWindow () {
			this.ShortcutList = new ShortcutList ();
			
			InitializeComponent ();
			
			this.ShortcutList.LoadShortcutsFromRegistry ();

			this.timer.Tick += TimerTicked;
			this.timer.IsEnabled = false;

			this.LoadPreferences ();

			DispatcherTimer awd = new DispatcherTimer { Interval = new TimeSpan (0, 0, 0, 0, 200) };
			awd.Start ();
			awd.Tick += (a, b) => {
				//new DeleteAppWindow (this.AppList [0]).ShowDialog ();
				//OpenShortcutWindow (this.AppList [0], false);
				awd.Stop ();
			};
		}

		private void NewBatchShortcutClicked (object sender, RoutedEventArgs e) {
			OpenShortcutWindow (new BatchShortcut (), true);
		}

		private void NewFileShortcutClicked (object sender, RoutedEventArgs e) {
			OpenShortcutWindow (new FileShortcut (), true);
		}

		private void NewFolderShortcutClicked (object sender, RoutedEventArgs e) {
			OpenShortcutWindow (new FolderShortcut (), true);
		}

		private bool? OpenShortcutWindow (Shortcut shortcut, bool isNew) {
			EditShortcutWindow window = new EditShortcutWindow (shortcut);
			window.Owner = this;
			bool? result = window.ShowDialog ();

			if (result.HasValue && result.Value) {
				if (isNew) {
					this.ShortcutList.Add (shortcut);
				}
			}
			
			return result;
		}

		private void EditShortcutClicked (object sender, RoutedEventArgs e) {
			Button button = sender as Button;
			bool? result = OpenShortcutWindow ((Shortcut) button.DataContext, false);
			
			if (result.HasValue && result.Value) {
				((Image) (button.Parent as Grid).Children [0]).GetBindingExpression (Image.SourceProperty).UpdateTarget ();
				Grid grid = (Grid) button.Parent;
				Grid grid2 = (Grid) grid.Children [1];
				TextBlock textBlock = (TextBlock) grid2.Children [0];
				textBlock.GetBindingExpression (TextBlock.TextProperty).UpdateTarget ();
			}
		}

		private void DeleteShortcutClicked (object sender, RoutedEventArgs e) {
			Button button = sender as Button;
			Shortcut shortcut = (Shortcut) button.DataContext;
			
			DeleteShortcutWindow window = new DeleteShortcutWindow (shortcut);
			window.Owner = this;
			bool? result = window.ShowDialog ();

			if (result.HasValue && result.Value) {
				shortcut.DeleteFromRegistry ();
				this.ShortcutList.Remove (shortcut);
			}
		}

		private void OpenDocumentsFolderExecuted (object sender, ExecutedRoutedEventArgs e) {
			Process.Start (preferences.DocumentsPath);
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
