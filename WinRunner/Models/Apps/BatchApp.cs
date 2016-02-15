

using Microsoft.Win32;
using System.Diagnostics;
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
			//base.FlushToRegistry ();
			//this.regKey.SetValue ("", this.Path);
		}
	}
}
