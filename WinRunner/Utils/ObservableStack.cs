

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
namespace WinRunner.Utils {
	public class ObservableStack <T>:ObservableCollection <T> {

		public void Push (T item) {
			this.Add (item);
		}

		public T Pop () {
			T item = this [this.Count - 1];
			this.RemoveItem (this.Count - 1);
			return item;
		}
	}
}
