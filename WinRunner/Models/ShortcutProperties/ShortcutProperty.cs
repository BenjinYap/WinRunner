

using System.Collections.Generic;
using System.Diagnostics;
namespace WinRunner.Models.ShortcutProperties {
	public abstract class ShortcutProperty {
		private string value;
		public string Value {
			get { return this.value; }
			set {
				this.value = value;
				this.Validate ();
			}
		}

		public bool HasErrors {
			get { return this.errors.ContainsValue (true); }
		}

		private Dictionary <string, bool> errors = new Dictionary <string, bool> ();
		public Dictionary <string, bool> Errors {
			get { return this.errors; }
		}

		protected abstract void Validate ();
	}
}
