

using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using WinRunner.Resources;
namespace WinRunner.Models.Shortcuts {
	public class FolderShortcut:Shortcut {
		private const string PathKeyName = "FolderPath";

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

		private string script {
			get { return "start explorer \"" + this.Path + "\""; }
		}

		private string scriptPath {
			get { return System.IO.Path.Combine (FolderShortcut.ScriptFolderPath, this.Name + ".bat"); }
		}

		public FolderShortcut ():base () {
			this.Path = "";
			this.SetIcon ();
		}

		public FolderShortcut (RegistryKey regKey):base (regKey) {
			//get the folder path from the key
			this.Path = regKey.GetValue (FolderShortcut.PathKeyName).ToString ();

			this.SetIcon ();

			//flush immediately to generate the batch file and to ensure the exe path matches
			this.FlushToRegistry ();
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

			//set the exe path to the batch file
			this.regKey.SetValue ("", this.scriptPath);

			//save the path of the folder to be opened
			this.regKey.SetValue (FolderShortcut.PathKeyName, this.Path);

			//create the batch file
			this.FlushToScript ();
		}

		public override void DeleteFromRegistry () {
			base.DeleteFromRegistry ();
			File.Delete (this.scriptPath);
			//File.Delete (BatchShortcut.ScriptFolderPath + this.oldName + ".bat");
		}

		private void ValidatePath ([CallerMemberName] string propertyName = null) {
			if (this.Path.Length <= 0) {
				base.AddError (General.Required, propertyName);
			} else {
				base.RemoveError (General.Required, propertyName);
				
				if (Directory.Exists (this.Path) == false) {
					base.AddError (General.DoesNotExist, propertyName);
				} else {
					base.RemoveError (General.DoesNotExist, propertyName);
				}
			}
		}

		private void SetIcon () {
			this.Icon = new System.Windows.Media.Imaging.BitmapImage (new Uri ("../Resources/folder.png", UriKind.Relative));
		}

		private readonly static string ScriptFolderPath = System.IO.Path.Combine (Shortcut.DocumentsPath, "FolderScripts");

		private void FlushToScript () {
			FileInfo fileInfo = new FileInfo (this.scriptPath);
			fileInfo.Directory.Create ();
			File.WriteAllText (fileInfo.FullName, this.script);
		}
	}
}
