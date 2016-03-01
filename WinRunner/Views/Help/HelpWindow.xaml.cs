using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Threading;
using WinRunner.Views.Help.Documents;

namespace WinRunner.Views.Help {
	/// <summary>
	/// Interaction logic for HelpWindow.xaml
	/// </summary>
	public partial class HelpWindow:Window {
		public HelpDocument [] Documents { get; set; }

		public Stack <Action> BackCommands { get; set; }
		public Stack <Action> ForwardCommands { get; set; }

		private bool throwawayKeyUp = false;
		private bool ignoreSelectionChange = false;

		public HelpWindow () {
			this.Documents = new HelpDocument [] {
				new CautionDocument (),
				new OverviewDocument (),
			};
			
			InitializeComponent ();

			this.BackCommands = new Stack <Action> ();
			this.ForwardCommands = new Stack <Action> ();
		}

		private void SelectionChanged (object sender, SelectionChangedEventArgs e) {
			if (ignoreSelectionChange == false && e.RemovedItems.Count > 0) {
				object doc = e.RemovedItems [0];
				this.BackCommands.Push (() => { this.DocumentListBox.SelectedItem = doc; });
			}
		}

		private void NavigateRequested (object sender, RequestNavigateEventArgs e) {
			Process.Start (new ProcessStartInfo (e.Uri.AbsoluteUri));
			e.Handled = true;
		}

		private void KeyUpped (object sender, KeyEventArgs e) {
			if (throwawayKeyUp && e.Key == Key.F1 || e.Key == Key.Escape) {
				this.Close ();
			}

			throwawayKeyUp = true;
		}

		private void BrowseBackExecuted (object sender, ExecutedRoutedEventArgs e) {
			if (this.BackCommands.Count > 0) {
				this.ignoreSelectionChange = true;
				this.BackCommands.Pop () ();
				this.ignoreSelectionChange = false;
			}
		}

		private void BrowseForwardExecuted (object sender, ExecutedRoutedEventArgs e) {
			if (this.ForwardCommands.Count > 0) {

			}
		}
	}
}
