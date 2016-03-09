

using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
namespace WinRunner.Models.Shortcuts {
	public class BatchShortcut:Shortcut {
		private const string CodeKeyName = "Code";

		private string script;
		public string Script {
			get { return this.script; }
			set {
				this.script = value;
				base.OnPropertyChanged ();
			}
		}

		private string oldScript;

		private string scriptPath {
			get { return System.IO.Path.Combine (BatchShortcut.ScriptFolderPath, this.Name + ".bat"); }
		}

		public BatchShortcut ():base () {
			this.Script = "";
		}

		public BatchShortcut (RegistryKey regKey):base (regKey) {
			//get the code from the key
			this.Script = regKey.GetValue (BatchShortcut.CodeKeyName).ToString ();

			//create the batch file
			this.FlushToScript ();

			base.GetIconFromPath (this.scriptPath);
		}

		public override void RememberProperties () {
			base.RememberProperties ();
			this.oldScript = this.Script;
		}

		public override void RevertProperties () {
			base.RevertProperties ();
			this.Script = this.oldScript;
		}

		public override void FlushToRegistry () {
			base.FlushToRegistry ();

			//set the exe path to the batch file
			this.regKey.SetValue ("", this.scriptPath);

			//save the code
			this.regKey.SetValue (BatchShortcut.CodeKeyName, this.Script);

			//create the batch file
			this.FlushToScript ();

			base.GetIconFromPath (this.scriptPath);
		}

		public override void DeleteFromRegistry () {
			base.DeleteFromRegistry ();
			File.Delete (this.scriptPath);
			File.Delete (BatchShortcut.ScriptFolderPath + this.oldName + ".bat");
		}

		private readonly static string ScriptFolderPath = Path.Combine (Shortcut.DocumentsPath, "BatchScripts");

		private void FlushToScript () {
			FileInfo fileInfo = new FileInfo (this.scriptPath);
			fileInfo.Directory.Create ();
			File.WriteAllText (fileInfo.FullName, this.Script);
		}
	}
}
