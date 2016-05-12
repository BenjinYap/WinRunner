

using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;
using WinRunner.Models.ShortcutProperties;
using WinRunner.Resources;
namespace WinRunner.Models.Shortcuts {
	public class MSEdgeShortcut:Shortcut {
		private const string UrlKeyName = "Url";

		public string Url {
			get { return this.urlProp.Value; }
			set {
				this.urlProp.Value = value;
				base.ValidateProperty (this.urlProp);
				base.OnPropertyChanged ();
			}
		}
		private UrlProperty urlProp = new UrlProperty ();

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
			this.SetIcon ();
		}

		public MSEdgeShortcut (RegistryKey regKey):base (regKey) {
			this.Url = regKey.GetValue (MSEdgeShortcut.UrlKeyName).ToString ();

			this.SetIcon ();
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

		private void SetIcon () {
			this.Icon = new System.Windows.Media.Imaging.BitmapImage (new Uri ("../Resources/ms-edge.png", UriKind.Relative));
		}
	}
}
