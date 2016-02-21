using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WinRunner.Resources;

namespace WinRunner.Views {
	public static class Commands {
		static Commands () {
			ApplicationCommands.Help.Text = Menu.ViewHelp;
			about.InputGestures.Add (new KeyGesture (Key.F12));
		}

		public static CanExecuteRoutedEventHandler CanExecuteAlways { get { return canExecuteAlways; } }

		private static void canExecuteAlways (object sender, CanExecuteRoutedEventArgs e) {
			e.CanExecute = true;
		}

		public static RoutedUICommand OpenDocumentsFolder { get { return openDocumentsFolder; } }
		private static RoutedUICommand openDocumentsFolder = new RoutedUICommand (Menu.OpenDocumentsFolder, Menu.OpenDocumentsFolder, typeof (Commands));

		public static RoutedUICommand About { get { return about; } }
		private static RoutedUICommand about = new RoutedUICommand (Menu.AboutWinRunner, Menu.AboutWinRunner, typeof (Commands));
	}
}
