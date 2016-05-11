

using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using WinRunner.Models.ShortcutProperties;
namespace WinRunner.Models.Shortcuts {
	public class BatchShortcut:Shortcut {
		private const string CodeKeyName = "Code";

		public string Script {
			get { return this.batchProp.Value; }
			set {
				this.batchProp.Value = value;
				this.ValidateProperty (this.batchProp);
				base.OnPropertyChanged ();
			}
		}
		private BatchScriptProperty batchProp = new BatchScriptProperty ();

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

			//flush immediately to generate the batch file and to ensure the exe path matches
			this.FlushToRegistry ();
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
			File.Delete (Path.Combine (BatchShortcut.ScriptFolderPath, this.oldName + ".bat"));
		}

		private readonly static string ScriptFolderPath = Path.Combine (Shortcut.DocumentsPath, "BatchScripts");

		private void FlushToScript () {
			FileInfo fileInfo = new FileInfo (this.scriptPath);
			fileInfo.Directory.Create ();
			File.WriteAllText (fileInfo.FullName, this.Script);
		}
	}
}
