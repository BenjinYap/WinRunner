using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WinRunner.Views;
using WinRunner.Views.Help;

namespace WinRunner {
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class Program:Application {
		
		protected override void OnStartup(StartupEventArgs e) {
			// Select the text in a TextBox when it receives focus.
			EventManager.RegisterClassHandler(typeof(TextBox), TextBox.PreviewMouseLeftButtonDownEvent,
				new MouseButtonEventHandler(SelectivelyIgnoreMouseButton));
			EventManager.RegisterClassHandler(typeof(TextBox), TextBox.GotKeyboardFocusEvent, 
				new RoutedEventHandler(SelectAllText));
			EventManager.RegisterClassHandler(typeof(TextBox), TextBox.MouseDoubleClickEvent,
				new RoutedEventHandler(SelectAllText));
			base.OnStartup(e); 
		}

		private void SelectivelyIgnoreMouseButton(object sender, MouseButtonEventArgs e) {
			// Find the TextBox
			DependencyObject parent = e.OriginalSource as UIElement;
			while (parent != null && !(parent is TextBox))
				parent = VisualTreeHelper.GetParent(parent);
			
			if (parent != null) {
				if (this.BelongsToHelpWindows (parent)) {
					return;
				}

				TextBox textBox = (TextBox) parent;
				if (!textBox.IsKeyboardFocusWithin) {
					// If the text box is not yet focused, give it the focus and
					// stop further processing of this click event.
					textBox.Focus();
					e.Handled = true;
				}
			}
		}

		private void SelectAllText(object sender, RoutedEventArgs e) {
			TextBox textBox = e.OriginalSource as TextBox;

			if (textBox != null && this.BelongsToHelpWindows (textBox) == false) {
				textBox.SelectAll();
			}
		}

		private bool BelongsToHelpWindows (DependencyObject element) {
			DependencyObject parent = element;

			while (parent != null) {
				parent = VisualTreeHelper.GetParent (parent);

				if (parent != null && parent is AboutWindow || parent is HelpWindow) {
					return true;
				}
			}

			return false;
		}
	}
}
