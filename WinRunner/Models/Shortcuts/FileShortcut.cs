

using Microsoft.Win32;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;
using WinRunner.Resources;
namespace WinRunner.Models.Shortcuts {
	public class FileShortcut:Shortcut {

		private string path;
		public string Path {
			get { return this.path; }
			set {
				this.path = value;
				this.ValidatePath ();
				base.OnPropertyChanged ();
				base.GetIconFromPath (value);
			}
		}

		private string oldPath;

		public FileShortcut ():base () {
			this.Path = "";
		}

		public FileShortcut (RegistryKey regKey):base (regKey) {
			this.Path = regKey.GetValue ("").ToString ();
		}

		public override void RememberProperties () {
			base.RememberProperties ();
			this.oldPath = this.Path;
		}

		public override void RevertProperties () {
			base.RevertProperties ();
			this.Path = this.oldPath;
		}

		public override void FlushToRegistry () {
			base.FlushToRegistry ();
			this.regKey.SetValue ("", this.Path);
		}

		private void ValidatePath ([CallerMemberName] string propertyName = null) {
			if (this.Path.Length <= 0) {
				base.AddError (General.Required, propertyName);
			} else {
				base.RemoveError (General.Required, propertyName);

				if (File.Exists (this.Path) == false) {
					base.AddError (General.DoesNotExist, propertyName);
				} else {
					base.RemoveError (General.DoesNotExist, propertyName);
				}
			}
		}
	}
}
