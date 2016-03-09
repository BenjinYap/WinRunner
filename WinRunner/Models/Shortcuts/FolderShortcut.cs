

using Microsoft.Win32;
using System;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using WinRunner.Resources;
namespace WinRunner.Models.Shortcuts {
	public class FolderShortcut:Shortcut {
		private string path;
		public string Path {
			get { return this.path; }
			set {
				this.path = value;
				this.ValidatePath ();
				base.OnPropertyChanged ();
			}
		}

		private string oldPath;

		public FolderShortcut ():base () {
			this.Path = "";
			this.SetIcon ();
		}

		public FolderShortcut (RegistryKey regKey):base (regKey) {
			this.Path = regKey.GetValue ("").ToString ();
			this.SetIcon ();
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

		private void SetIcon () {
			this.Icon = new System.Windows.Media.Imaging.BitmapImage (new Uri ("Resources/folder.png", UriKind.Relative));
		}
	}
}
