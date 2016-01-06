

using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace WinRunner.Models {
	public class RegistryApp:INotifyPropertyChanged {
		public event PropertyChangedEventHandler PropertyChanged;

		public string Name {
			get { return this.name; }
			set {
				this.name = value;
				OnPropertyChanged ();
			}
		}

		private string name;

		public RegistryApp () {

		}

		public RegistryApp (string name) {
			this.Name = name;
		}

		protected virtual void OnPropertyChanged ([CallerMemberName] string propertyName = null) {
			PropertyChangedEventHandler handler = this.PropertyChanged;

			if (handler != null) {
				handler (this, new PropertyChangedEventArgs (propertyName));
			}
		}
	}
}
