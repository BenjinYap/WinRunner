﻿

using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using WinRunner.Models.ShortcutProperties;
using WinRunner.Resources;
namespace WinRunner.Models.Shortcuts {
	public class FolderShortcut:Shortcut {
		private const string PathKeyName = "FolderPath";

		public string Path {
			get { return this.folderProp.Value; }
			set {
				this.folderProp.Value = value;
				this.ValidateProperty (this.folderProp);
				base.OnPropertyChanged ();
			}
		}
		private FolderProperty folderProp = new FolderProperty ();

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
			File.Delete (System.IO.Path.Combine (FolderShortcut.ScriptFolderPath, this.oldName + ".bat"));
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
