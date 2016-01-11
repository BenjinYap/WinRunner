

using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
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
				this.name = value;
				this.ValidateName ();
				base.OnPropertyChanged ();
			}
		}

		private string path;
		public string Path {
			get { return this.path; }
			set {
				this.path = value;
				this.ValidatePath ();
				base.OnPropertyChanged ();
				this.GetIconFromPath (value);
			}
		}

		private RegistryKey regKey;

		public RegistryApp () {
			this.Name = "";
			this.Path = "";
		}

		public RegistryApp (RegistryKey regKey) {
			this.regKey = regKey;
			this.Name = this.GetAppName (regKey.Name);
			this.Path = regKey.GetValue ("").ToString ();
		}

		public bool FlushToRegistry () {
			bool create = false;
			RegistryKey rootKey = RegistryHelper.OpenAppPaths ();
			
			if (this.regKey == null) {
				create = true;
			} else if (this.regKey.Name.ToLower ().Replace (".exe", "") != this.Name) {
				create = true;
				this.DeleteFromRegistry ();
			}
			
			if (create) {
				this.regKey = rootKey.CreateSubKey (this.Name + ".exe");
			}

			rootKey.Close ();
			this.regKey.SetValue ("", this.Path);
			
			return true;
		}

		public bool DeleteFromRegistry () {
			RegistryKey rootKey = RegistryHelper.OpenAppPaths ();
			rootKey.DeleteSubKey (System.IO.Path.GetFileName (this.regKey.Name), true);
			rootKey.Close ();
			return true;
		}

		private void ValidateName ([CallerMemberName] string propertyName = null) {
			RegistryKey rootKey = RegistryHelper.OpenAppPaths ();
			string [] appKeyNames = rootKey.GetSubKeyNames ();
			rootKey.Close ();
			
			foreach (string keyName in appKeyNames) {
				if (this.GetAppName (keyName.ToLower ()) == this.Name.ToLower () && (this.regKey == null || this.GetAppName (this.regKey.Name.ToLower ()) != this.Name.ToLower ())) {
					base.AddError (Resource.NameExists, propertyName);
					break;
				} else {
					base.RemoveError (Resource.NameExists, propertyName);
				}
			}

			if (this.Name.Length <= 0) {
				base.AddError (Resource.NameRequired, propertyName);
			} else {
				base.RemoveError (Resource.NameRequired, propertyName);
			}

			if (this.Name.Contains (@"\")) {
				base.AddError (Resource.NameInvalidBackslash, propertyName);
			} else {
				base.RemoveError (Resource.NameInvalidBackslash, propertyName);
			}

			if (this.Name.Contains (" ")) {
				base.AddError (Resource.NameInvalidSpace, propertyName);
			} else {
				base.RemoveError (Resource.NameInvalidSpace, propertyName);
			}

			if (this.Name.Contains (".")) {
				base.AddError (Resource.NameInvalidDot, propertyName);
			} else {
				base.RemoveError (Resource.NameInvalidDot, propertyName);
			}
		}

		private void ValidatePath ([CallerMemberName] string propertyName = null) {
			if (this.Path.Length <= 0) {
				base.AddError (Resource.PathRequired, propertyName);
			} else {
				base.RemoveError (Resource.PathRequired, propertyName);

				if (File.Exists (this.Path) == false) {
					base.AddError (Resource.PathDoesNotExist, propertyName);
				} else {
					base.RemoveError (Resource.PathDoesNotExist, propertyName);
				}
			}
		}

		private void GetIconFromPath (string path) {
			if (File.Exists (path)) {
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
			}
		}

		private string GetAppName (string path) {
			return System.IO.Path.GetFileNameWithoutExtension (path);
		}
	}
}
