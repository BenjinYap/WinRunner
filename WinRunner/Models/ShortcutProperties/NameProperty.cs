

using Microsoft.Win32;
using System.Diagnostics;
using WinRunner.Resources;
namespace WinRunner.Models.ShortcutProperties {
	public class NameProperty:ShortcutProperty {
		RegistryKey regKey;

		public NameProperty () {

		}

		public NameProperty (RegistryKey regKey) {
			this.regKey = regKey;
		}

		protected override void Validate () {
			//get all the existing names from the registry
			RegistryKey rootKey = RegistryHelper.OpenAppPaths ();
			string [] appKeyNames = rootKey.GetSubKeyNames ();
			rootKey.Close ();
			
			//check if any of the existing names conflict with the current name
			foreach (string keyName in appKeyNames) {
				if (this.GetAppName (keyName.ToLower ()) == this.Value.ToLower () && (this.regKey == null || this.GetAppName (this.regKey.Name.ToLower ()) != this.Value.ToLower ())) {
					this.Errors [General.NameExists] = true;
					break;
				} else {
					this.Errors [General.NameExists] = false;
				}
			}
			
			//check required
			if (this.Value.Length <= 0) {
				this.Errors [General.NameRequired] = true;
			} else {
				this.Errors [General.NameRequired] = false;
			}

			//check backslash
			if (this.Value.Contains (@"\")) {
				this.Errors [General.NameInvalidBackslash] = true;
			} else {
				this.Errors [General.NameInvalidBackslash] = false;
			}

			//check space
			if (this.Value.Contains (" ")) {
				this.Errors [General.NameInvalidSpace] = true;
			} else {
				this.Errors [General.NameInvalidSpace] = false;
			}

			//check dot
			if (this.Value.Contains (".")) {
				this.Errors [General.NameInvalidDot] = true;
			} else {
				this.Errors [General.NameInvalidDot] = false;
			}
		}

		private string GetAppName (string path) {
			return System.IO.Path.GetFileNameWithoutExtension (path);
		}
	}
}
