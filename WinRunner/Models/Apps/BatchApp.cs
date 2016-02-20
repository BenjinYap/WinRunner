

using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
namespace WinRunner.Models.Apps {
	public class BatchApp:App {
		private string script;
		public string Script {
			get { return this.script; }
			set {
				this.script = value;
				base.OnPropertyChanged ();
			}
		}

		private string oldScript;

		public BatchApp ():base () {
			this.Script = "";
		}

		public BatchApp (RegistryKey regKey):base (regKey) {
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
			this.WriteToBatchFile ();
			base.GetIconFromPath (this.GetBatchFilePath ());
			base.FlushToRegistry ();
			this.regKey.SetValue ("", this.GetBatchFilePath ());
		}

		public override void DeleteFromRegistry () {
			base.DeleteFromRegistry ();
			File.Delete (this.GetBatchFilePath ());
		}

		public static string BatchFilesPath = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments) + @"\WinRunner\BatchFiles\";

		private string GetBatchFilePath () {
			return BatchFilesPath + this.Name + ".bat";
		}

		private void WriteToBatchFile () {
			FileInfo fileInfo = new FileInfo (this.GetBatchFilePath ());
			fileInfo.Directory.Create ();
			File.WriteAllText (fileInfo.FullName, this.Script);
		}
	}
}
