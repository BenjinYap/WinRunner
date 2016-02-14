
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using WinRunner.Models;
using WinRunner.Models.Apps;
namespace WinRunner.Views.EditAppWidgets {
	public class EditAppWidget:UserControl, INotifyPropertyChanged {
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

		public static readonly DependencyProperty AppProperty = DependencyProperty.Register ("App", typeof (App), typeof (EditAppWidget));

		public App App {
			get { return (App) GetValue (AppProperty); }
			set { SetValue (AppProperty, value); }
		}
	}
}
