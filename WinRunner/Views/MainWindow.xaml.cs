﻿using System;
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
			
			this.AppList.Add (new RegistryApp ("hello", @"C:\Users\Benjin\Desktop\awd.bat"));
			this.AppList.Add (new RegistryApp ("goodbye", @"C:\Users\Benjin\Desktop\audacity-win-2.1.1.exe"));

			new EditRegistryAppWindow ().ShowDialog ();
		}
	}
}
