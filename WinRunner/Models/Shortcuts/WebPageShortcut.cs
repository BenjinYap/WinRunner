﻿

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
	public class WebPageShortcut:FileShortcut {
		private const string UrlKeyName = "Url";

		private string url;
		public string Url {
			get { return this.url; }
			set {
				this.url = value;
				base.OnPropertyChanged ();
			}
		}

		private string oldUrl;

		private readonly static string WebPageFolderPath = System.IO.Path.Combine (Shortcut.DocumentsPath, "WebPageScripts");

		private string script {
			get { return string.Format ("{0} {1}", this.Path, this.Url); }
		}

		private string scriptPath {
			get { return System.IO.Path.Combine (WebPageShortcut.WebPageFolderPath, this.Name + ".bat"); }
		}

		public WebPageShortcut ():base () {
			this.Url = "";
		}

		public WebPageShortcut (RegistryKey regKey):base (regKey) {
			this.Url = regKey.GetValue (WebPageShortcut.UrlKeyName).ToString ();
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
			this.regKey.SetValue (WebPageShortcut.UrlKeyName, this.Url);

			//create the batch file
			this.FlushToScript ();
		}

		public override void DeleteFromRegistry () {
			base.DeleteFromRegistry ();
			File.Delete (this.scriptPath);
			File.Delete (System.IO.Path.Combine (WebPageShortcut.WebPageFolderPath, this.oldName + ".bat"));
		}

		private void FlushToScript () {
			FileInfo fileInfo = new FileInfo (this.scriptPath);
			fileInfo.Directory.Create ();
			File.WriteAllText (fileInfo.FullName, this.script);
		}
	}
}
