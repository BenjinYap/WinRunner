

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
namespace WinRunner.Models.Apps {
	public enum AppType { Path, Batch }

	public abstract class App:ModelBase {
		
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

		protected RegistryKey regKey;

		private string oldName;

		public App () {
			this.Name = "";
		}

		public App (RegistryKey regKey) {
			this.regKey = regKey;
			this.Name = this.GetAppName (regKey.Name);
		}

		public virtual void RememberProperties () {
			this.oldName = this.Name;
		}

		public virtual void RevertProperties () {
			this.Name = this.oldName;
		}

		public virtual void FlushToRegistry () {
			bool create = false;
			RegistryKey rootKey = RegistryHelper.OpenAppPaths ();
			
			if (this.regKey == null) {
				create = true;
			} else if (this.GetAppName (this.regKey.Name) != this.Name) {
				create = true;
				this.DeleteFromRegistry ();
			}
			
			if (create) {
				this.regKey = rootKey.CreateSubKey (this.Name + ".exe");
			}

			rootKey.Close ();
		}

		public virtual void DeleteFromRegistry () {
			RegistryKey rootKey = RegistryHelper.OpenAppPaths ();
			rootKey.DeleteSubKey (System.IO.Path.GetFileName (this.regKey.Name), true);
			rootKey.Close ();
		}

		protected void GetIconFromPath (string path) {
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

		private string GetAppName (string path) {
			return System.IO.Path.GetFileNameWithoutExtension (path);
		}
	}
}
