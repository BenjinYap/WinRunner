

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
			//this.Path = regKey.GetValue ("").ToString ();
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
			//base.FlushToRegistry ();
			//this.regKey.SetValue ("", this.Path);
		}

		public static string BatchFilesPath = Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments) + @"\WinRunner\BatchFiles\";

		private void WriteToBatchFile () {
			FileInfo fileInfo = new FileInfo (BatchFilesPath + this.Name + ".bat");
			fileInfo.Directory.Create ();
			File.WriteAllText (fileInfo.FullName, this.Script);
		}
	}
}
