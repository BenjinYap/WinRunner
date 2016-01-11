

using Microsoft.Win32;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
namespace WinRunner.Models {
	public class RegistryAppList:ObservableCollection <RegistryApp> {

		public void LoadAppsFromRegistry () {
			RegistryKey rootKey = RegistryHelper.OpenAppPaths ();
			string [] keyNames = rootKey.GetSubKeyNames ();

			foreach (string keyName in keyNames) {
				if (Regex.Match (keyName, @"\.exe$", RegexOptions.IgnoreCase).Success) {
					RegistryKey key = rootKey.OpenSubKey (keyName, true);
					this.Add (new RegistryApp (key));
				}
			}

			rootKey.Close ();
		}
	}
}
