

using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using WinRunner.Models.Apps;
namespace WinRunner.Models {
	public class AppList:ObservableCollection <App> {

		public void LoadAppsFromRegistry () {
			RegistryKey rootKey = RegistryHelper.OpenAppPaths ();
			string [] keyNames = rootKey.GetSubKeyNames ();

			foreach (string keyName in keyNames) {
				if (Regex.Match (keyName, @"\.exe$", RegexOptions.IgnoreCase).Success) {
					RegistryKey key = rootKey.OpenSubKey (keyName, true);
					App app = null;

					if (key.GetValue ("").ToString ().Contains (BatchApp.BatchFilesPath)) {
						app = new BatchApp (key);
					} else {
						app = new PathApp (key);
					}

					this.Add (app);
				}
			}

			rootKey.Close ();
		}
	}
}
