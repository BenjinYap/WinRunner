

using Microsoft.Win32;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;
using WinRunner.Resources;
namespace WinRunner.Models.Apps {
	public class PathApp:App {

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

		public PathApp ():base () {
			this.Path = "";
		}

		public PathApp (RegistryKey regKey):base (regKey) {
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
				base.AddError (General.PathRequired, propertyName);
			} else {
				base.RemoveError (General.PathRequired, propertyName);

				if (Directory.Exists (this.Path) == false && File.Exists (this.Path) == false) {
					base.AddError (General.PathDoesNotExist, propertyName);
				} else {
					base.RemoveError (General.PathDoesNotExist, propertyName);
				}
			}
		}
	}
}
