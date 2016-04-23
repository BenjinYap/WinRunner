

using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;
using WinRunner.Resources;
namespace WinRunner.Models.Shortcuts {
	public class MSEdgeShortcut:Shortcut {
		private const string UrlKeyName = "Url";

		private string url;
		public string Url {
			get { return this.url; }
			set {
				this.url = value;
				this.ValidateUrl ();
				base.OnPropertyChanged ();
			}
		}

		private string oldUrl;

		private readonly static string MSEdgeFolderPath = System.IO.Path.Combine (Shortcut.DocumentsPath, "MSEdgeScripts");

		private string script {
			get { return string.Format ("start microsoft-edge:{0}", this.Url); }
		}

		private string scriptPath {
			get { return System.IO.Path.Combine (MSEdgeShortcut.MSEdgeFolderPath, this.Name + ".bat"); }
		}

		public MSEdgeShortcut ():base () {
			this.Url = "";
		}

		public MSEdgeShortcut (RegistryKey regKey):base (regKey) {
			this.Url = regKey.GetValue (MSEdgeShortcut.UrlKeyName).ToString ();
		}

		public override void RememberProperties () {
			base.RememberProperties ();
			this.oldUrl = this.Url;
		}

		public override void RevertProperties () {
			base.RevertProperties ();
			this.Url = this.oldUrl;
		}

		public override void FlushToRegistry () {
			base.FlushToRegistry ();

			//set the exe path to the batch file
			this.regKey.SetValue ("", this.scriptPath);

			//save the path of the folder to be opened
			this.regKey.SetValue (MSEdgeShortcut.UrlKeyName, this.Url);

			//create the batch file
			this.FlushToScript ();
		}

		public override void DeleteFromRegistry () {
			base.DeleteFromRegistry ();
			File.Delete (this.scriptPath);
			File.Delete (System.IO.Path.Combine (MSEdgeShortcut.MSEdgeFolderPath, this.oldName + ".bat"));
		}

		private void FlushToScript () {
			FileInfo fileInfo = new FileInfo (this.scriptPath);
			fileInfo.Directory.Create ();
			File.WriteAllText (fileInfo.FullName, this.script);
		}

		private void ValidateUrl ([CallerMemberName] string propertyName = null) {
			if (this.Url.Length <= 0) {
				base.AddError (General.Required, propertyName);
			} else {
				base.RemoveError (General.Required, propertyName);

				Uri uri = null;

				if (Uri.TryCreate (this.Url, UriKind.Absolute, out uri)) {
					base.RemoveError (General.Invalid, propertyName);
				} else {
					base.AddError (General.Invalid, propertyName);
				}
			}
		}
	}
}
