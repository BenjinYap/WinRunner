

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;
namespace WinRunner.Models {
	public class RegistryApp:INotifyPropertyChanged {
		public event PropertyChangedEventHandler PropertyChanged;

		public BitmapImage Icon {
			get { return this.icon; }
			set {
				this.icon = value;
				OnPropertyChanged ();
			}
		}


		public string Name {
			get { return this.name; }
			set {
				this.name = value;
				OnPropertyChanged ();
			}
		}

		public string Path {
			get { return this.path; }
			set {
				this.path = value;
				OnPropertyChanged ();

				try {
					System.Drawing.Icon icon = System.Drawing.Icon.ExtractAssociatedIcon (value);
					Bitmap bitmap = icon.ToBitmap ();

					using (MemoryStream memory = new MemoryStream ())
					{
						bitmap.Save (memory, ImageFormat.Bmp);
						memory.Position = 0;
						BitmapImage bitmapimage = new BitmapImage();
						bitmapimage.BeginInit ();
						bitmapimage.StreamSource = memory;
						bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
						bitmapimage.EndInit ();
					
						this.Icon = bitmapimage;
					}
				} catch (Exception) {
					this.Icon = null;
				}
			}
		}

		private BitmapImage icon;
		private string name;
		private string path;

		private bool isNew;

		public RegistryApp () {
			this.isNew = true;
			this.Name = "";
			this.Path = "";
		}

		public RegistryApp (string name, string path) {
			this.Name = name;
			this.Path = path;
		}

		protected virtual void OnPropertyChanged ([CallerMemberName] string propertyName = null) {
			PropertyChangedEventHandler handler = this.PropertyChanged;

			if (handler != null) {
				handler (this, new PropertyChangedEventArgs (propertyName));
			}
		}
	}
}
