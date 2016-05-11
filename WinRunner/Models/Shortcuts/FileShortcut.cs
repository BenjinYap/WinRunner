

using Microsoft.Win32;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;
using WinRunner.Models.ShortcutProperties;
using WinRunner.Resources;
namespace WinRunner.Models.Shortcuts {
	public class FileShortcut:Shortcut {

		public string Path {
			get { return this.fileProp.Value; }
			set {
				this.fileProp.Value = value;
				base.ValidateProperty (this.fileProp);
				base.OnPropertyChanged ();
				base.GetIconFromPath (value);
			}
		}
		private FileProperty fileProp = new FileProperty ();

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
	}
}
