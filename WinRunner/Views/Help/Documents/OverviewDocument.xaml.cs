﻿using System;
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

namespace WinRunner.Views.Help.Documents {
	/// <summary>
	/// Interaction logic for OverviewDocument.xaml
	/// </summary>
	public partial class OverviewDocument:HelpDocument {
		public OverviewDocument () {
			InitializeComponent ();
		}

		private void NavigateRequested (object sender, RequestNavigateEventArgs e) {
			Process.Start (new ProcessStartInfo (e.Uri.AbsoluteUri));
			e.Handled = true;
		}
	}
}
