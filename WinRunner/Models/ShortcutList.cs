

using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using WinRunner.Models.Shortcuts;
namespace WinRunner.Models {
	public class ShortcutList:ObservableCollection <Shortcut> {

		public void LoadShortcutsFromRegistry () {
			RegistryKey rootKey = RegistryHelper.OpenAppPaths ();
			string [] keyNames = rootKey.GetSubKeyNames ();

			foreach (string keyName in keyNames) {
				if (Regex.Match (keyName, @"\.exe$", RegexOptions.IgnoreCase).Success) {
					RegistryKey key = rootKey.OpenSubKey (keyName, true);
					Shortcut Shortcut = null;

					if (key.GetValue ("").ToString ().Contains (BatchShortcut.BatchFilesPath)) {
						Shortcut = new BatchShortcut (key);
					} else {
						Shortcut = new PathShortcut (key);
					}

					this.Add (Shortcut);
				}
			}

			rootKey.Close ();
		}
	}
}
