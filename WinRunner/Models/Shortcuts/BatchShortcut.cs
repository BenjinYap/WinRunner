

using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
namespace WinRunner.Models.Shortcuts {
	public class BatchShortcut:Shortcut {
		private string script;
		public string Script {
			get { return this.script; }
			set {
				this.script = value;
				base.OnPropertyChanged ();
			}
		}

		private string oldScript;

		public BatchShortcut ():base () {
			this.Script = "";
		}

		public BatchShortcut (RegistryKey regKey):base (regKey) {
			if (File.Exists (this.GetBatchFilePath ())) {
				this.Script = File.ReadAllText (this.GetBatchFilePath ());
			} else {
				this.Script = "";
				this.WriteToBatchFile ();
			}

			base.GetIconFromPath (this.GetBatchFilePath ());
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
			this.regKey.SetValue ("", this.GetBatchFilePath ());
			this.WriteToBatchFile ();
			base.GetIconFromPath (this.GetBatchFilePath ());
		}

		public override void DeleteFromRegistry () {
			base.DeleteFromRegistry ();
			File.Delete (this.GetBatchFilePath ());
			File.Delete (BatchShortcut.ScriptFolderPath + this.oldName + ".bat");
		}

		public readonly static string ScriptFolderPath = Path.Combine (Shortcut.DocumentsPath, "BatchScripts");

		private string GetBatchFilePath () {
			return Path.Combine (BatchShortcut.ScriptFolderPath, this.Name + ".bat");
		}

		private void WriteToBatchFile () {
			FileInfo fileInfo = new FileInfo (this.GetBatchFilePath ());
			fileInfo.Directory.Create ();
			File.WriteAllText (fileInfo.FullName, this.Script);
		}
	}
}
