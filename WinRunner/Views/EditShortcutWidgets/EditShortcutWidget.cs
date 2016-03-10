
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using WinRunner.Models;
using WinRunner.Models.Shortcuts;
namespace WinRunner.Views.EditShortcutWidgets {
	public class EditShortcutWidget:UserControl, INotifyPropertyChanged {
		#region INotifyPropertyChanged
		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged ([CallerMemberName] string propertyName = null) {
			PropertyChangedEventHandler handler = this.PropertyChanged;

			if (handler != null) {
				handler (this, new PropertyChangedEventArgs (propertyName));
			}
		}
		#endregion

		private bool valid;
		public bool Valid {
			get { return this.valid; }
			set {
				this.valid = value;
				this.OnPropertyChanged ();
			}
		}
		
		public static readonly DependencyProperty ShortcutProperty = DependencyProperty.Register ("Shortcut", typeof (Shortcut), typeof (EditShortcutWidget));

		public Shortcut Shortcut {
			get { return (Shortcut) GetValue (ShortcutProperty); }
			set { SetValue (ShortcutProperty, value); }
		}
	}
}
