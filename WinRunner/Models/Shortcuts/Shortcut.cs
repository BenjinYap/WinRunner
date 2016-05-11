

using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Windows.Media.Imaging;
using WinRunner.Models.ShortcutProperties;
using WinRunner.Resources;
namespace WinRunner.Models.Shortcuts {
	public enum ShortcutType { File, Batch, Folder, WebPage, MSEdge }

	public abstract class Shortcut:ModelBase {
		public const string TypeKeyName = "Type";

		protected readonly static string DocumentsPath = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.MyDocuments), "WinRunner");

		private BitmapImage icon;
		public BitmapImage Icon {
			get { return this.icon; }
			set {
				this.icon = value;
				OnPropertyChanged ();
			}
		}

		public string Name {
			get { return this.nameProp.Value; }
			set {
				this.nameProp.Value = value;
				this.ValidateProperty (this.nameProp);
				base.OnPropertyChanged ();
			}
		}
		private NameProperty nameProp;

		protected RegistryKey regKey;

		protected string oldName;

		private ShortcutType type;

		public Shortcut () {
			this.nameProp = new NameProperty ();
			this.Name = "";
			
			//assign the type of the shortcut
			if (this is WebPageShortcut) {
				this.type = ShortcutType.WebPage;
			} else if (this is MSEdgeShortcut) {
				this.type = ShortcutType.MSEdge;
			} else if (this is FileShortcut) {
				this.type = ShortcutType.File;
			} else if (this is FolderShortcut) {
				this.type = ShortcutType.Folder;
			} else if (this is BatchShortcut) {
				this.type = ShortcutType.Batch;
			}
		}

		public Shortcut (RegistryKey regKey):this () {
			this.regKey = regKey;
			this.nameProp = new NameProperty (regKey);
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
			
			if (this.regKey == null) {  //new shortcut, must create key
				create = true;
			} else if (this.GetAppName (this.regKey.Name) != this.Name) {  //shortcut was renamed
				create = true;  //must create key
				this.DeleteFromRegistry ();  //delete old key first
			}
			
			//create the key if required
			if (create) {
				this.regKey = rootKey.CreateSubKey (this.Name + ".exe");
				this.nameProp.RegKey = this.regKey;
			}

			//set the type key
			this.regKey.SetValue (Shortcut.TypeKeyName, this.type);

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
					bitmap.Save (memory, ImageFormat.Png);
					memory.Position = 0;
					BitmapImage bitmapimage = new BitmapImage();
					bitmapimage.BeginInit ();
					bitmapimage.StreamSource = memory;
					bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
					bitmapimage.EndInit ();
					
					this.Icon = bitmapimage;
				}
			} else {
				this.Icon = new System.Windows.Media.Imaging.BitmapImage (new Uri ("../Resources/missing-file.png", UriKind.Relative));
			}
		}

		protected void ValidateProperty (ShortcutProperty prop, [CallerMemberName] string propertyName = null) {
			foreach (KeyValuePair <string, bool> pair in prop.Errors) {
				if (pair.Value) {
					base.AddError (pair.Key, propertyName);
				} else {
					base.RemoveError (pair.Key, propertyName);
				}
			}
		}

		private string GetAppName (string path) {
			return System.IO.Path.GetFileNameWithoutExtension (path);
		}
	}
}
