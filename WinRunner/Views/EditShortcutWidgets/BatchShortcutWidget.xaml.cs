﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace WinRunner.Views.EditShortcutWidgets {
	/// <summary>
	/// Interaction logic for BatchAppWidget.xaml
	/// </summary>
	public partial class BatchShortcutWidget:EditShortcutWidget {
		public BatchShortcutWidget () {
			InitializeComponent ();
		}

		private void KeyDowned (object sender, KeyEventArgs e) {
			if (e.Key == Key.Tab && this.TxtScript.SelectionLength > 0) {
				string script = this.TxtScript.Text;
				string newScript = script;

				//how many new lines are in the selection
				int numNewLines = this.TxtScript.SelectedText.Split ('\n').Length - 1;

				//get the beginning of the first line
				int firstLineStartIndex = script.LastIndexOf ("\n", this.TxtScript.SelectionStart, this.TxtScript.SelectionStart);

				//get the end of the last line
				int lastLineEndIndex = script.IndexOf ("\n", this.TxtScript.SelectionStart + this.TxtScript.SelectionLength);

				if (firstLineStartIndex <= -1) {
					firstLineStartIndex = 0;
				}

				if (lastLineEndIndex <= -1) {
					lastLineEndIndex = script.Length;
				}

				if (Keyboard.IsKeyDown (Key.LeftShift) || Keyboard.IsKeyDown (Key.RightShift)) {
					//string interestedText = script.Substring (firstLineStartIndex, lastLineEndIndex - firstLineStartIndex);
					//string newText = interestedText;
					//string [] lines = interestedText.Split ('\n');
					
					//for (int i = 0; i < lines.Length; i++) {
						
					//	if (lines [i][0] == '\t') {
					//		lines [i] = lines [i].Substring (1);
					//	}
					//}

					//newText = string.Join ("\n", lines);
					//newScript = script.Replace (interestedText, newText);
					//this.TxtScript.Text = newScript;
				} else {
					//insert a tab after every selected newline
					newScript = new Regex ("\n").Replace (newScript, "\n\t", numNewLines, firstLineStartIndex + 1);

					//insert tab in the first line
					newScript = newScript.Insert (firstLineStartIndex + (firstLineStartIndex == 0 ? 0 : 1), "\t");

					this.TxtScript.Text = newScript;
					this.TxtScript.SelectionStart = firstLineStartIndex;
					this.TxtScript.SelectionLength = lastLineEndIndex - firstLineStartIndex + numNewLines + 1;
				}

				//cancel event
				e.Handled = true;
			}
		}
	}
}
