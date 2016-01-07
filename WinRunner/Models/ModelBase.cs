

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace WinRunner.Models {
	public class ModelBase:INotifyPropertyChanged, INotifyDataErrorInfo {
		#region INotifyPropertyChanged
		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged ([CallerMemberName] string propertyName = null) {
			PropertyChangedEventHandler handler = this.PropertyChanged;

			if (handler != null) {
				handler (this, new PropertyChangedEventArgs (propertyName));
			}
		}
		#endregion

		#region INotifyDataErrorInfo
		private Dictionary <string, List <string>> errors = new Dictionary <string, List <string>> ();

		public event EventHandler <DataErrorsChangedEventArgs> ErrorsChanged;

		public IEnumerable GetErrors (string propertyName) {
			if (this.errors.ContainsKey (propertyName)) {
				return this.errors [propertyName];
			}

			return null;
		}

		public bool HasErrors {
			get { return this.errors.Count > 0; }
		}

		public void AddError (string error, [CallerMemberName] string propertyName = null) {
			if (this.errors.ContainsKey (propertyName) == false) {
				this.errors [propertyName] = new List <string> ();
			}

			if (this.errors [propertyName].Contains (error) == false) {
				this.errors [propertyName].Add (error);
				this.NotifyErrorsChanged (propertyName);
			}
		}

		public void RemoveError (string error, [CallerMemberName] string propertyName = null) {
			if (this.errors.ContainsKey (propertyName) && this.errors [propertyName].Contains (error)) {
				this.errors [propertyName].Remove (error);

				if (this.errors [propertyName].Count <= 0) {
					this.errors.Remove (propertyName);
				}

				this.NotifyErrorsChanged (propertyName);
			}
		}

		public void NotifyErrorsChanged (string propertyName) {
			EventHandler <DataErrorsChangedEventArgs> handler = this.ErrorsChanged;

			if (handler != null) {
				handler (this, new DataErrorsChangedEventArgs (propertyName));
			}
		}
		#endregion
	}
}
