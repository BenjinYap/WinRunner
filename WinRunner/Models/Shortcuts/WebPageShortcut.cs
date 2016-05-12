

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
	public class WebPageShortcut:Shortcut {
		private const string BrowserKeyName = "Browser";
		private const string UrlKeyName = "Url";

		public string Browser {
			get { return this.fileProp.Value; }
			set {
				this.fileProp.Value = value;
				base.ValidateProperty (this.fileProp);
				base.OnPropertyChanged ();
				base.GetIconFromPath (value);
			}
		}
		private FileProperty fileProp = new FileProperty ();

		public string Url {
			get { return this.urlProp.Value; }
			set {
				this.urlProp.Value = value;
				base.ValidateProperty (this.urlProp);
				base.OnPropertyChanged ();
			}
		}
		private UrlProperty urlProp = new UrlProperty ();

		private string oldBrowser;
		private string oldUrl;

		private readonly static string WebPageFolderPath = System.IO.Path.Combine (Shortcut.DocumentsPath, "WebPageScripts");

		private string script {
			get { return string.Format ("start \"{0}\" {1}", this.urlProp.Value, this.Url); }
		}

		private string scriptPath {
			get { return System.IO.Path.Combine (WebPageShortcut.WebPageFolderPath, this.Name + ".bat"); }
		}

		public WebPageShortcut ():base () {
			this.Browser = "";
			this.Url = "";
		}

		public WebPageShortcut (RegistryKey regKey):base (regKey) {
			//set the browser
			this.Browser = regKey.GetValue (WebPageShortcut.BrowserKeyName).ToString ();

			//set the url
			this.Url = regKey.GetValue (WebPageShortcut.UrlKeyName).ToString ();

			//flush immediately to generate the batch file and to ensure the exe path matches
			this.FlushToRegistry ();
		}

		public override void RememberProperties () {
			base.RememberProperties ();
			this.oldBrowser = this.Browser;
			this.oldUrl = this.Url;
		}

		public override void RevertProperties () {
			base.RevertProperties ();
			this.Browser = this.oldBrowser;
			this.Url = this.oldUrl;
		}

		public override void FlushToRegistry () {
			base.FlushToRegistry ();

			//set the exe path to the batch file
			this.regKey.SetValue ("", this.scriptPath);

			//save the browser
			this.regKey.SetValue (WebPageShortcut.BrowserKeyName, this.Browser);

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
