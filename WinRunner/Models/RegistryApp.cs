

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;
using WinRunner.Resources;
namespace WinRunner.Models {
	public class RegistryApp:ModelBase {
		//public event PropertyChangedEventHandler PropertyChanged;

		private BitmapImage icon;
		public BitmapImage Icon {
			get { return this.icon; }
			set {
				this.icon = value;
				OnPropertyChanged ();
			}
		}

		private string name;
		public string Name {
			get { return this.name; }
			set {
				if (value == "awd") {
					base.AddError (Resource.NameExists);
				} else {
					base.RemoveError (Resource.NameExists);
				}

				this.name = value;
				base.OnPropertyChanged ();
			}
		}

		private string path;
		public string Path {
			get { return this.path; }
			set {
				this.path = value;
				base.OnPropertyChanged ();
				this.GetIconFromPath (value);
			}
		}

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

		private void GetIconFromPath (string path) {
			try {
				System.Drawing.Icon icon = System.Drawing.Icon.ExtractAssociatedIcon (path);
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

		//protected virtual void OnPropertyChanged ([CallerMemberName] string propertyName = null) {
		//	PropertyChangedEventHandler handler = this.PropertyChanged;

		//	if (handler != null) {
		//		handler (this, new PropertyChangedEventArgs (propertyName));
		//	}
		//}
	}
}
