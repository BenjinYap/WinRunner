

using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
					
					//figure out the shortcut type from the type key
					ShortcutType type = (ShortcutType) Enum.Parse (typeof (ShortcutType), key.GetValue (Shortcut.TypeKeyName).ToString ());

					//create appropriate shortcut instance based on type
					if (type == ShortcutType.Batch) {
						Shortcut = new BatchShortcut (key);
					} else if (type == ShortcutType.File) {
						Shortcut = new FileShortcut (key);
					} else {
						throw new Exception ();
					}

					this.Add (Shortcut);
				}
			}

			rootKey.Close ();
		}
	}
}
